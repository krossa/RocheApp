using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocheApp.DataAccess.Dapper;
using RocheApp.Database.DbUp;
using RocheApp.Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Xunit;

namespace RocheApp.Integration.Tests
{
    [Collection("Database")]
    public abstract class DbBaseTest : IDisposable
    {
        protected ServiceProvider ServiceProvider;
        protected readonly string ConnectionString;

        protected DbBaseTest()
        {
            RegisterServices();
            using var scope = ServiceProvider.CreateScope();
            ConnectionString = scope.ServiceProvider.GetService<DataAccessSettings>()?.ConnectionString;
            InitialDbMigration();
        }

        public void Dispose()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"DELETE FROM [dbo].[Pet]
                        DELETE FROM [dbo].[User]");
            ServiceProvider?.Dispose();
        }

        private void RegisterServices()
        {
            var config = LoadConfiguration();
            var services = new ServiceCollection();
            services.AddRocheAppDomain(config);
            services.AddDataAccessDapper(config);
            ServiceProvider = services.BuildServiceProvider(true);
        }

        private IConfiguration LoadConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

        private void InitialDbMigration()
        {
            var migrator = new DbUpSqlServerSchemaMigrator();
            migrator.Execute(ConnectionString);
            using IDbConnection db = new SqlConnection(ConnectionString);
            db.Execute(@"
                DELETE FROM [dbo].[Pet]
                DELETE FROM [dbo].[User]");
        }
    }
}