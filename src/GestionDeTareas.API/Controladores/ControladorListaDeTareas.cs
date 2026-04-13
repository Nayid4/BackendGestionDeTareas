using Aplicacion.ListaDeTareas.Actualizar;
using Aplicacion.ListaDeTareas.ActualizarEstadoDeTarea;
using Aplicacion.ListaDeTareas.ActualizarTarea;
using Aplicacion.ListaDeTareas.AgregarTarea;
using Aplicacion.ListaDeTareas.Crear;
using Aplicacion.ListaDeTareas.Eliminar;
using Aplicacion.ListaDeTareas.EliminarTarea;
using Aplicacion.ListaDeTareas.FiltrarTareasPorEstado;
using Aplicacion.ListaDeTareas.ListarTodos;
using Microsoft.AspNetCore.Authorization;

namespace GestionDeTareas.API.Controllers

{
    [Route("lista-de-tareas")]
    [AllowAnonymous]
    public class ControladorListaDeTareas : ApiController
    {
        private readonly ISender _mediator;
        private readonly ILogger<ControladorListaDeTareas> _logger;

        public ControladorListaDeTareas(ISender mediator, ILogger<ControladorListaDeTareas> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            _logger.LogInformation("Consultando todas las listas de tareas en el entorno AWS.");

            var resultadosDeListarTodos = await _mediator.Send(new ListarTodasLasListasDeTareasQuery());

            return resultadosDeListarTodos.Match(
                resp => Ok(resp),
                errores => {
                    _logger.LogError("Error al listar tareas: {Errores}", errores); // Log de error
                    return Problem(errores);
                }
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] CrearListaDeTareasCommand comando)
        {
            _logger.LogInformation("Iniciando creación de lista de tareas: {NombreLista}", comando.Titulo); // Asumiendo que tiene propiedad Nombre

            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                resp => {
                    _logger.LogInformation("Lista de tareas creada exitosamente con ID: {ListaId}", resp);
                    return Ok(resp);
                },
                errores => {
                    _logger.LogWarning("Fallo al crear lista de tareas. Errores: {@Errores}", errores);
                    return Problem(errores);
                }
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            _logger.LogInformation("Solicitud para eliminar lista de tareas: {ListaId}", id); 

            var resultadoDeEliminar = await _mediator.Send(new EliminarListaDeTareasCommand(id));

            return resultadoDeEliminar.Match(
                resp => {
                    _logger.LogInformation("Lista {ListaId} eliminada correctamente", id);
                    return NoContent();
                },
                errores => {
                    _logger.LogError("Error al intentar eliminar la lista {ListaId}: {@Errores}", id, errores);
                    return Problem(errores);
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarListaDeTareasCommand comando)
        {
            if (comando.Id != id)
            {
                _logger.LogWarning("Inconsistencia en IDs para actualización. Query: {QueryId}, Body: {BodyId}", id, comando.Id);
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActualizarListaTarea = await _mediator.Send(comando);

            return resultadoDeActualizarListaTarea.Match(
                resp => {
                    _logger.LogInformation("Lista de tareas {ListaId} actualizada exitosamente", id);
                    return NoContent();
                },
                errores => {
                    _logger.LogError("Fallo en actualización de lista {ListaId}: {@Errores}", id, errores);
                    return Problem(errores);
                }
            );
        }

        [HttpPost("filtrar-por-estado/{id}")]
        public async Task<IActionResult> FiltrarPorEstado(Guid id, [FromBody] FiltrarTareasPorEstadoDeListaDeTareasQuery comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                _logger.LogWarning("ID de lista no coincide en filtrado. Path: {PathId}, Query: {QueryId}", id, comando.IdListaDeTareas);
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.AgregacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            _logger.LogInformation("Filtrando tareas de la lista {ListaId} por estado", id);
            var resultadoDeFiltrarPorEstado = await _mediator.Send(comando);

            return resultadoDeFiltrarPorEstado.Match(
                resp => Ok(resp),
                errores => {
                    _logger.LogError("Error al filtrar tareas de la lista {ListaId}: {@Errores}", id, errores);
                    return Problem(errores);
                }
            );
        }

        [HttpPost("agregar-tarea/{id}")]
        public async Task<IActionResult> AgregarTarea(Guid id, [FromBody] AgregarTareaAListaDeTareasCommand comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                _logger.LogWarning("ID de lista no coincide al agregar tarea. Path: {PathId}, Command: {CmdId}", id, comando.IdListaDeTareas);
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.AgregacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeAgregarTarea = await _mediator.Send(comando);

            return resultadoDeAgregarTarea.Match(
                resp => {
                    _logger.LogInformation("Nueva tarea agregada a la lista {ListaId}", id);
                    return NoContent();
                },
                errores => {
                    _logger.LogError("Error al agregar tarea en lista {ListaId}: {@Errores}", id, errores);
                    return Problem(errores);
                }
            );
        }

        [HttpPost("eliminar-tarea/{id}")]
        public async Task<IActionResult> EliminarTarea(Guid id, [FromBody] EliminarTareaDeListaDeTareasCommand comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                _logger.LogWarning("ID de lista no coincide al eliminar tarea. Path: {PathId}", id);
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.EliminacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeEliminarTarea = await _mediator.Send(comando);

            return resultadoDeEliminarTarea.Match(
                resp => {
                    _logger.LogInformation("Tarea {TareaId} eliminada de la lista {ListaId}", comando.IdTarea, id);
                    return NoContent();
                },
                errores => {
                    _logger.LogError("Error al eliminar tarea de lista {ListaId}: {@Errores}", id, errores);
                    return Problem(errores);
                }
            );
        }

        [HttpPost("actualizar-tarea/{id}")]
        public async Task<IActionResult> ActualizarTarea(Guid id, [FromBody] ActualizarTareaDeListaDeTareasCommand comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                _logger.LogWarning("ID de lista no coincide al actualizar tarea. Path: {PathId}", id);
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActulizarTarea = await _mediator.Send(comando);

            return resultadoDeActulizarTarea.Match(
                resp => {
                    _logger.LogInformation("Tarea {TareaId} en lista actualizada", id.ToString());
                    return NoContent();
                },
                errores => {
                    _logger.LogError("Error actualizando tarea en lista {ListaId}: {@Errores}", id, errores);
                    return Problem(errores);
                }
            );
        }

        [HttpPost("actualizar-estado-de-tarea/{id}")]
        public async Task<IActionResult> ActualizarEstadoDeTarea(Guid id, [FromBody] ActualizarEstadoDeTareaDeListaDeTareasCommand comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                _logger.LogWarning("ID de lista no coincide al cambiar estado de tarea. Path: {PathId}", id);
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActulizarEstadoDeTarea = await _mediator.Send(comando);

            return resultadoDeActulizarEstadoDeTarea.Match(
                resp => {
                    _logger.LogInformation("Estado cambiado para tarea {TareaId} en lista {ListaId}", comando.IdTarea, id);
                    return NoContent();
                },
                errores => {
                    _logger.LogError("Error cambiando estado de tarea en lista {ListaId}: {@Errores}", id, errores);
                    return Problem(errores);
                }
            );
        }
    }
}
