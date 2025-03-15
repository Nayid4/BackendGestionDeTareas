using Dominio.ListasDeTareas;
using Dominio.Primitivos;

namespace Aplicacion.ListaDeTareas.Eliminar
{
    public class EliminarListaDeTareasCommandHandler : IRequestHandler<EliminarListaDeTareasCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioListaDeTareas _repositorioListaDeTareas;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarListaDeTareasCommandHandler(IRepositorioListaDeTareas repositorioListaDeTareas, IUnitOfWork unitOfWork)
        {
            _repositorioListaDeTareas = repositorioListaDeTareas ?? throw new ArgumentNullException(nameof(repositorioListaDeTareas));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarListaDeTareasCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioListaDeTareas.ListarPorId(new IdListaDeTareas(comando.Id)) is not ListaDeTarea lista)
            {
                return Error.NotFound("Lista.NoEncontrada", "No se encontro la lista de tareas.");
            }

            _repositorioListaDeTareas.Eliminar(lista);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
