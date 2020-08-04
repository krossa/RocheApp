using Dapper;
using Microsoft.Extensions.DependencyInjection;
using RocheApp.Domain.Services.User;
using RocheApp.Domain.Services.User.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RocheApp.Integration.Tests
{
    public class UpdateTests : DbBaseTest
    {
        [Fact]
        public async Task Returns_Correct_Result()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111111-b9ca-4cac-9c32-06a46179ecf3', N'Jon', N'Doe', 0, 0, 1000)
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111112-b9ca-4cac-9c32-06a46179ecf3', N'Ron', N'Tom', 0, 0, 100)");
            using var scope = ServiceProvider.CreateScope();
            var updateService = scope.ServiceProvider.GetService<IUserUpdater>();

            var result =  await updateService.UpdateAsync(1).ToListAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal(1001, result.ElementAt(0).ExperiencePoints);
            Assert.Equal(8, result.ElementAt(0).RowVersion.Length);
            Assert.Equal(102, result.ElementAt(1).ExperiencePoints);
            Assert.Equal(8, result.ElementAt(1).RowVersion.Length);
        }

        [Fact]
        public async Task Deletes_Equal_Half_Of_Pets()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111113-b9ca-4cac-9c32-06a46179ecf3', N'Jon', N'Doe', 0, 0, 1000)
                
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111111-ec78-424b-8c26-00fc44f53583', N'Hugo',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111112-ec78-424b-8c26-00fc44f53583', N'Hektor', N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111113-ec78-424b-8c26-00fc44f53583', N'Roki',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111114-ec78-424b-8c26-00fc44f53583', N'Misza',  N'11111113-b9ca-4cac-9c32-06a46179ecf3')");
            using var scope = ServiceProvider.CreateScope();
            var updateService = scope.ServiceProvider.GetService<IUserUpdater>();
            var userService = scope.ServiceProvider.GetService<IUserService>();
        
            _ = await updateService.UpdateAsync(1).ToListAsync();
            var result = await userService.UsersAsync(UserFilter.EmptyFilter);
        
            Assert.Equal(2, result.TotalPetCount);
        }
        
        [Fact]
        public async Task Deletes_Smaller_Half_Of_Pets()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111113-b9ca-4cac-9c32-06a46179ecf3', N'Jon', N'Doe', 0, 0, 1000)
                
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111111-ec78-424b-8c26-00fc44f53583', N'Hugo',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111112-ec78-424b-8c26-00fc44f53583', N'Hektor', N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111113-ec78-424b-8c26-00fc44f53583', N'Roki',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111114-ec78-424b-8c26-00fc44f53583', N'Misza',  N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111115-ec78-424b-8c26-00fc44f53583', N'Isza',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')");
            using var scope = ServiceProvider.CreateScope();
            var updateService = scope.ServiceProvider.GetService<IUserUpdater>();
            var userService = scope.ServiceProvider.GetService<IUserService>();
        
            _ = await updateService.UpdateAsync(1).ToListAsync();
            var result = await userService.UsersAsync(UserFilter.EmptyFilter);
        
            Assert.Equal(3, result.TotalPetCount);
        }
        
        [Fact]
        public async Task Does_Not_Delete_Pets_When_User_Has_Only_One()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111113-b9ca-4cac-9c32-06a46179ecf3', N'Jon', N'Doe', 0, 0, 1000)
                
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111111-ec78-424b-8c26-00fc44f53583', N'Hugo',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')");
            using var scope = ServiceProvider.CreateScope();
            var updateService = scope.ServiceProvider.GetService<IUserUpdater>();
            var userService = scope.ServiceProvider.GetService<IUserService>();
        
            _ = await updateService.UpdateAsync(1).ToListAsync();
            var result = await userService.UsersAsync(UserFilter.EmptyFilter);
        
            Assert.Equal(1, result.TotalPetCount);
        }
        
        [Fact]
        public async Task Does_Not_Delete_Pets_After_Reaching_1000_Points()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111113-b9ca-4cac-9c32-06a46179ecf3', N'Jon', N'Doe', 0, 0, 999)
                
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111111-ec78-424b-8c26-00fc44f53583', N'Hugo',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111112-ec78-424b-8c26-00fc44f53583', N'Hektor', N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111113-ec78-424b-8c26-00fc44f53583', N'Roki',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111114-ec78-424b-8c26-00fc44f53583', N'Misza',  N'11111113-b9ca-4cac-9c32-06a46179ecf3')");
            using var scope = ServiceProvider.CreateScope();
            var updateService = scope.ServiceProvider.GetService<IUserUpdater>();
            var userService = scope.ServiceProvider.GetService<IUserService>();
        
            _ = await updateService.UpdateAsync(1).ToListAsync();
            var result = await userService.UsersAsync(UserFilter.EmptyFilter);
        
            Assert.Equal(4, result.TotalPetCount);
            Assert.Equal(1000, result.Users.First().ExperiencePoints);
        }
        
        [Fact]
        public async Task Delete_Pets_After_Reaching_1001_Points()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111113-b9ca-4cac-9c32-06a46179ecf3', N'Jon', N'Doe', 0, 0, 1000)
                
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111111-ec78-424b-8c26-00fc44f53583', N'Hugo',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111112-ec78-424b-8c26-00fc44f53583', N'Hektor', N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111113-ec78-424b-8c26-00fc44f53583', N'Roki',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111114-ec78-424b-8c26-00fc44f53583', N'Misza',  N'11111113-b9ca-4cac-9c32-06a46179ecf3')");
            using var scope = ServiceProvider.CreateScope();
            var updateService = scope.ServiceProvider.GetService<IUserUpdater>();
            var userService = scope.ServiceProvider.GetService<IUserService>();
        
            _ = await updateService.UpdateAsync(1).ToListAsync();
            var result = await userService.UsersAsync(UserFilter.EmptyFilter);
        
            Assert.Equal(2, result.TotalPetCount);
            Assert.Equal(1001, result.Users.First().ExperiencePoints);
        }
        
        [Fact]
        public async Task Does_Not_Delete_Pets_Twice()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111113-b9ca-4cac-9c32-06a46179ecf3', N'Jon', N'Doe', 0, 1, 1001)
                
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111111-ec78-424b-8c26-00fc44f53583', N'Hugo',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111112-ec78-424b-8c26-00fc44f53583', N'Hektor', N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111113-ec78-424b-8c26-00fc44f53583', N'Roki',   N'11111113-b9ca-4cac-9c32-06a46179ecf3')
                INSERT [dbo].[Pet] ([PetId], [Name], [UserId]) VALUES (N'11111114-ec78-424b-8c26-00fc44f53583', N'Misza',  N'11111113-b9ca-4cac-9c32-06a46179ecf3')");
            using var scope = ServiceProvider.CreateScope();
            var updateService = scope.ServiceProvider.GetService<IUserUpdater>();
            var userService = scope.ServiceProvider.GetService<IUserService>();
        
            _ = await updateService.UpdateAsync(1).ToListAsync();
            var result = await userService.UsersAsync(UserFilter.EmptyFilter);
        
            Assert.Equal(4, result.TotalPetCount);
        }
        
        [Theory]
        [InlineData(1, 1001, 102, 3)]
        [InlineData(2, 1003, 106, 9)]
        [InlineData(3, 1006, 112, 18)]
        [InlineData(4, 1010, 120, 30)]
        [InlineData(5, 1015, 130, 45)]
        [InlineData(6, 1021, 142, 63)]
        [InlineData(7, 1028, 156, 84)]
        [InlineData(8, 1036, 172, 108)]
        [InlineData(9, 1045, 190, 135)]
        [InlineData(10, 1055, 210, 165)]
        public async Task Updates_Points_Correctly(int count, int jon, int ron, int tim)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111111-b9ca-4cac-9c32-06a46179ecf3', N'Jon', N'Doe', 0, 0, 1000)
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111112-b9ca-4cac-9c32-06a46179ecf3', N'Ron', N'Son', 0, 0, 100)
                INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) VALUES (N'11111113-b9ca-4cac-9c32-06a46179ecf3', N'Tim', N'Tom', 0, 0, 0)");
            using var scope = ServiceProvider.CreateScope();
            var updateService = scope.ServiceProvider.GetService<IUserUpdater>();
        
            var result = await updateService.UpdateAsync(count).ToListAsync();
        
            Assert.Equal(jon, result.ElementAt(0).ExperiencePoints);
            Assert.Equal(ron, result.ElementAt(1).ExperiencePoints);
            Assert.Equal(tim, result.ElementAt(2).ExperiencePoints);
        }
    }
}