using Aplicacion.ListaDeTareas.Actualizar;
using Aplicacion.ListaDeTareas.ActualizarTarea;
using Aplicacion.ListaDeTareas.AgregarTarea;
using Aplicacion.ListaDeTareas.Crear;
using Aplicacion.ListaDeTareas.Eliminar;
using Aplicacion.ListaDeTareas.EliminarTarea;
using Aplicacion.ListaDeTareas.ListarTodos;
using Microsoft.AspNetCore.Authorization;

namespace GestionDeTareas.API.Controllers

{
    [Route("lista-de-tareas")]
    public class ControladorListaDeTareas : ApiController
    {
        private readonly ISender _mediator;

        public ControladorListaDeTareas(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new ListarTodasLasListasDeTareasQuery());

            return resultadosDeListarTodos.Match(
                usuarios => Ok(usuarios),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] CrearListaDeTareasCommand comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                usuarioId => Ok(usuarioId),
                errores => Problem(errores)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var resultadoDeEliminar = await _mediator.Send(new EliminarListaDeTareasCommand(id));

            return resultadoDeEliminar.Match(
                usuarioId => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarListaDeTareasCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActualizarListaTarea = await _mediator.Send(comando);

            return resultadoDeActualizarListaTarea.Match(
                pqrdId => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("agregar-tarea/{id}")]
        public async Task<IActionResult> AgregarTarea(Guid id, [FromBody] AgregarTareaAListaDeTareasCommand comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.AgregacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeAgregarTarea = await _mediator.Send(comando);

            return resultadoDeAgregarTarea.Match(
                pqrdId => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("eliminar-tarea/{id}")]
        public async Task<IActionResult> EliminarTarea(Guid id, [FromBody] EliminarTareaDeListaDeTareasCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.EliminacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeEliminarTarea = await _mediator.Send(comando);

            return resultadoDeEliminarTarea.Match(
                pqrdId => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("actulizar-tarea/{id}")]
        public async Task<IActionResult> ActualizarTarea(Guid id, [FromBody] ActualizarTareaDeListaDeTareasCommand comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActulizarTarea = await _mediator.Send(comando);

            return resultadoDeActulizarTarea.Match(
                pqrdId => NoContent(),
                errores => Problem(errores)
            );
        }
    }
}
