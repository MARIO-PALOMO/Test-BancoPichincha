using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using banco_api.Extensiones;
using banco_dao.Contexto;

namespace banco_api
{
    public class Startup
    {
        private readonly string migrationTableName = "__bancoMigrationsHistory";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "banco_api", Version = "v1" });
            });

            services.AddDependencyDeclaration();

            ConfigDatabaseConnection(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "banco_api v1"));
            }

            app.UseCors("AllOrigins");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void ConfigDatabaseConnection(IServiceCollection services)
        {
            var switchConnectionString = Configuration.GetConnectionString("Banco");
            if (string.IsNullOrEmpty(switchConnectionString))
            {
                throw new Exception("Cadena de conexion de BDD no configurada: Banco");
            }

            services.AddDbContext<BancoContext>(
                (serviceProvider, options) =>
                {
                    options.UseSqlServer(switchConnectionString,
                        x =>
                        {
                            x.MigrationsHistoryTable(migrationTableName);
                            x.EnableRetryOnFailure(5);
                        }).EnableSensitiveDataLogging();
                });
        }
    }
}
