using Aplicacion.comun.Behaviors;
using Aplicacion.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Servicios
{
    public static class InyenccionDeDependencias
    {
        public static IServiceCollection AddAplication(this IServiceCollection servicios)
        {
            servicios.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });

            servicios.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidacionBehavior<,>)
            );

            servicios.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();


            return servicios;
        }
    }
}
