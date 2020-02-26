using System.Web.Mvc;
using NearDuplicatesAnalysis.Model.Services;

namespace NearDuplicates.API.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsPreFlight()
        {
            return Request?.HttpMethod == "OPTIONS";
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DatabaseManager.CheckDatabase();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = Json(new { error = filterContext.Exception.Message }, JsonRequestBehavior.AllowGet);
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 500;

            base.OnException(filterContext);
        }
    }
}
