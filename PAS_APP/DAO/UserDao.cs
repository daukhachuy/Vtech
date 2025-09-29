using Microsoft.EntityFrameworkCore;
using PAS_APP.DBContext;
using PAS_APP.Models;

namespace PAS_APP.DAO
{
    public class UserDao
    {
        private readonly VtechDatabaseContext _context;

        public UserDao(VtechDatabaseContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                         .FirstOrDefaultAsync(u => u.Email == email && u.Status);
        }

        public async Task<User?> GetPackageAsync(int userId)
        {
            return await _context.Users
                         .Include(u => u.Package)
                         .FirstOrDefaultAsync(u => u.UserId == userId && u.Status);
        }

        public User? Checkemail(string email)
        {
            return  _context.Users
                         .FirstOrDefault(u => u.Email == email && u.Status);
        }

        public async Task<bool> update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Dob = user.Dob;
                existingUser.Phone = user.Phone;
                existingUser.PassWord = user.PassWord;
                existingUser.CompanyName = user.CompanyName;
                existingUser.CompanyAddress = user.CompanyAddress;
                existingUser.CompanyCode = user.CompanyCode;
                await _context.SaveChangesAsync();
                return true;
            }  
            return false;
        }


        public async Task<User> GetUserByFormIdAsync(string formId)
        {
            var user = await _context.Users
                .Include(u => u.Forms)
                .FirstOrDefaultAsync(u => u.Forms.Any(f => f.FormId == formId));
            return user!;
        }

    }
}
