using System;
using System.Collections.Generic;
using System.Linq;
using NearDuplicatesAnalysis.Model.Helpers;
using NearDuplicatesAnalysis.Model.Models;

namespace NearDuplicatesAnalysis.Model.Services
{
    public static class AnalyseListingDescriptions
    {
        private const int nGramLength = 9;
        private const int minHashCount = 200;

        public static void ProcessDescriptions(List<Listing> listings, string job_id)
        {
            BuildTitleMinHashes(listings);
            CalculateLshForListingSet(listings, job_id);
        }

        private static void BuildTitleMinHashes(List<Listing> listings)
        {
            var universe = new Dictionary<string, int>();
            var wordId = 0;
            var mh = new MinHash(200000, minHashCount);

            // Build ngrams for each listing (if not done already) and save to universe of ngrams
            foreach (var listing in listings.ToList())
            {
                if (!listing.ngrams_description.Any())
                {
                    listing.ngrams_description = mh.GetProfile(listing.description, nGramLength);
                }

                foreach (var ngram in listing.ngrams_description.Keys)
                {
                    if (!universe.ContainsKey(ngram))
                    {
                        universe[ngram] = wordId++;
                    }
                }
            }

            mh = new MinHash(universe.Count, minHashCount);

            foreach (var listing in listings)
            {
                if(listing.minhash_description.Any())
                    continue;

                // Set word ID in each document
                foreach(var ngram in listing.ngrams_description.Keys)
                {
                    listing.word_ids_description.Add(universe[ngram]);
                }

                // Calculate min hash for each listing
                listing.minhash_description= mh.GetMinHash(listing.word_ids_description);
            }
        }

        private static void CalculateLshForListingSet(List<Listing> listings, string job_id)
        {
            var numSimilarityBuckets = (int)Math.Ceiling(listings.Count / 100M);

            // First make 2 dimensional array (docs by min-hashes)
            var matrix = new int[listings.Count, minHashCount];

            for (int listing = 0; listing < listings.Count; listing++)
            {
                for (int hash = 0; hash < listings[listing].minhash_description.Count; hash++)
                {
                    matrix[listing, hash] = (int)listings[listing].minhash_description[hash];
                }
            }

            // Now set LSH
            var lsh = new LSH(matrix, numSimilarityBuckets);
            lsh.Calc();

            // Set closes duplicate on each listing
            var duplicatesFound = new Dictionary<long, long>();
            var singleItemProgress = ProgressManager.CalculateLoopIncrement(listings.Count(), 0.4M);

            for (int listing = 0; listing < listings.Count; listing++)
            {
                ProgressManager.IncrementJobPercentBy(job_id, singleItemProgress);

                var nearest = lsh.GetNearest(listing);
                if(!nearest.Any())
                    continue;

                var thisListing = listings[listing];
                var nearestListing = listings[nearest[0]];

                var priceRatio = nearestListing.buy_now_price / thisListing.buy_now_price;
                if(priceRatio < 0.8M || priceRatio > 1.2M)
                    continue;

                if(duplicatesFound.ContainsKey(nearestListing.id))
                    continue;

                listings[listing].likely_duplicate_id_by_description = nearestListing.id;
                listings[listing].similarity_description = Jaccard.Calc(ArrayHelpers.GetRow<int>(matrix, listing).ToList(), nearest);
            }
        }
    }
}


