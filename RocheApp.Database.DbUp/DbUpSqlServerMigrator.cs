using System;
using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Helpers;

namespace RocheApp.Database.DbUp
{
    public class DbUpSqlServerMigrator : IMigrator
    {
        public void Execute(string connectionString)
        {
            EnsureDatabase.For.SqlDatabase(connectionString);

            EnsureJournalSchema(connectionString);

            var result = DeployChanges.To
              .SqlDatabase(connectionString)
              .JournalToSqlTable("DbUp", "SchemaVersions")
              .WithScriptsEmbeddedInAssembly(typeof(IMigrator).Assembly)
              .LogToConsole()
              .WithTransactionPerScript()
              .WithExecutionTimeout(TimeSpan.FromMinutes(10))
              .Build()
              .PerformUpgrade();

            HandleResult(result);
        }

        private void EnsureJournalSchema(string connectionString) =>
            DeployChanges.To
              .SqlDatabase(connectionString)
              .JournalTo(new NullJournal())
              .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
              .LogToConsole()
              .WithTransactionPerScript()
              .Build()
              .PerformUpgrade();

        private void HandleResult(DatabaseUpgradeResult result)
        {
            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return;
        }

    }
}
