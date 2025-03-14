
namespace Aplicacion.ListaDeTareas.Crear
{
    public class CrearListaDeTareasValidation : AbstractValidator<CrearListaDeTareasCommand>
    {
        public CrearListaDeTareasValidation()
        {
            RuleFor(r => r.Titulo)
                .NotEmpty().WithMessage("El título de la lista de tareas no puede estar vacío.")
                .MaximumLength(50).WithMessage("El título no puede tener más de 50 caracteres.");
        }
    }
}
