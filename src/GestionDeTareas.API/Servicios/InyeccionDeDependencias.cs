
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
                /*options.AddPolicy("webLocalSac", policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://localhost:4200");
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowCredentials();
                });*/

                /*options.AddPolicy("webLocalSac", policyBuilder =>
                {
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowCredentials();
                });*/

                options.AddPolicy("webRemota", policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://gestion-de-tareas-liard.vercel.app/")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                });
            });

            return servicios;
        }
    }
}
