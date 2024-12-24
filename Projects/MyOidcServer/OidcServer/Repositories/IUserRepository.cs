using OidcServer.Models;

namespace OidcServer.Repositories
{
    public interface IUserRepository
    {
        User? FindByUsername(string username);
    }
}
