using System;
using System.Web.Mvc;
using NearDuplicatesAnalysis.Model.Services;

namespace NearDuplicates.API.Controllers
{
    public class ActionsController : BaseController
    {
        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Post)]
        public JsonResult Analyze(string mcat_path, int? seller_id, string job_id)
        {
            if(IsPreFlight())
                return new JsonResult();

            if (seller_id.HasValue)
            {
                new ListingDownloader().RefreshSeller(seller_id.Value, job_id);
                AnalyseListings.ProcessSeller(seller_id.Value, job_id);
            }
            else if (!string.IsNullOrEmpty(mcat_path))
            {
                new ListingDownloader().RefreshCategory(mcat_path, job_id);
                AnalyseListings.ProcessCategory(mcat_path, job_id);
            }

            ProgressManager.UpdateJobPercent(job_id, 100M);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Get)]
        public JsonResult GetJobProgress(string job_id)
        {
            return  Json(ProgressManager.GetJobPercentProgress(job_id), JsonRequestBehavior.AllowGet);
        }
    }
}
