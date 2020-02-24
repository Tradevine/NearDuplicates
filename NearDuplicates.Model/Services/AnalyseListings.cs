using System.Linq;
using NearDuplicatesAnalysis.Model.Repository;

namespace NearDuplicatesAnalysis.Model.Services
{
    public static class AnalyseListings
    {
        public static void ProcessSeller(int sellerId)
        {
            var context = new NearDuplicatesDbContext();
            var listings = context.Listings.Where(x => x.seller_id == sellerId).ToList();

            AnalyseListingTitles.ProcessTitles(listings);
            AnalyseListingDescriptions.ProcessDescriptions(listings);

            context.SaveChanges();
        }
    }
}


