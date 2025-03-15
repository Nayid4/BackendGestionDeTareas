using Dominio.ListasDeTareas;
using Dominio.Primitivos;
using Dominio.Tareas;

namespace Aplicacion.ListaDeTareas.ActualizarTarea
{
    public class ActualizarTareaDeListaDeTareasCommandHandler : IRequestHandler<ActualizarTareaDeListaDeTareasCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioListaDeTareas _repositorioListaDeTareas;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarTareaDeListaDeTareasCommandHandler(IRepositorioListaDeTareas repositorioListaDeTareas, IUnitOfWork unitOfWork)
        {
            _repositorioListaDeTareas = repositorioListaDeTareas ?? throw new ArgumentNullException(nameof(repositorioListaDeTareas));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ActualizarTareaDeListaDeTareasCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioListaDeTareas.ListarPorId(new IdListaDeTareas(comando.IdListaDeTareas)) is not ListaDeTarea lista)
            {
                return Error.NotFound("Lista.NoEncontrada", "No se encontro la lista de tareas.");
            }

            if (lista.BuscarTarea(new IdTarea(comando.Tarea.Id)) is not Tarea tarea)
            {
                return Error.NotFound("Tarea.NoEncontrada", "No se encontro la tarea en la lista de tareas.");
            }

            tarea.Actualizar(lista.Id, comando.Tarea.Titulo, comando.Tarea.Descripcion, comando.Tarea.Estado);

            _repositorioListaDeTareas.Actualizar(lista);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
