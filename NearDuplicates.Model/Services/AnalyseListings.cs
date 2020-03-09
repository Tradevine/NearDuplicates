using System.Collections.Generic;
using System.Linq;
using NearDuplicatesAnalysis.Model.Models;
using NearDuplicatesAnalysis.Model.Repository;

namespace NearDuplicatesAnalysis.Model.Services
{
    public static class AnalyseListings
    {
        public static void ProcessCategory(string mcat_path, string job_id)
        {
            var context = new NearDuplicatesDbContext();
            var listings = context.Listings.Where(x => x.mcat_path.StartsWith(mcat_path)).ToList();

            ProcessListings(listings, job_id, context);
        }

        public static void ProcessSeller(int seller_id, string job_id)
        {
            var context = new NearDuplicatesDbContext();
            var listings = context.Listings.Where(x => x.seller_id == seller_id).ToList();

            ProcessListings(listings, job_id, context);
        }

        private static void ProcessListings(List<Listing> listings, string job_id, NearDuplicatesDbContext context)
        {
            var duplicates = new Dictionary<long, long>();

            AnalyseListingTitles.ProcessTitles(listings, job_id, duplicates);
            AnalyseListingDescriptions.ProcessDescriptions(listings, job_id, duplicates);

            context.SaveChanges();
        }
    }
}


