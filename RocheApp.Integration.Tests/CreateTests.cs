using Microsoft.Extensions.DependencyInjection;
using RocheApp.Domain.Models;
using RocheApp.Domain.Services.User;
using RocheApp.Domain.Services.User.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RocheApp.Integration.Tests
{
    public class CreateTests : DbBaseTest
    {
        [Fact]
        public async Task Returns_Correct_Result()
        {
            using var scope = ServiceProvider.CreateScope();
            var createService = scope.ServiceProvider.GetService<IUserCreator>();
            var id = Guid.NewGuid();
            var user = new User {UserId = id};

            var result = await createService.CreateAsync(user);

            Assert.Equal(id, result.UserId);
            Assert.Equal(8, result.RowVersion.Length);
        }

        [Fact]
        public async Task Creates_Record()
        {
            using var scope = ServiceProvider.CreateScope();
            var createService = scope.ServiceProvider.GetService<IUserCreator>();
            var userService = scope.ServiceProvider.GetService<IUserService>();
            var user = new User();

            await createService.CreateAsync(user);
            var result = await userService.UsersAsync(UserFilter.EmptyFilter);

            Assert.Equal(1, result.TotalUserCount);
            Assert.Equal(0, result.TotalPetCount);
            Assert.Single(result.Users);
        }

        [Fact]
        public async Task Creates_Correct_Record()
        {
            using var scope = ServiceProvider.CreateScope();
            var createService = scope.ServiceProvider.GetService<IUserCreator>();
            var userService = scope.ServiceProvider.GetService<IUserService>();
            var id = Guid.NewGuid();
            var user = new User
            {
                UserId = id,
                FirstName = "Jane",
                LastName = "Doe",
                Status = 1,
                PetsDeleted = 0,
                ExperiencePoints = 137
            };

            await createService.CreateAsync(user);
            var result = await userService.UsersAsync(UserFilter.EmptyFilter);

            var createdUser = result.Users.First();
            Assert.Equal(id, createdUser.UserId);
            Assert.Equal("Jane", createdUser.FirstName);
            Assert.Equal("Doe", createdUser.LastName);
            Assert.Equal(1, createdUser.Status);
            Assert.Equal(0, createdUser.PetsDeleted);
            Assert.Equal(137, createdUser.ExperiencePoints);
        }
    }
}