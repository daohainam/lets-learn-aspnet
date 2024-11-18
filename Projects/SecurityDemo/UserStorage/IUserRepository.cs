using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage
{
    public interface IUserRepository
    {
        public Task<User?> FindByUserNameAsync(string userName);
        public Task SaveAsync(User user);
    }
}
