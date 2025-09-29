using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using PAS_APP.Models;
using PAS_APP.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PAS_APP.Controllers
{

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _user;

        private readonly IServiceService _service;
        private readonly IFormService _form;

        private readonly IStudentService _student;

        public UserController(ILogger<UserController> logger , IUserService user , IServiceService service ,IFormService form ,IStudentService student)
        {
            _logger = logger;
            _user = user;
            _service = service;
            _form = form;
            _student = student;
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
            var package = await _user.GetPackageAsync(acc.UserId);

            ViewBag.Packkage = package?.Package?.Title ?? "Chưa có gói dịch vụ";
            ViewBag.Price = package?.Package?.Price ?? 0;
            ViewBag.Detail = package?.Package?.Detail ?? "";

            if (package?.DateBuyPackage != null && package?.Package.Due > 0)
            {
                int monthsToAdd = package.Package.Due; 
                DateTime startDate = package.DateBuyPackage.Value; 
                DateTime dueDate = startDate.AddMonths(monthsToAdd); 
                ViewBag.Time = dueDate.ToString("dd/MM/yyyy"); 
                ViewBag.DueDate = package.DateBuyPackage?.ToString("dd/MM/yyyy");

            }
            else
            {
                ViewBag.DueDate = "N/A";
                ViewBag.Time = "N/A";
            }

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

        [HttpGet]
        public async  Task<IActionResult> Formm()
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
            var forms = await _form.GetFormsByUserId(acc.UserId);
            if (acc.CompanyCode == null)
            {
                TempData["Error"] = "Vui lòng thêm mã công ty trước khi tạo form thu thập";
                return View();

            }
            TempData["cpname"] = acc.CompanyName;
            TempData["cpcode"] = acc.CompanyCode + "_";
            TempData["cpaddress"] = acc.CompanyAddress;
            TempData["userid"] = acc.UserId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Formm(int userid, string cpname, string cpcode, string cpaddress, DateTime? createdDate, DateTime? expiredDate, string infor)
        {
            var account = GetUsername();
            if (account == false)
            {
                TempData["Error"] = "Vui lòng đăng nhập để tiếp tục";
                return RedirectToAction("Signin", "Account");
            }
            ViewBag.Username = HttpContext.Session.GetString("Username");
            Console.WriteLine($"[DEBUG] FormId: {cpcode}, UserId: {userid}");
            Console.WriteLine($"[DEBUG] Company date: {createdDate}");
            Console.WriteLine($"[DEBUG] Company date: {expiredDate}");
            Console.WriteLine($"[DEBUG] Created stirng t: {infor}");
            if (string.IsNullOrEmpty(cpcode) || string.IsNullOrEmpty(infor) || createdDate == null || expiredDate == null )
            {
                TempData["ErrorInfor"] = "Vui lòng điền đầy đủ thông tin.";
                return RedirectToAction(nameof(Formm));
            }
            var form = new Form
            {
                FormId = cpcode,
                CreateAt = createdDate.HasValue ? createdDate.Value : DateTime.Now,
                Due = expiredDate,
                Info = infor
            };
            var addFormResult = await _form.AddAsync(form, userid);
            if (addFormResult)
            {
                return RedirectToAction("Frame1", "FormColect" , new {formid= form.FormId});
            }

            TempData["Error"] = "Tạo form thất bại. Vui lòng kiểm tra lại mã tuyển dụng có thể đã tồn tại.";
            return RedirectToAction(nameof(Formm));
            
            
        }

        [HttpGet]
        public async Task<IActionResult> Service()
        {
            var account = GetUsername();
            if (account == false)
            {
                TempData["Error"] = "Vui lòng đăng nhập để tiếp tục";
                return RedirectToAction("Signin", "Account");
            }
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var services =  await _service.GetAllServicesAsync();
            if (services == null || services.Count == 0)
            {
                TempData["Error"] = "Không có dịch vụ nào được tìm thấy.";
                return View(new List<Service>());
            }
            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> FileExcel()
        {
            var account = GetUsername();
            if (account == false)
            {
                TempData["Error"] = "Vui lòng đăng nhập để tiếp tục";
                return RedirectToAction("Signin", "Account");
            }
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");
            var form = await _form.GetFormsByUserId(userId.Value);
            ViewBag.Forms = form;
            if (TempData["Data"] != null)
            {
                ViewBag.Data = JsonConvert.DeserializeObject<List<Student>>(
                                   TempData["Data"].ToString());
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReadForm(string? FormId)
        {
            var account = GetUsername();
            if (account == false)
            {
                TempData["Error"] = "Vui lòng đăng nhập để tiếp tục";
                return RedirectToAction("Signin", "Account");
            }
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");
            var form = await _form.GetFormsByUserId(userId.Value);
            ViewBag.Forms = form;
            var students = await _student.GetStudentsByFormId(FormId);
            TempData["Data"] = JsonConvert.SerializeObject(students);
            return RedirectToAction(nameof(FileExcel));
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
