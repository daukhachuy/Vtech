using PAS_APP.Models;

namespace PAS_APP.Services
{
    public interface IFormService
    {
        Task<List<Models.Form>> GetAllAsync();

        Task<bool> AddAsync(Form form , int userid);
        Task<Form?> GetByIdAsync(string id);
        Task<List<Form>> GetFormsByUserId(int userId);
    }
}
