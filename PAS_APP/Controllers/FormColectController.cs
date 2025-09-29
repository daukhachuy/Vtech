using Microsoft.AspNetCore.Mvc;
using PAS_APP.Services;

namespace PAS_APP.Controllers
{
    public class FormColectController : Controller
    {
        private readonly IFormService _formService; 

        private readonly IUserService _userService;
        public FormColectController(IFormService formService , IUserService userService) 
        { 
            _formService = formService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Frame1(string formId)
        {
            var form = await _formService.GetByIdAsync(formId);
            var user = await _userService.GetUserByFormIdAsync(formId);
            TempData["cpname"] = user.CompanyName;
            TempData["cpaddress"] = user.CompanyAddress;
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Frame1(Models.Student student , string formId)
        {            
            return View();
        }
    }
}
