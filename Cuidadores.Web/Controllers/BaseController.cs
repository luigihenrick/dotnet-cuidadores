using System.Web.Mvc;
using System.Web.Routing;
using Cuidadores.Core.Entities;

namespace Cuidadores.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Pessoa CurrentUser;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuario = filterContext.HttpContext.Session["Usuario"];

            CurrentUser = (Pessoa)usuario;

            ViewBag.CurrentUser = CurrentUser;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var usuario = filterContext.HttpContext.Session["Usuario"];

            if (usuario == null 
                && filterContext.ActionDescriptor.ControllerDescriptor.ControllerType != typeof(HomeController))
            {
                AddMessageWarning("Não logado");
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Home", action = "Login" }));
            }
        }

        protected void AddMessageSuccess(string messageSuccess)
        {
            TempData["success"] = messageSuccess;
        }
        
        protected void AddMessageWarning(string messageWarning)
        {
            TempData["warning"] = messageWarning;
        }
        
        protected void AddMessageError(string messageError)
        {
            TempData["error"] = messageError;
        }
    }
}