
namespace Aplicacion.ListaDeTareas.FiltrarTareasPorEstado
{
    public class FiltrarTareasPorEstadoDeListaDeTareasValidation : AbstractValidator<FiltrarTareasPorEstadoDeListaDeTareasQuery>
    {
        public FiltrarTareasPorEstadoDeListaDeTareasValidation()
        {
            RuleFor(r => r.IdListaDeTareas)
                .NotEmpty().WithMessage("El Id de la lista de tareas no puede estar vacío.");

            RuleFor(r => r.EstadoTarea)
                .NotEmpty().WithMessage("El estado no puede estar vacío.")
                .MaximumLength(25).WithMessage("El estado no puede tener más de 25 caracteres.")
                .Must(estado => new[] { "Pendiente", "Completada", "Todas" }.Contains(estado))
                .WithMessage("El estado debe ser 'Pendiente', 'Completada' o 'Todas'.");
        }
    }
}
