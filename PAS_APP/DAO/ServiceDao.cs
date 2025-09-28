using Microsoft.EntityFrameworkCore;
using PAS_APP.DBContext;
using PAS_APP.Models;

namespace PAS_APP.DAO
{
    public class ServiceDao
    {
        private readonly VtechDatabaseContext _context;

        public ServiceDao(VtechDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Service?>> GetAllAsync()
        {
            return await _context.Services.ToListAsync(); 
        }
    }
}
