using DbUp;
using DbUp.Engine.Output;
using DbUp.Helpers;
using System;
using System.Reflection;

namespace RocheApp.Database.DbUp
{
    public class DbUpSqlServerSchemaMigrator : IMigrator
    {
        public void Execute(string connectionString)
        {
            EnsureDatabase.For.SqlDatabase(connectionString, new NoOpUpgradeLog());

            EnsureJournalSchema(connectionString);

            DeployChanges.To
                .SqlDatabase(connectionString)
                .JournalToSqlTable("DbUp", "SchemaVersions")
                .WithScriptsEmbeddedInAssembly(typeof(IMigrator).Assembly, s => !s.Contains("initial_data"))
                .LogToNowhere()
                .WithTransactionPerScript()
                .WithExecutionTimeout(TimeSpan.FromMinutes(10))
                .Build()
                .PerformUpgrade();
        }

        private void EnsureJournalSchema(string connectionString) =>
            DeployChanges.To
                .SqlDatabase(connectionString)
                .JournalTo(new NullJournal())
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToNowhere()
                .WithTransactionPerScript()
                .Build()
                .PerformUpgrade();
    }
}