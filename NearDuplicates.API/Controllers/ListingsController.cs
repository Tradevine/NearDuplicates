﻿using System.Linq;
using System.Web.Mvc;
using NearDuplicatesAnalysis.Model.Models;
using NearDuplicatesAnalysis.Model.Repository;

namespace NearDuplicates.API.Controllers
{
    public class ListingsController : Controller
    {
        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Get)]
        public JsonResult GetDuplicatesList()
        {
            if(IsPreFlight())
                return new JsonResult();

            var context = new NearDuplicatesDbContext();

            var listings = context.Listings.Where(x => x.likely_duplicate_id_by_title != null || x.likely_duplicate_id_by_description != null).ToList();
            var output = listings.Select(x => new
            {
                x.id,
                x.category_id,
                x.mcat_path,
                x.seller_id,
                x.seller_name,
                x.title
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
                closestDuplicateByTitle,
                closestDuplicateByDescription
            };

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        private bool IsPreFlight()
        {
            return Request?.HttpMethod == "OPTIONS";
        }
    }
}
