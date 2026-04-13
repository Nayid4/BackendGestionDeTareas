
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GestionDeTareas.API.Middlewares;

namespace GestionDeTareas.API.Servicios
{
    public static class InyeccionDeDependencias
    {
        public static IServiceCollection AddPresentation(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddControllers();
            servicios.AddEndpointsApiExplorer();
            servicios.AddSwaggerGen();

            servicios.AddHealthChecks();

            servicios.AddTransient<GlobalExceptionHandlingMiddleware>();


            servicios.AddCors(options =>
            {
                
                /*options.AddPolicy("web", policyBuilder =>
                {
                    policyBuilder.WithOrigins(
                        "http://localhost:4200",
                        "http://localhost:8100",
                        "http://localhost:80",
                        "https://nayid4.github.io/FrontendGestionDeTareas",
                        "https://nayid4.github.io",
                        "http://frontend-gestion-de-tareas:4200",
                        "http://frontend-gestion-de-tareas-1:4200",
                        "http://frontend-gestion-de-tareas-2:4200",

                        "http://frontend-gestion-de-tareas:80",
                        "http://frontend-gestion-de-tareas-1:80",
                        "http://frontend-gestion-de-tareas-2:80",
                        "http://localhost:58702/"
                        );
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                });*/
                

                options.AddPolicy("web", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin();
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                });

            });

            return servicios;
        }
    }
}
