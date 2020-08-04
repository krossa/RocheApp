using Dapper;
using RocheApp.Domain.Models;
using RocheApp.Domain.Repositories;
using RocheApp.Domain.Services.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocheApp.DataAccess.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataAccessSettings _settings;

        public UserRepository(DataAccessSettings settings)
        {
            _settings = settings;
        }

        public async Task<UserCreatorResult> CreateAsync(User user)
        {
            const string query = @"INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Status], [PetsDeleted], [ExperiencePoints]) 
                        OUTPUT INSERTED.[RowVersion]
                        VALUES (@UserId, @FirstName, @LastName, @Status, @PetsDeleted, @ExperiencePoints)";

            using IDbConnection db = new SqlConnection(_settings.ConnectionString);
            var rowVersion = await db.QueryAsync<byte[]>(query, user);
            return new UserCreatorResult
            {
                UserId = user.UserId,
                RowVersion = rowVersion.First()
            };
        }

        public async Task UpdateAsync(int multiplier)
        {
            const string query = @"UPDATE x
                        SET x.ExperiencePoints = (x.order_value * @Multiplier) + x.ExperiencePoints
                        FROM (
                        SELECT ExperiencePoints, ROW_NUMBER() OVER (ORDER BY [RowVersion]) AS order_value 
                        FROM [dbo].[User]
                        ) x;";

            using IDbConnection db = new SqlConnection(_settings.ConnectionString);
            await db.ExecuteAsync(query, new {Multiplier = multiplier});
        }
        
        public async Task<UserResult> UsersAsync(UserFilter filter)
        {
            var query = new StringBuilder();
            query.Append(@"SELECT * FROM [dbo].[User] as u
                        LEFT JOIN [dbo].[Pet] as p on u.UserId = p.UserId");
            if (filter.HasAnyValues)
            {
                query.Append(" WHERE 1=1");
                if (filter.HasFirstNameValue)
                    query.Append(" AND u.FirstName LIKE @FirstName");
                if (filter.HasStatusValue)
                    query.Append(" AND u.Status = @Status");
            }

            query.Append(" ORDER BY u.RowVersion");


            using IDbConnection db = new SqlConnection(_settings.ConnectionString);
            var userDict = new Dictionary<Guid, User>();
            var userResult = new UserResult();
            userResult.Users = (await db.QueryAsync<User, Pet, User>(query.ToString(),
                    (user, pet) =>
                    {
                        if (!userDict.TryGetValue(user.UserId, out var userEntry))
                        {
                            userResult.TotalUserCount++;
                            userEntry = user;
                            userEntry.Pets = new List<Pet>();
                            userDict.Add(userEntry.UserId, userEntry);
                        }

                        if (pet is null) return userEntry;
                        
                        userResult.TotalPetCount++;
                        userEntry.Pets.Add(pet);
                        return userEntry;
                    },
                    filter,
                    splitOn: "PetId"))
                .Distinct();

            return userResult;
        }
    }
}