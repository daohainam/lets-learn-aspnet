using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UserStorage
{
    public class JsonFileUserRepository: IUserRepository
    {
        private readonly string _dbPath;
        public JsonFileUserRepository(IOptions<JsonFileUserRepositoryOptions> options)
        {
            _dbPath = options.Value.DatabasePath;

            if (!Directory.Exists(_dbPath)) { 
                Directory.CreateDirectory(_dbPath);
            }
        }

        public async Task<User?> FindByUserNameAsync(string userName)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));
            if (!IsValidUserName(userName))
            {
                throw new ArgumentException("Invalid user name", nameof(userName));
            }
            
            var fileInfo = new FileInfo(Path.Combine(_dbPath, userName));
            if (!fileInfo.Exists)
            {
                return null;
            }

            var json = await File.ReadAllTextAsync(fileInfo.FullName);
            return JsonSerializer.Deserialize<User>(json);
        }

        public Task SaveAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            if (!IsValidUserName(user.UserName))
            {
                throw new ArgumentException("Invalid user name", nameof(user));
            }

            var fileInfo = new FileInfo(Path.Combine(_dbPath, user.UserName));
            var json = JsonSerializer.Serialize(user);

            return File.WriteAllTextAsync(fileInfo.FullName, json);
        }

        private static bool IsValidUserName(string userName)
        {
            return userName.All((c) => char.IsAsciiLetterOrDigit(c) || c == '-');
        }
    }
}
