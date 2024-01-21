using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Helper;
public static partial class DatabaseService
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(60));
        });

        return services;
    }
}