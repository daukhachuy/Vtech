using Microsoft.AspNetCore.Mvc;

namespace PAS_APP.Controllers
{
    public class FormColectController : Controller
    {
        [HttpGet]
        public IActionResult Frame1()
        {

            return View();
        }
    }
}
