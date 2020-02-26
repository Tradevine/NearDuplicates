using System.Linq;
using System.Web.Mvc;
using NearDuplicatesAnalysis.Model.Models;
using NearDuplicatesAnalysis.Model.Repository;

namespace NearDuplicates.API.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsPreFlight()
        {
            return Request?.HttpMethod == "OPTIONS";
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = Json(new { error = filterContext.Exception.Message });
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 500;

            base.OnException(filterContext);
        }
    }
}
