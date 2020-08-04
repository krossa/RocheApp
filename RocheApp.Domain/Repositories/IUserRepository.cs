using RocheApp.Domain.Models;
using RocheApp.Domain.Services.User;

namespace RocheApp.Domain.Repositories
{
    public interface IUserRepository
    {
        UserCreatorResult Create(User user);
        
        void Update(int multiplier);
        
        UserResult Users(UserFilter filter);
    }
}
