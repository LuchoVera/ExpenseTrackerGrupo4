using System.Reflection;

using DbUp;

using Microsoft.Extensions.Options;
namespace ExpenseTrackerGrupo4.src.Database.Configuration
{
    public class DBInitializer : IDBInitializer
    {
        private readonly DBOptions _options;
        public DBInitializer(IOptions<DBOptions> options)
        {
            _options = options.Value;
        }
        public void InitializeDatabase()
        {
            EnsureDatabase.For.PostgresqlDatabase(_options.DefaultConnection);

            var dpUp = DeployChanges.To
            .PostgresqlDatabase(_options.DefaultConnection)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .WithTransaction()
            .LogToConsole()
            .Build();

            var result = dpUp.PerformUpgrade();

            if (!result.Successful)
            {
                Console.WriteLine("Invalid Migrations");
            }
        }
    }
}