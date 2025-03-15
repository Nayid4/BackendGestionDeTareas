

using Dominio.ListasDeTareas;
using Dominio.Primitivos;

namespace Aplicacion.ListaDeTareas.Crear
{
    public sealed class CrearListaDeTareasCommandHandler : IRequestHandler<CrearListaDeTareasCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioListaDeTareas _repositorioListaDeTareas;
        private readonly IUnitOfWork _unitOfWork;

        public CrearListaDeTareasCommandHandler(IRepositorioListaDeTareas repositorioListaDeTareas, IUnitOfWork unitOfWork)
        {
            _repositorioListaDeTareas = repositorioListaDeTareas ?? throw new ArgumentNullException(nameof(repositorioListaDeTareas));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearListaDeTareasCommand comando, CancellationToken cancellationToken)
        {

            _repositorioListaDeTareas.Crear(new ListaDeTarea(new IdListaDeTareas(Guid.NewGuid()), comando.Titulo));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
