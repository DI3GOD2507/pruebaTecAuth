using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pruebas.tec.auth.application.Interfaces.Auth;
using pruebas.tec.auth.application.Services.Auth;
using pruebas.tec.auth.application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruebas.tec.auth.application.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();



            services.AddSingleton<PasswordUtils>();
            services.AddSingleton<JWTUtils>();

            return services;
        }
    }
}
