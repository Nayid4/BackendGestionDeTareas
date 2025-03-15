
namespace Aplicacion.ListaDeTareas.AgregarTarea
{
    public class AgregarTareaAListaDeTareaValidation : AbstractValidator<AgregarTareaAListaDeTareasCommand>
    {
        public AgregarTareaAListaDeTareaValidation()
        {
            RuleFor(r => r.IdListaDeTareas)
                .NotEmpty().WithMessage("El Id de la lista de tareas no puede estar vacío.");

            RuleFor(r => r.Tarea.Titulo)
                .NotEmpty().WithMessage("El título de la tarea no puede estar vacío.")
                .MaximumLength(50).WithMessage("El título no puede tener más de 50 caracteres.");


            RuleFor(r => r.Tarea.Descripcion)
                .NotEmpty().WithMessage("La descripción no puede estar vacía.")
                .MaximumLength(500).WithMessage("El título no puede tener más de 500 caracteres.");

            RuleFor(r => r.Tarea.Estado)
                .NotEmpty().WithMessage("El estado no puede estar vacío.")
                .MaximumLength(25).WithMessage("El estado no puede tener más de 25 caracteres.")
                .Must(estado => new[] { "Pendiente", "Completada" }.Contains(estado))
                .WithMessage("El estado debe ser 'Pendiente' o 'Completada'.");

        }
    }
}
