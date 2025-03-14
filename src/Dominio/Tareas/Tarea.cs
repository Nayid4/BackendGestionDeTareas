using Dominio.ListasDeTareas;
using Dominio.Primitivos;

namespace Dominio.Tareas
{
    public sealed class Tarea : AggregateRoot
    {
        public IdTarea Id { get; private set; } = default!;
        public IdListaDeTareas IdListaDetareas { get; private set; } = default!;
        public string Titulo { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public bool Estado { get; private set; } = false;

        public Tarea() { }

        public Tarea(IdTarea id, IdListaDeTareas idListaDetareas, string titulo, string descripcion, bool estado)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            IdListaDetareas = idListaDetareas ?? throw new ArgumentNullException(nameof(Id));
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Estado = estado;
        }

        public void Actualizar(IdListaDeTareas idListaDetareas, string titulo, string descripcion, bool estado)
        {
            IdListaDetareas = idListaDetareas ?? throw new ArgumentNullException(nameof(Id));
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Estado = estado;
        }
    }
}
