using RocheApp.Domain.Repositories;
using RocheApp.Domain.Services.User.Interfaces;

namespace RocheApp.Domain.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserResult Users(UserFilter filter) => _userRepository.Users(filter);
    }
}