using RocheApp.Domain.Repositories;
using RocheApp.Domain.Services.User.Interfaces;

namespace RocheApp.Domain.Services.User
{
    public class UserCreator : IUserCreator
    {
        private readonly IUserRepository _userRepository;

        public UserCreator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserCreatorResult Create(Models.User user) =>
            _userRepository.Create(user);
    }
}