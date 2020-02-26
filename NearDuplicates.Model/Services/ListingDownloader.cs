﻿using System.Collections.Generic;
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

        public void RefreshCategory(string mcat_path)
        {
            var context = new NearDuplicatesDbContext();
            RemoveListingsFromDb(context, mcat_path);
            var output = GetNewListingsFromDb(mcat_path);
            context.Listings.AddRange(output);
            context.SaveChanges();
        }

        private void RemoveListingsFromDb(NearDuplicatesDbContext context, string mcat_path)
        {
            context.Listings.SqlQuery($"delete from Listings where mcat_path like '@mcat_path%'", new SqlParameter("@mcat_path", mcat_path));
        }

        private List<Listing> GetNewListingsFromDb(string mcat_path)
        {
            var output = new List<Listing>();

            var connection = new SqlConnection(connectionString);
            connection.Open();

            var query = GetListingsQuery(mcat_path);

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

        private string GetListingsQuery(string mcat_path)
        {
            return $"select a.auctionid, a.memberId, m.Nickname, a.categoryid, c.mcat_path, a.title,a.BuyNowPrice,ad.Body,isnull(a.photo_id, 0) as photo_id from dbo.auction a with (nolock)join dbo.member m with (nolock) on m.MemberId = a.MemberId join dbo.category c with (nolock) on c.CategoryId = a.CategoryId join dbo.auction_description ad with (nolock) on ad.auctionid = a.auctionid where c.mcat_path like '{mcat_path}%'";
        }
    }
}


