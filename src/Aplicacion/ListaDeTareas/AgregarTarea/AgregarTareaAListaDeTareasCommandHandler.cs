
using Dominio.ListasDeTareas;
using Dominio.Primitivos;
using Dominio.Tareas;

namespace Aplicacion.ListaDeTareas.AgregarTarea
{
    public class AgregarTareaAListaDeTareasCommandHandler : IRequestHandler<AgregarTareaAListaDeTareasCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioListaDeTareas _repositorioListaDeTareas;
        private readonly IUnitOfWork _unitOfWork;

        public AgregarTareaAListaDeTareasCommandHandler(IRepositorioListaDeTareas repositorioListaDeTareas, IUnitOfWork unitOfWork)
        {
            _repositorioListaDeTareas = repositorioListaDeTareas ?? throw new ArgumentNullException(nameof(repositorioListaDeTareas));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(AgregarTareaAListaDeTareasCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioListaDeTareas.ListarPorId(new IdListaDeTareas(comando.IdListaDeTareas)) is not ListaDeTarea lista)
            {
                return Error.NotFound("Lista.NoEncontrada", "No se encontro la lista de tareas.");
            }

            var tarea = new Tarea(
                new IdTarea(Guid.NewGuid()),
                lista.Id,
                comando.Tarea.Titulo,
                comando.Tarea.Descripcion,
                comando.Tarea.Estado
            );

            lista.AgregarTarea(tarea);

            _repositorioListaDeTareas.Actualizar(lista);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
