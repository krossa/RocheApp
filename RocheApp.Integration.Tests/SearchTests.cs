using Dapper;
using Microsoft.Extensions.DependencyInjection;
using RocheApp.Domain.Services.User;
using RocheApp.Domain.Services.User.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RocheApp.Integration.Tests
{
    public class SearchTests : DbBaseTest
    {
        public SearchTests()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111111-b9ca-4cac-9c32-06a46179ecf3', N'Tom',   N'Ron', 1, 0, 10)
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111112-b9ca-4cac-9c32-06a46179ecf3', N'Tommy', N'Gun', 0, 0, 100)
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111113-b9ca-4cac-9c32-06a46179ecf3', N'Jon',   N'Doe', 0, 0, 1000)

                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111111-ec78-424b-8c26-00fc44f53583', N'Hugo',   N'11111111-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111112-ec78-424b-8c26-00fc44f53583', N'Hektor', N'11111112-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111113-ec78-424b-8c26-00fc44f53583', N'Roki',   N'11111112-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111114-ec78-424b-8c26-00fc44f53583', N'Misza',  N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111115-ec78-424b-8c26-00fc44f53583', N'Diego',  N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111116-ec78-424b-8c26-00fc44f53583', N'Leo',    N'11111113-b9ca-4cac-9c32-06a46179ecf3')");
        }

        [Fact]
        public async Task Gets_All_Records()
        {
            using var scope = ServiceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            var result = await userService.UsersAsync(UserFilter.EmptyFilter);

            Assert.Equal(3, result.TotalUserCount);
            Assert.Equal(6, result.TotalPetCount);
            Assert.Equal(3, result.Users.Count());
            Assert.Equal(1, result.Users.ElementAt(0).Pets.Count);
            Assert.Equal(2, result.Users.ElementAt(1).Pets.Count);
            Assert.Equal(3, result.Users.ElementAt(2).Pets.Count);
        }

        [Fact]
        public async Task Gets_Correct_Data()
        {
            using var scope = ServiceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            var result = await userService.UsersAsync(UserFilter.EmptyFilter);

            var user = result.Users.First();
            Assert.Equal(Guid.Parse("11111111-b9ca-4cac-9c32-06a46179ecf3"), user.UserId);
            Assert.Equal("Tom", user.FirstName);
            Assert.Equal("Ron", user.LastName);
            Assert.Equal(1, user.Status);
            Assert.Equal(0, user.PetsDeleted);
            Assert.Equal(10, user.ExperiencePoints);

            var pet = user.Pets.First();
            Assert.Equal(Guid.Parse("11111111-ec78-424b-8c26-00fc44f53583"), pet.PetId);
            Assert.Equal("Hugo", pet.Name);
        }

        [Fact]
        public async Task Filters_Records_On_FirstName()
        {
            using var scope = ServiceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            var result = await userService.UsersAsync(new UserFilter {FirstName = "om"});
            Assert.Equal(2, result.TotalUserCount);
            Assert.Equal(3, result.TotalPetCount);
            Assert.Equal(2, result.Users.Count());
            Assert.Equal(Guid.Parse("11111111-b9ca-4cac-9c32-06a46179ecf3"), result.Users.ElementAt(0).UserId);
            Assert.Equal(Guid.Parse("11111112-b9ca-4cac-9c32-06a46179ecf3"), result.Users.ElementAt(1).UserId);
        }

        [Fact]
        public async Task Filters_Records_On_Status()
        {
            using var scope = ServiceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            var result = await userService.UsersAsync(new UserFilter {Status = 1});
            Assert.Equal(1, result.TotalUserCount);
            Assert.Equal(1, result.TotalPetCount);
            Assert.Single(result.Users);
            Assert.Equal(Guid.Parse("11111111-b9ca-4cac-9c32-06a46179ecf3"), result.Users.First().UserId);
        }

        [Fact]
        public async Task Filters_Records_On_FirstName_And_Status()
        {
            using var scope = ServiceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            var result = await userService.UsersAsync(new UserFilter {FirstName = "om", Status = 1});
            Assert.Equal(1, result.TotalUserCount);
            Assert.Equal(1, result.TotalPetCount);
            Assert.Single(result.Users);
            Assert.Equal(Guid.Parse("11111111-b9ca-4cac-9c32-06a46179ecf3"), result.Users.First().UserId);
        }
    }
}