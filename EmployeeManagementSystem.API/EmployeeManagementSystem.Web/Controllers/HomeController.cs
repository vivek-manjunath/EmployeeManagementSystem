using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}