using Microsoft.AspNetCore.Mvc;

namespace HistoryGenPin.Areas.Admin.Controllers
{
    [Route("")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        [Route("Admin")]
        [Route("Admin/AccountManagement")]
        public IActionResult AccountManagement()
        {
            return View();
        }

        [Route("Admin/RoleManagement")]
        public IActionResult RoleManagement()
        {
            return View();
        }
        
        [Route("Admin/ReportManagement")]
        public IActionResult ReportManagement()
        {
            return View();
        }
    }
}
