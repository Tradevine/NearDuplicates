using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using NearDuplicatesAnalysis.Model.Models;
using NearDuplicatesAnalysis.Model.Repository;

namespace NearDuplicatesAnalysis.Model.Services
{
    public class ListingDownloader
    {
        const string connectionString = "Persist Security Info=False;Integrated Security=SSPI; database = trademe; server = TMDATA";

        public List<string> GetCategoriesFromDb()
        {
            var output = new List<string>();

            var connection = new SqlConnection(connectionString);
            connection.Open();

            var query = "select mcat_path from  dbo.category with (nolock) where categoryid > 0 order by mcat_path";

            using (var cmd = new SqlCommand(query, connection))
            {
                var results = cmd.ExecuteReader();
                while (results.Read())
                {
                    output.Add(results.GetString(0));
                }

                results.Close();
            }

            return output;
        }

        public void RefreshCategory(string mcat_path, string job_id)
        {
            var context = new NearDuplicatesDbContext();

            ProgressManager.UpdateJobPercent(job_id, 1M);

            RemoveListingsFromDb(context, mcat_path, job_id);

            var output = GetNewListingsFromDb(mcat_path);

            ProgressManager.IncrementJobPercentBy(job_id, 2M);

            AddBackToDatabase(context, output, job_id);

            ProgressManager.IncrementJobPercentBy(job_id, 2M);
        }

        public void RefreshSeller(int seller_id, string job_id)
        {
            var context = new NearDuplicatesDbContext();

            ProgressManager.UpdateJobPercent(job_id, 1M);

            RemoveListingsFromDb(context, seller_id, job_id);

            var output = GetNewListingsFromDb(seller_id);

            ProgressManager.IncrementJobPercentBy(job_id, 2M);

            AddBackToDatabase(context, output, job_id);

            ProgressManager.IncrementJobPercentBy(job_id, 2M);
        }

        private void AddBackToDatabase(NearDuplicatesDbContext context, List<Listing> listings, string job_id)
        {
            var singleItemProgress = ProgressManager.CalculateLoopIncrement(listings.Count(), 0.05M);

            foreach (var listing in listings)
            {
                var existing = context.Listings.Find(listing.id);

                if (existing != null)
                {
                    context.Listings.Remove(existing);
                }

                context.Listings.Add(listing);

                ProgressManager.IncrementJobPercentBy(job_id, singleItemProgress);
            }
            context.SaveChanges();

        }

        private void RemoveListingsFromDb(NearDuplicatesDbContext context, string mcat_path, string job_id)
        {
            var listingCount = context.Listings.AsQueryable().Count(x => x.mcat_path.StartsWith(mcat_path));

            var singleItemProgress = ProgressManager.CalculateLoopIncrement(listingCount, 0.2M);

            context.Listings.AsQueryable().Where(x => x.mcat_path.StartsWith(mcat_path)).ToList().ForEach(y =>
            {
                context.Listings.Remove(y);
                ProgressManager.IncrementJobPercentBy(job_id, singleItemProgress);
            });
        }

        private void RemoveListingsFromDb(NearDuplicatesDbContext context, int seller_id, string job_id)
        {
            var listingCount = context.Listings.AsQueryable().Count(x => x.seller_id == seller_id);

            context.Listings.RemoveRange(context.Listings.AsQueryable().Where(x => x.seller_id == seller_id));

            ProgressManager.IncrementJobPercentBy(job_id, 0.2M);
        }

        private List<Listing> GetNewListingsFromDb(string mcat_path)
        {
            return GetNewListingsFromQuery(GetListingsQuery(mcat_path));
        }

        private List<Listing> GetNewListingsFromDb(int seller_id)
        {
            return GetNewListingsFromQuery(GetListingsQuery(seller_id));
        }

        private List<Listing> GetNewListingsFromQuery(string query)
        {
            var output = new List<Listing>();

            var connection = new SqlConnection(connectionString);
            connection.Open();

            using (var cmd = new SqlCommand(query, connection))
            {
                var results = cmd.ExecuteReader();

                while (results.Read())
                {
                    var listing = new Listing
                    {
                        id = results.GetInt64(0),
                        seller_id = results.GetInt32(1),
                        seller_name = results.GetString(2),
                        category_id = results.GetInt32(3),
                        mcat_path = results.GetString(4),
                        title = results.GetString(5),
                        buy_now_price = !results.IsDBNull(6) ? results.GetDecimal(6) : (decimal?)null,
                        description = results.GetString(7),
                        photo_id = !results.IsDBNull(8) ? results.GetInt32(8) : (int?)null,
                    };

                    output.Add(listing);
                }

                results.Close();
            }

            return output;
        }

        private string GetListingsQuery(string mcat_path)
        {
            return $"{GetBaseQuery()} where c.mcat_path like '{mcat_path}%'";
        }

        private string GetListingsQuery(int seller_id)
        {
            return $"{GetBaseQuery()} where a.memberid = {seller_id}";
        }

        private string GetBaseQuery()
        {
            return $"select a.auctionid, a.memberId, m.Nickname, a.categoryid, c.mcat_path, a.title,a.BuyNowPrice,ad.Body,isnull(a.photo_id, 0) as photo_id from dbo.auction a with (nolock)join dbo.member m with (nolock) on m.MemberId = a.MemberId join dbo.category c with (nolock) on c.CategoryId = a.CategoryId join dbo.auction_description ad with (nolock) on ad.auctionid = a.auctionid";
        }
    }
}


