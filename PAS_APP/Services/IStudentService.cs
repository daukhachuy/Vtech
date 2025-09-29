using PAS_APP.Models;

namespace PAS_APP.Services
{
    public interface IStudentService
    {

        // Add the missing method definition to fix the error
        Task<List<Student>> GetStudentsByFormId(string? formId);
    }
}
