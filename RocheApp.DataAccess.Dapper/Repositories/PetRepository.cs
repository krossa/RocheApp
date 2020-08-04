using Dapper;
using RocheApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RocheApp.DataAccess.Dapper.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly DataAccessSettings _settings;

        public PetRepository(DataAccessSettings settings)
        {
            _settings = settings;
        }

        public void Delete(Guid userId, IEnumerable<Guid> petIds)
        {
            const string query = @"
                DECLARE @PetIdsToDelete TABLE(PetId UNIQUEIDENTIFIER)
                BEGIN TRANSACTION
                EXEC sp_getapplock @Resource='DeletePets', @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout = 10000

                INSERT INTO @PetIdsToDelete
                SELECT p.PetId FROM [dbo].[Pet] as p
                INNER JOIN [dbo].[User] as u on p.UserId = u.UserId
                WHERE p.PetId IN @PetIds
                AND u.PetsDeleted = 0 

                DELETE [dbo].[Pet]
                WHERE PetId in (SELECT PetId FROM @PetIdsToDelete)

                UPDATE [dbo].[User]
                SET PetsDeleted = 1
                WHERE UserId = @UserId
                COMMIT";
            
            using IDbConnection db = new SqlConnection(_settings.ConnectionString);
            db.Execute(query, new {PetIds = petIds, UserId = userId});
        }
    }
}