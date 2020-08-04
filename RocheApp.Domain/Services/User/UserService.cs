using RocheApp.Domain.Repositories;
using RocheApp.Domain.Services.User.Interfaces;
using System.Threading.Tasks;

namespace RocheApp.Domain.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResult> UsersAsync(UserFilter filter) => await _userRepository.UsersAsync(filter);
    }
}