namespace PAS_APP.Services
{
    public class ServiceService : IServiceService
    {
        private readonly DAO.ServiceDao _serviceDao;
        public ServiceService(DAO.ServiceDao serviceDao)
        {
            _serviceDao = serviceDao;
        }
        public async Task<List<Models.Service?>> GetAllServicesAsync()
        {
            return await _serviceDao.GetAllAsync();
        }
    }
}
