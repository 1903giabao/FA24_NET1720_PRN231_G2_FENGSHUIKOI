using Microsoft.AspNetCore.Mvc;

namespace FENGSHUIKOI.MVCApp.Controllers
{
    public class MembersV2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();

        }

    }
}
