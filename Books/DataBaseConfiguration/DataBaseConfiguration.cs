using Books.InterfaceAdapter.Layer;
using Microsoft.EntityFrameworkCore;
using Books.EnterpriseBusiness.Layer.Constants;

namespace BooksPresentation.DataBaseConfiguration
{
    public class DataBaseConfiguration
    {
        public static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            services.AddDbContext<AppDbConext>(options =>
            {

                if (environment == "Development")
                    options.UseNpgsql(
                        configuration.GetConnectionString(Constants.DEV_CONNECTION),
                        b => b.MigrationsAssembly("Books.InterfaceAdapter.Layer"));

                else if (environment == "Production")
                    options.UseNpgsql(
                        configuration.GetConnectionString(Constants.DEV_CONNECTION),
                        b => b.MigrationsAssembly("Books.InterfaceAdapter.Layer"));
                else
                    options.UseNpgsql(
                        configuration.GetConnectionString(Constants.DEV_CONNECTION),
                        b => b.MigrationsAssembly("Books.InterfaceAdapter.Layer"));

            });
        }
    }
}
