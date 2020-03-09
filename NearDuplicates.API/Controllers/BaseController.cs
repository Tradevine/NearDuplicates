using System;
using System.Data.SqlClient;
using System.Text;
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
            filterContext.Result = Json(new { error = BuildErrorMessage(filterContext.Exception) }, JsonRequestBehavior.AllowGet);
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 500;

            base.OnException(filterContext);
        }

        private string BuildErrorMessage(Exception ex)
        {
            var sb = new StringBuilder();

            var innermost = GetInnermostException(ex);

            sb.AppendLine(innermost.Message);
            sb.AppendLine(GetSqlErrors(ex));

            return sb.ToString();
        }

        public Exception GetInnermostException(Exception ex)
        {
            Exception inner = ex.InnerException;

            while (null != inner)
            {
                if (null != inner.InnerException)
                {
                    inner = inner.InnerException;
                }
                else
                {
                    break;
                }
            }

            return inner ?? ex;
        }

        private string GetSqlErrors(Exception ex)
        {
            var sb = new StringBuilder();

            try
            {
                AppendSqlErrorsIfAny(ex, sb);

                var innerException = ex.InnerException;

                while (innerException != null)
                {
                    AppendSqlErrorsIfAny(innerException, sb);
                    innerException = innerException.InnerException;
                }
            }
            catch(Exception exInHandler)
            {
                sb.AppendLine($"Exception getting SQL errors {exInHandler.Message}, {ex.StackTrace}");
            }

            return sb.ToString();
        }

        private void AppendSqlErrorsIfAny(Exception ex, StringBuilder sb)
        {
            try
            {
                var sqlException = ex as SqlException;
                if (sqlException == null)
                    return;

                foreach (var sqlExceptionError in sqlException.Errors)
                {
                    sb.AppendLine(sqlExceptionError.ToString());
                }
            }
            catch(Exception exInHandler)
            {
                sb.AppendLine($"Exception getting SQL errors {exInHandler.Message}, {ex.StackTrace}");
            }
        }
    }
}
