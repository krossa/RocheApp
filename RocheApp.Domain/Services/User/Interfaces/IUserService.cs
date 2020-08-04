using System.Threading.Tasks;

namespace RocheApp.Domain.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<UserResult> UsersAsync(UserFilter filter);
    }
}