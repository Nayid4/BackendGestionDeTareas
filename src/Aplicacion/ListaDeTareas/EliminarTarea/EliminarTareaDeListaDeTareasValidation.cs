
namespace Aplicacion.ListaDeTareas.EliminarTarea
{
    public class EliminarTareaDeListaDeTareasValidation : AbstractValidator<EliminarTareaDeListaDeTareasCommand>
    {
        public EliminarTareaDeListaDeTareasValidation()
        {
            RuleFor(r => r.IdListaDeTareas)
                .NotEmpty().WithMessage("El Id de la lista de tareas no puede estar vacío.");

            RuleFor(r => r.IdTarea)
                .NotEmpty().WithMessage("El Id de la tarea no puede estar vacío.");
        }
    }
}
