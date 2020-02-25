using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using NearDuplicatesAnalysis.Model.Models;
using NearDuplicatesAnalysis.Model.Repository;

namespace NearDuplicatesAnalysis.Model.Services
{
    public class ListingDownloader
    {
        public void GetNewListings()
        {
            var context = new NearDuplicatesDbContext();
            var output = GetNewListingsFromDb();
            context.Listings.AddRange(output);
            context.SaveChanges();
        }

        private List<Listing> GetNewListingsFromDb()
        {
            var output = new List<Listing>();

            const string connectionString = "Persist Security Info=False;Integrated Security=SSPI; database = trademe; server = TMDATA";

            var connection = new SqlConnection(connectionString);
            connection.Open();

            var query = File.ReadAllText("Repository/GetListings.sql");

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
                        buy_now_price = results.GetDecimal(6),
                        description = results.GetString(7),
                        photo_id = results.GetInt32(8)
                    };

                    output.Add(listing);
                }

                results.Close();
            }

            return output;
        }
    }
}


