using Dominio.ListasDeTareas;
using Infraestructure.Persistencia;

namespace Infrastructura.Persistencia.Repositorios
{
    public class RepositorioListaDeTareas : IRepositorioListaDeTareas
    {
        protected readonly AplicacionContextoDb _contexto;
        protected readonly DbSet<ListaDeTarea> _dbSet;

        public RepositorioListaDeTareas(AplicacionContextoDb contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _dbSet = _contexto.Set<ListaDeTarea>();
        }

        public void Actualizar(ListaDeTarea lista) => _dbSet.Update(lista);

        public async void Crear(ListaDeTarea lista) => await _dbSet.AddAsync(lista);

        public void Eliminar(ListaDeTarea id) => _dbSet.Remove(id);

        public async Task<ListaDeTarea?> ListarPorId(IdListaDeTareas id) => await _dbSet.Include(t => t.Tareas).FirstOrDefaultAsync(i => i.Id.Equals(id));

        public Task<List<ListaDeTarea>> ListarTodos() => _dbSet.OrderBy(t => t.FechaDeCreacion).Include(t => t.Tareas).ToListAsync();
    }
}
