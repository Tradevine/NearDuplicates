using System;
using System.Collections.Generic;
using System.Linq;
using NearDuplicatesAnalysis.Model.Helpers;
using NearDuplicatesAnalysis.Model.Models;

namespace NearDuplicatesAnalysis.Model.Services
{
    public static class AnalyseListingTitles
    {
        private const int ngramLength = 3;
        private const int minHashCount = 200;

        public static void ProcessTitles(List<Listing> listings, string job_id, Dictionary<long, long> duplicates)
        {
            BuildTitleMinHashes(listings, job_id);
            CalculateLshForListingSet(listings, job_id, duplicates);
        }

        private static void BuildTitleMinHashes(List<Listing> listings, string job_id)
        {
            var universe = new Dictionary<string, int>();
            var wordId = 0;
            var mh = new MinHash(200000, minHashCount);

            var singleItemProgress = ProgressManager.CalculateLoopIncrement(listings.Count(), 0.1M);

            // Build ngrams for each listing (if not done already) and save to universe of ngrams
            foreach (var listing in listings.ToList())
            {
                if (!listing.ngrams_title.Any())
                {
                    listing.ngrams_title = mh.GetProfile(listing.title, ngramLength);
                }

                foreach (var ngram in listing.ngrams_title.Keys)
                {
                    if (!universe.ContainsKey(ngram))
                    {
                        universe[ngram] = wordId++;
                    }
                }

                ProgressManager.IncrementJobPercentBy(job_id, singleItemProgress);
            }

            singleItemProgress = ProgressManager.CalculateLoopIncrement(listings.Count(), 0.1M);

            mh = new MinHash(universe.Count, minHashCount);

            foreach (var listing in listings)
            {
                if(listing.minhash_title.Any())
                    continue;

                // Set word ID in each document
                foreach(var ngram in listing.ngrams_title.Keys)
                {
                    listing.word_ids_title.Add(universe[ngram]);
                }

                // Calculate min hash for each listing
                listing.minhash_title = mh.GetMinHash(listing.word_ids_title);

                ProgressManager.IncrementJobPercentBy(job_id, singleItemProgress);
            }
        }

        private static void CalculateLshForListingSet(List<Listing> listings, string job_id, Dictionary<long, long> duplicates)
        {
            var numSimilarityBuckets = (int)Math.Ceiling(listings.Count / 100M);

            // First make 2 dimensional array (docs by min-hashes)
            var matrix = new int[listings.Count, minHashCount];

            for (int listing = 0; listing < listings.Count; listing++)
            {
                for (int hash = 0; hash < listings[listing].minhash_title.Count; hash++)
                {
                    matrix[listing, hash] = (int)listings[listing].minhash_title[hash];
                }
            }

            // Now set LSH
            var lsh = new LSH(matrix, numSimilarityBuckets);
            lsh.Calc();

            // Set closes duplicate on each listing
            var singleItemProgress = ProgressManager.CalculateLoopIncrement(listings.Count(), 0.1M);

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

                if(duplicates.ContainsKey(nearestListing.id))
                    continue;

                listings[listing].likely_duplicate_id_by_title = nearestListing.id;
                listings[listing].similarity_title = Jaccard.Calc(ArrayHelpers.GetRow<int>(matrix, listing).ToList(), nearest);
                duplicates[nearestListing.id] = thisListing.id;
                duplicates[thisListing.id] = nearestListing.id;
            }
        }
    }
}


