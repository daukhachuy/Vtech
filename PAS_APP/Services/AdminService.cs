using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace PAS_APP.Services
{
    public class AdminService : IAdminService
    {

        private readonly IPasswordHasher<object> _hasher;
        private readonly string _path;

        private class AdminModel { public string Username { get; set; } = ""; public string PasswordHash { get; set; } = ""; }

        public AdminService(IWebHostEnvironment env, IPasswordHasher<object> hasher)
        {
            _hasher = hasher;
            _path = Path.Combine(env.ContentRootPath, "Models", "Admin.json");
        }

        public bool Validate(string username, string password)
        {
            var json = File.ReadAllText(_path);
            var admin = JsonSerializer.Deserialize<AdminModel>(json);
            if (admin == null || admin.Username != username) return false;

            var result = _hasher.VerifyHashedPassword(new object(), admin.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
