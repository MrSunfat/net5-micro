using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Runtime.CompilerServices;

namespace Discount.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryValue = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                var connectionString = config.GetValue<string>("DatabaseSettings:ConnectionStrings");

                try
                {
                    logger.LogInformation("Migrating Postgresql database!");
                    //using var connection = new NpgsqlConnection(connectionString);
                    //connection.Open();

                    //using var command = new NpgsqlCommand()
                    //{
                    //    Connection = connection,
                    //};

                    //command.CommandText = "DROP TABLE IF EXISTS Coupon;";
                    //command.ExecuteNonQuery();

                    logger.LogInformation("Migrated Postgresql database!");
                }
                catch (NpgsqlException e)
                {
                    logger.LogError(e, "An error occurred while migrating!");
                    if (retryValue < 50)
                    {
                        retryValue++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryValue);
                    }
                }
            }

            return host;
        }
    }
}
