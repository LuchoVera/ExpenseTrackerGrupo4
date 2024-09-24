using ExpenseTrackerGrupo4.src.Database.Configuration;
namespace ExpenseTrackerGrupo4.src.Utils
{
    public static class WebApplicationDB
    {
        public static WebApplication InitializeDB(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();

                dbInitializer.InitializeDatabase();

                return app;
            }
        }
    }
}
