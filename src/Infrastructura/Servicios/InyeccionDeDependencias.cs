using Aplicacion.Datos;
using Dominio.ListasDeTareas;
using Dominio.Primitivos;
using Infraestructure.Persistencia;
using Infrastructura.Persistencia.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Servicios
{
    public static class InyeccionDeDependencias
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AgregarPersistencias(configuracion);
            return servicios;
        }

        public static IServiceCollection AgregarPersistencias(this IServiceCollection servicios, IConfiguration configuracion)
        {
            // 1. Registro del Health Check para SQL Server
            servicios.AddHealthChecks()
                .AddSqlServer(
                    connectionString: configuracion.GetConnectionString("Database"),
                    name: "RDS-SQLServer",
                    timeout: TimeSpan.FromSeconds(5),
                    tags: new[] { "db", "sql", "aws" });

            servicios.AddDbContext<AplicacionContextoDb>(options =>
                options.UseSqlServer(
                    configuracion.GetConnectionString("Database"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    }
                    ));

            servicios.AddScoped<IAplicacionContextoDb>(sp =>
                sp.GetRequiredService<AplicacionContextoDb>());

            servicios.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<AplicacionContextoDb>());

            servicios.AddScoped<IRepositorioListaDeTareas, RepositorioListaDeTareas>();

            return servicios;
        }

    }
}
