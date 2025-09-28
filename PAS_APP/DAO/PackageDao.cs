using Microsoft.EntityFrameworkCore;

namespace PAS_APP.DAO
{
    public class PackageDao
    {
        private readonly DBContext.VtechDatabaseContext _context;
        public PackageDao(DBContext.VtechDatabaseContext context)
        {
            _context = context;
        }
        public async  Task<List<Models.Package>> GetAllAsync()
        {
            return await _context.Packages.ToListAsync();
        }
    }
}
