using System.Linq;
using System.Web.Mvc;
using NearDuplicatesAnalysis.Model.Models;
using NearDuplicatesAnalysis.Model.Repository;
using NearDuplicatesAnalysis.Model.Services;

namespace NearDuplicates.API.Controllers
{
    public class ListingsController : BaseController
    {
        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Get)]
        public JsonResult GetCategories()
        {
            if(IsPreFlight())
                return new JsonResult();

            var output = new ListingDownloader().GetCategoriesFromDb();

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Get)]
        public JsonResult GetSellers(string mcat_path, int? seller_id)
        {
            if(IsPreFlight())
                return new JsonResult();

            var context = new NearDuplicatesDbContext();

            var listings = context.Listings.Where(x =>
                (string.IsNullOrEmpty(mcat_path) || x.mcat_path.StartsWith(mcat_path)) &&
                (seller_id == null || x.seller_id == seller_id.Value) &&
                (x.likely_duplicate_id_by_title != null && x.likely_duplicate_id_by_description == x.likely_duplicate_id_by_title))
                .ToList();

            var sellers = listings.GroupBy(x => x.seller_id).ToList();

            var output = sellers.Select(x => new
            {
                seller_id = x.Key,
                x.First().seller_name,
                listings_count = x.Count()
            });

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Get)]
        public JsonResult GetListingsForSeller(int seller_id, string mcat_path)
        {
            if(IsPreFlight())
                return new JsonResult();

            var context = new NearDuplicatesDbContext();

            var listings = context.Listings.Where(x =>
                    x.seller_id == seller_id &&
                    x.mcat_path.StartsWith(mcat_path) &&
                    (x.likely_duplicate_id_by_title != null && x.likely_duplicate_id_by_description == x.likely_duplicate_id_by_title)
                    ).ToList();

            var output = listings
                .OrderByDescending(a => a.similarity_title)
                .ThenByDescending(b => b.similarity_description)
                .Select(x => new
            {
                x.id,
                x.category_id,
                x.mcat_path,
                x.seller_id,
                x.seller_name,
                x.title,
                x.similarity_title,
                x.similarity_description
            });

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Get)]
        public JsonResult GetComparison(long listing_id)
        {
            if(IsPreFlight())
                return new JsonResult();

            var context = new NearDuplicatesDbContext();
            var query = context.Listings.AsQueryable();

            var baseListing = query.First<Listing>(x => x.id == listing_id);
            var closestDuplicateByTitle = baseListing.likely_duplicate_id_by_title.HasValue ? query.FirstOrDefault(x => x.id == baseListing.likely_duplicate_id_by_title) : null;
            var closestDuplicateByDescription = baseListing.likely_duplicate_id_by_description.HasValue ? query.FirstOrDefault(x => x.id == baseListing.likely_duplicate_id_by_description) : null;

            var output = new
            {
                baseListing,
                duplicate = closestDuplicateByTitle ?? closestDuplicateByDescription,
            };

            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}
