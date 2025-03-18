
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
            servicios.AddTransient<GlobalExceptionHandlingMiddleware>();


            servicios.AddCors(options =>
            {
                /*
                options.AddPolicy("web", policyBuilder =>
                {
                    policyBuilder.WithOrigins(
                        "http://localhost:4200",
                        "https://nayid4.github.io/FrontendGestionDeTareas",
                        "https://nayid4.github.io"
                        );
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                });
                */

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
