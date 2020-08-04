using RocheApp.Domain.Models;
using RocheApp.Domain.Services.User;
using System.Threading.Tasks;

namespace RocheApp.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserCreatorResult> CreateAsync(User user);

        Task UpdateAsync(int multiplier);

        Task<UserResult> UsersAsync(UserFilter filter);
    }
}