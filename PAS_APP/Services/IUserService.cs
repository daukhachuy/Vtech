using PAS_APP.Models;

namespace PAS_APP.Services
{
    public interface IUserService
    {
        Task<User?> ValidateUserAsync(string email, string password);

        Task<User?> GetUserByEmail(string email);
        User? Checkemail(string email);
        Task<bool> UpdateProfile(User user);
    }
}
