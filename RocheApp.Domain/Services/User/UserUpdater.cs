using RocheApp.Domain.Repositories;
using RocheApp.Domain.Services.Pet.Interfaces;
using RocheApp.Domain.Services.User.Interfaces;
using System.Collections.Generic;

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

        public IEnumerable<UserUpdateResult> Update(int count)
        {
            for (var i = 1; i <= count; i++)
            {
                _userRepository.Update(i);
            }

            var users = _userService.Users(UserFilter.EmptyFilter);
            foreach (var user in users.Users)
            {
                _petDeleter.Delete(user);
                yield return new UserUpdateResult
                    {ExperiencePoints = user.ExperiencePoints, RowVersion = user.RowVersion};
            }
        }
    }
}