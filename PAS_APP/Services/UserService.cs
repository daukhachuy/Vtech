using Microsoft.AspNetCore.Identity;
using PAS_APP.DAO;
using PAS_APP.Models;

namespace PAS_APP.Services
{
    public class UserService : IUserService
    {
        private readonly UserDao _userDao;
        private readonly IPasswordHasher<User> _hasher;
        public UserService(UserDao userDao, IPasswordHasher<User> hasher)
        {
            _userDao = userDao;
            _hasher = hasher;
        }

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var user = await _userDao.GetByEmailAsync(email);
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PassWord!, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userDao.GetByEmailAsync(email);
           
        }

        public User? Checkemail(string email)
        {
            return _userDao.Checkemail(email);
        }

        public async Task<bool> UpdateProfile(User user)
        {

           return await _userDao.update(user);
        }


        public async Task<User?> GetPackageAsync(int userId)
        {
            return await _userDao.GetPackageAsync(userId);
        }


        public async Task<User> GetUserByFormIdAsync(string formId)
        {
            return await _userDao.GetUserByFormIdAsync(formId);
        }
    }
}
