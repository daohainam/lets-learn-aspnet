using OidcServer.Models;

namespace OidcServer.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users =
        [
            new() { Name = "alice" },
            new() { Name = "bob" },
            new() { Name = "nam" }
        ];

        public User? FindByUsername(string username)
        {
            return _users.FirstOrDefault(x => x.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
