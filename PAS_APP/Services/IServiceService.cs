namespace PAS_APP.Services
{
    public interface IServiceService
    {
        Task<List<Models.Service?>> GetAllServicesAsync();
    }
}
