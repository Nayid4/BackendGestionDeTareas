
namespace Aplicacion.ListaDeTareas.Eliminar
{
    public class EliminarListaDeTareasValidation : AbstractValidator<EliminarListaDeTareasCommand>
    {
        public EliminarListaDeTareasValidation()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("El Id de la lista de tareas no puede estar vacío.");

        }
    }
}
