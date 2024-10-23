using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DoAnPhanMem.Controllers
{
    public class UserSessionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                var username = context.HttpContext.Session.GetString("username");
                controller.ViewBag.Username = username;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
