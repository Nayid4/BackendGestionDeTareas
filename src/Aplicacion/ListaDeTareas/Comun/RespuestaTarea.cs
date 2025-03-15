
namespace Aplicacion.ListaDeTareas.Comun
{
    public record RespuestaTarea(
        Guid Id,
        string Titulo,
        string Descripcion,
        string Estado
    );
}
