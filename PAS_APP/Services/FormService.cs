namespace PAS_APP.Services
{
    public class FormService : IFormService
    {
        private readonly DAO.FormDao _formDao;
        private readonly IUserService _userService;
        public FormService(DAO.FormDao formDao, IUserService userService)
        {
            _formDao = formDao;
            _userService = userService;
        }
        public async Task<List<Models.Form>> GetAllAsync()
        {
            return await _formDao.GetAllAsync();
        }
        public async Task<bool> AddAsync(Models.Form form, int userid)
        {
                return await _formDao.AddAsync(form, userid);
        }
        public async Task<Models.Form?> GetByIdAsync(string id)
        {
            return await _formDao.GetByIdAsync(id);
        }
        public async Task<List<Models.Form>> GetFormsByUserId(int userId)
        {
            return await _formDao.GetFormsByUserId(userId);
        }
    }
}
