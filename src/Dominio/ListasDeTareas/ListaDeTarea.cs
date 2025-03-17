using Dominio.Primitivos;
using Dominio.Tareas;

namespace Dominio.ListasDeTareas
{
    public sealed class ListaDeTarea : AggregateRoot
    {
        public IdListaDeTareas Id { get; private set; } = default!;
        public string Titulo { get; private set; } = string.Empty;

        private readonly HashSet<Tarea> _tareas = new HashSet<Tarea>();
        public ICollection<Tarea> Tareas => _tareas;
        public DateTime? FechaDeCreacion { get; private set; }

        public ListaDeTarea()
        {
        }

        public ListaDeTarea(IdListaDeTareas id, string titulo)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            FechaDeCreacion = DateTime.Now;
        }

        public void Actualizar(string titulo)
        {
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
        }

        public void AgregarTarea(Tarea tarea)
        {
            _tareas.Add(tarea);
        }

        public void EliminarTarea(Tarea tarea)
        {
            _tareas.Remove(tarea);
        }

        public Tarea? BuscarTarea(IdTarea id)
        {
            return _tareas.FirstOrDefault(t => t.Id.Equals(id));
        }

        public List<Tarea> FiltrarPorEstado(string estado)
        {
            return _tareas.Where(r => r.Estado.Equals(estado)).ToList();
        }

        public List<Tarea> ListarTodasLasTareas()
        {
            return _tareas.ToList();
        }


    }
}
