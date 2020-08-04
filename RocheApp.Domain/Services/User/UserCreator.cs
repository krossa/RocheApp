using RocheApp.Domain.Repositories;
using RocheApp.Domain.Services.User.Interfaces;
using System.Threading.Tasks;

namespace RocheApp.Domain.Services.User
{
    public class UserCreator : IUserCreator
    {
        private readonly IUserRepository _userRepository;

        public UserCreator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserCreatorResult> CreateAsync(Models.User user) =>
            await _userRepository.CreateAsync(user);
    }
}