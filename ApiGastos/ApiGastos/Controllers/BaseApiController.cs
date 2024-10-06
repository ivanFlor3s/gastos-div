using Microsoft.AspNetCore.Mvc;

namespace ApiGastos.Controllers
{
    public class ApiBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
