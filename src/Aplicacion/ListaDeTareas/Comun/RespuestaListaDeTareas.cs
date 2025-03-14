
namespace Aplicacion.ListaDeTareas.Comun
{
    public record RespuestaListaDeTareas(
        Guid Id,
        string Titulo,
        List<RespuestaTarea> Tareas
    );
}
