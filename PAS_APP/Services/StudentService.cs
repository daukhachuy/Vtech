using PAS_APP.Models;

namespace PAS_APP.Services
{
    public class StudentService : IStudentService

    {
        private readonly DAO.StudentDao _studentDao;
        public StudentService(DAO.StudentDao studentDao)
        {
            _studentDao = studentDao;
        }
        public async Task<List<Student>> GetStudentsByFormId(string? formId)
        {
            return await _studentDao.GetAllByFormIdAsync(formId);
        }
    }
}
