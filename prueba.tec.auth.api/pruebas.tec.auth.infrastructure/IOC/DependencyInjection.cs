using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pruebas.tec.auth.infrastructure.Database;
using pruebas.tec.auth.infrastructure.Interfaces.Auth;
using pruebas.tec.auth.infrastructure.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruebas.tec.auth.infrastructure.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            string conection = configuration["ConecctionStrings:ConexionBD"]!.ToString();

            var username = Environment.GetEnvironmentVariable("pruebaAUTH_BD_USER");
            var password = Environment.GetEnvironmentVariable("pruebaAUTH_BD_PASSWORD");

            var conecctionBuilder = new SqlConnectionStringBuilder(conection)
            {
                Password = password,
                UserID = username,
            };

            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(conecctionBuilder.ConnectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            return services;
        }
    }
}
