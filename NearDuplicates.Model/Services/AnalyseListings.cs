using System.Linq;
using NearDuplicatesAnalysis.Model.Repository;

namespace NearDuplicatesAnalysis.Model.Services
{
    public static class AnalyseListings
    {
        public static void ProcessCategory(string mcat_path, string job_id)
        {
            var context = new NearDuplicatesDbContext();
            var listings = context.Listings.Where(x => x.mcat_path.StartsWith(mcat_path)).ToList();

            AnalyseListingTitles.ProcessTitles(listings, job_id);
            AnalyseListingDescriptions.ProcessDescriptions(listings, job_id);

            context.SaveChanges();
        }
    }
}


