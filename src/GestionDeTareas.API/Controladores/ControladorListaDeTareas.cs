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
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] CrearListaDeTareasCommand comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var resultadoDeEliminar = await _mediator.Send(new EliminarListaDeTareasCommand(id));

            return resultadoDeEliminar.Match(
                resp => NoContent(),
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
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("filtrar-por-estado/{id}")]
        public async Task<IActionResult> FiltrarPorEstado(Guid id, [FromBody] FiltrarTareasPorEstadoDeListaDeTareasQuery comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.AgregacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeFiltrarPorEstado = await _mediator.Send(comando);

            return resultadoDeFiltrarPorEstado.Match(
                resp => Ok(resp),
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
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("eliminar-tarea/{id}")]
        public async Task<IActionResult> EliminarTarea(Guid id, [FromBody] EliminarTareaDeListaDeTareasCommand comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.EliminacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeEliminarTarea = await _mediator.Send(comando);

            return resultadoDeEliminarTarea.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("actualizar-tarea/{id}")]
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
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("actualizar-estado-de-tarea/{id}")]
        public async Task<IActionResult> ActualizarEstadoDeTarea(Guid id, [FromBody] ActualizarEstadoDeTareaDeListaDeTareasCommand comando)
        {
            if (comando.IdListaDeTareas != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("ListaDeTarea.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActulizarEstadoDeTarea = await _mediator.Send(comando);

            return resultadoDeActulizarEstadoDeTarea.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }
    }
}
