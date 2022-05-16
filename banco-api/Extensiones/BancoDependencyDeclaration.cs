using banco_dao.Modelos;
using banco_dao.ModelosContratos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace banco_api.Extensiones
{
    public static class BancoDependencyDeclaration
    {
        public static IServiceCollection AddDependencyDeclaration(this IServiceCollection services)
        {

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IClienteModelo, ClienteModelo>();
            services.AddScoped<ICuentaModelo, CuentaModelo>();
            services.AddScoped<IMovimientoModelo, MovimientoModelo>();


            return services;
        }
    }
}
