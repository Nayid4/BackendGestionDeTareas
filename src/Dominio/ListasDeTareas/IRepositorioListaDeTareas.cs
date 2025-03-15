

namespace Dominio.ListasDeTareas
{
    public interface IRepositorioListaDeTareas
    {
        void Crear(ListaDeTarea lista);
        void Eliminar(ListaDeTarea id);
        Task<List<ListaDeTarea>> ListarTodos();
        void Actualizar(ListaDeTarea lista);
        Task<ListaDeTarea?> ListarPorId(IdListaDeTareas id);
    }
}
