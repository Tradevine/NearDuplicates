using System;
using System.Web.Mvc;
using NearDuplicatesAnalysis.Model.Services;

namespace NearDuplicates.API.Controllers
{
    public class ActionsController : BaseController
    {
        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Post)]
        public JsonResult AnalyzeCategory(string mcat_path)
        {
            if(IsPreFlight())
                return new JsonResult();

            new ListingDownloader().RefreshCategory(mcat_path);
            AnalyseListings.ProcessCategory(mcat_path);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}
