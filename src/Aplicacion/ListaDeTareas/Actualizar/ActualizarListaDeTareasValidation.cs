
namespace Aplicacion.ListaDeTareas.Actualizar
{
    public class ActualizarListaDeTareasValidation : AbstractValidator<ActualizarListaDeTareasCommand>
    {
        public ActualizarListaDeTareasValidation()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("El Id de la lista de tareas no puede estar vacío.");

            RuleFor(r => r.Titulo)
                .NotEmpty().WithMessage("El título de la lista de tareas no puede estar vacío.")
                .MaximumLength(50).WithMessage("El título no puede tener más de 50 caracteres.");
        }
    }
}
