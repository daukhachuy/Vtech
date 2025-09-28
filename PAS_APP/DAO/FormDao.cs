using Microsoft.EntityFrameworkCore;
using PAS_APP.Models;

namespace PAS_APP.DAO
{
    public class FormDao
    {
        private readonly DBContext.VtechDatabaseContext _context;
        public FormDao(DBContext.VtechDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Form>> GetAllAsync()
        {
            return await _context.Forms.ToListAsync();
        }

        public async Task<bool> AddAsync(Form forms, int userid)
        {
            var existingForm = await _context.Forms
                .FirstOrDefaultAsync(f => f.FormId == forms.FormId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userid);
            if (existingForm == null && user != null)
            {
                _context.Forms.Add(forms);
                user.Forms.Add(forms);
                await _context.SaveChangesAsync();
                return true;
            }

            Console.WriteLine("Form with the same FormId already exists or User not found.");
            return false;
        }



        public async Task<Form?> GetByIdAsync(string id)
        {
            return await _context.Forms
                         .FirstOrDefaultAsync(f => f.FormId == id);
        }
        public async Task<List<Form>> GetFormsByUserId(int userId)
        {
            return await _context.Users
                .Where(uf => uf.UserId == userId)
                .SelectMany(uf => uf.Forms)
                .ToListAsync();
        }
    }
}
