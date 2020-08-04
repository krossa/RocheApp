using RocheApp.Domain.Repositories;
using RocheApp.Domain.Services.Pet.Interfaces;
using RocheApp.Domain.Services.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RocheApp.Domain.Services.User
{
    public class UserUpdater : IUserUpdater
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IPetDeleter _petDeleter;

        public UserUpdater(
            IUserRepository userRepository,
            IUserService userService,
            IPetDeleter petDeleter
        )
        {
            _userRepository = userRepository;
            _userService = userService;
            _petDeleter = petDeleter;
        }

        public async IAsyncEnumerable<UserUpdateResult> UpdateAsync(int count)
        {
            for (var i = 1; i <= count; i++)
            {
                await _userRepository.UpdateAsync(i);
            }

            var users = await _userService.UsersAsync(UserFilter.EmptyFilter);
            foreach (var user in users.Users)
            {
                await _petDeleter.DeleteAsync(user);
                yield return new UserUpdateResult
                    {ExperiencePoints = user.ExperiencePoints, RowVersion = user.RowVersion};
            }
        }
    }
}