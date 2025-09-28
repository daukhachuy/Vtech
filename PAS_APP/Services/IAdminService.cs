namespace PAS_APP.Services
{
    public interface IAdminService
    {
        bool Validate(string username, string password);
    }
}
