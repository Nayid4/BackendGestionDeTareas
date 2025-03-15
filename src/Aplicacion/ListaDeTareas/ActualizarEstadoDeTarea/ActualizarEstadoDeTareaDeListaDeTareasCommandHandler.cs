using Dominio.ListasDeTareas;
using Dominio.Primitivos;
using Dominio.Tareas;

namespace Aplicacion.ListaDeTareas.ActualizarEstadoDeTarea
{
    public class ActualizarEstadoDeTareaDeListaDeTareasCommandHandler : IRequestHandler<ActualizarEstadoDeTareaDeListaDeTareasCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioListaDeTareas _repositorioListaDeTareas;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarEstadoDeTareaDeListaDeTareasCommandHandler(IRepositorioListaDeTareas repositorioListaDeTareas, IUnitOfWork unitOfWork)
        {
            _repositorioListaDeTareas = repositorioListaDeTareas ?? throw new ArgumentNullException(nameof(repositorioListaDeTareas));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ActualizarEstadoDeTareaDeListaDeTareasCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioListaDeTareas.ListarPorId(new IdListaDeTareas(comando.IdListaDeTareas)) is not ListaDeTarea lista)
            {
                return Error.NotFound("Lista.NoEncontrada", "No se encontro la lista de tareas.");
            }

            if (lista.BuscarTarea(new IdTarea(comando.IdTarea)) is not Tarea tarea)
            {
                return Error.NotFound("Tarea.NoEncontrada", "No se encontro la tarea en la lista de tareas.");
            }

            tarea.ActualizarEstado(comando.Estado);

            _repositorioListaDeTareas.Actualizar(lista);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
