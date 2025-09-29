using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAS_APP.Models;
using PAS_APP.Services;

namespace PAS_APP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;

        public AccountController(IUserService userService, IAdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(string username, string password)
        {
            // Check admin trước
            if (_adminService.Validate(username, password))
            {
                HttpContext.Session.SetString("Role", "Admin");
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Admin", "AdminHome");
            }

            // Check user DB
            var user = await _userService.ValidateUserAsync(username, password);
            if (user != null)
            {
                HttpContext.Session.SetString("Role", "User");
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", user.UserName ?? user.Email);
                HttpContext.Session.SetString("Email", user.Email);
                return RedirectToAction("User", "UserHome");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }



        [HttpGet]
        public IActionResult GoogleLogin(string? returnUrl = "/")
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", new { returnUrl }),
                // Bắt Google luôn hiển thị màn hình chọn tài khoản
                Items = { ["prompt"] = "select_account" }
            };

            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> GoogleResponse(string? returnUrl = "/")
        {

            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                ViewBag.Error = "Đăng nhập Google thất bại.";
                return View("Signin");
            }

            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Không lấy được email từ Google.";
                return View("Signin");
            }
           // Lưu session
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                // Không tồn tại -> báo lỗi và quay lại Signin
                ViewBag.Error = "Tài khoản không tồn tại";
                return View("Signin");
            }
            // Nếu tồn tại -> lưu session và chuyển đến UserHome
            HttpContext.Session.SetString("Role", "User");
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.UserName ?? user.Email);
            HttpContext.Session.SetString("Email", user.Email);

            return RedirectToAction("UserHome", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Signin");
        }
    }
}
