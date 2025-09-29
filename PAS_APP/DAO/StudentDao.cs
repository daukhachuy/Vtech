using Microsoft.EntityFrameworkCore;
using PAS_APP.Models;

namespace PAS_APP.DAO
{
    public class StudentDao
    {
        private readonly DBContext.VtechDatabaseContext _context;

        public StudentDao(DBContext.VtechDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Student>>  GetAllByFormIdAsync(string? formId)
        {
            return await _context.Forms
                .Where(s => s.FormId == formId)
                .SelectMany(s => s.Students)
                .ToListAsync();
        }
    }
}
