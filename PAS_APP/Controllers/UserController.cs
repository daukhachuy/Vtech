using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PAS_APP.Models;
using PAS_APP.Services;
using System.Diagnostics;

namespace PAS_APP.Controllers
{

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _user;

        public UserController(ILogger<UserController> logger , IUserService user)
        {
            _logger = logger;
            _user = user;
        }

        public IActionResult UserHome()
        {
             var account = GetUsername();
            if (account == false)
            {
                TempData["Error"] = "Vui lòng đăng nhập để tiếp tục";
                return RedirectToAction("Signin", "Account");
            }
            ViewBag.Username = HttpContext.Session.GetString("Username");

            return View();
        }

        public IActionResult Filter()
        {
            return View();
        }

        [HttpGet]
        public async  Task<IActionResult> Profile()
        {
            var account = GetUsername();
            if (account == false)
            {
                TempData["Error"] = "Vui lòng đăng nhập để tiếp tục";
                return RedirectToAction("Signin", "Account");
            }
            ViewBag.Username =  HttpContext.Session.GetString("Username");

            var acc = await _user.GetUserByEmail(HttpContext.Session.GetString("Email"));
            ViewBag.Account = acc;
            return View();    
        }

        [HttpPost]
        public async Task<IActionResult> Profile(string name , DateTime dob , string phone , string pass , string companyname , string code , string address )
        {
            var account = GetUsername();
            if (account == false)
            {
                TempData["Error"] = "Vui lòng đăng nhập để tiếp tục";
                return RedirectToAction("Signin", "Account");
            }
            ViewBag.Username = HttpContext.Session.GetString("Username");



            var email = HttpContext.Session.GetString("Email");
            var acc = await _user.GetUserByEmail(email);


            var updateapp = new User
            {
                UserId = acc.UserId,
                UserName = name ?? acc.UserName,              
                Email = acc.Email,
                Phone = phone ?? acc.Phone,
                Dob = dob != default(DateTime) ? dob : acc.Dob,
                PassWord = pass ?? acc.PassWord,
                CompanyName = companyname ?? acc.CompanyName,
                CompanyCode = code ?? acc.CompanyCode,
                CompanyAddress = address ?? acc.CompanyAddress,
            };
            
             var updateResult = await _user.UpdateProfile(updateapp);
            if (updateResult)
            {
                TempData["Success"] = "Cập nhật thông tin thành công.";
                // Cập nhật lại session nếu tên người dùng thay đổi
                HttpContext.Session.SetString("Username", updateapp.UserName ?? email);
                ViewBag.Account = acc;
            }
            else
            {
                TempData["Error"] = "Cập nhật thông tin thất bại.";
            }
            return RedirectToAction(nameof(Profile));
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult FileExcel()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public bool GetUsername()
        {
            var role = HttpContext.Session.GetString("Role");
            var username = HttpContext.Session.GetString("Username");
            var email = HttpContext.Session.GetString("Email");
            // Kiểm tra: nếu chưa đăng nhập hoặc không phải User
            if (string.IsNullOrEmpty(username) || role != "User" || string.IsNullOrEmpty(email)) 
            {
                return  false;
            }
            return true;

        }
    }
}
