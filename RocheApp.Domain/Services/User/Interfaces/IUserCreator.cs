using System.Threading.Tasks;

namespace RocheApp.Domain.Services.User.Interfaces
{
    public interface IUserCreator
    {
        Task<UserCreatorResult> CreateAsync(Models.User user);
    }
}