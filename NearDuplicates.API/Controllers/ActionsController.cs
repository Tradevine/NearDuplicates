using System;
using System.Web.Mvc;
using NearDuplicatesAnalysis.Model.Services;

namespace NearDuplicates.API.Controllers
{
    public class ActionsController : BaseController
    {
        [AcceptVerbs(HttpVerbs.Options | HttpVerbs.Post)]
        public JsonResult AnalyzeCategory(string mcat_path, string job_id)
        {
            if(IsPreFlight())
                return new JsonResult();

            new ListingDownloader().RefreshCategory(mcat_path, job_id);
            AnalyseListings.ProcessCategory(mcat_path, job_id);

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
