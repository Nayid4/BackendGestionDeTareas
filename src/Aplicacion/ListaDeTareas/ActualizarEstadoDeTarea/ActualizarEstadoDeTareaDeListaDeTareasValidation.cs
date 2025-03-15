
namespace Aplicacion.ListaDeTareas.ActualizarEstadoDeTarea
{
    public class ActualizarEstadoDeTareaDeListaDeTareasValidation : AbstractValidator<ActualizarEstadoDeTareaDeListaDeTareasCommand>
    {
        public ActualizarEstadoDeTareaDeListaDeTareasValidation()
        {
            RuleFor(r => r.IdListaDeTareas)
                .NotEmpty().WithMessage("El Id de la lista de tareas no puede estar vacío.");

            RuleFor(r => r.IdTarea)
                .NotEmpty().WithMessage("El Id de la tarea no puede estar vacío.");

            RuleFor(r => r.Estado)
                .NotEmpty().WithMessage("El estado no puede estar vacío.")
                .MaximumLength(25).WithMessage("El estado no puede tener más de 25 caracteres.")
                .Must(estado => new[] { "Pendiente", "Completada" }.Contains(estado))
                .WithMessage("El estado debe ser 'Pendiente' o 'Completada'.");
        }
    }
}
