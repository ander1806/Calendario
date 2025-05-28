using Calendario.Core.Servicios;
using Calendario.Dominio.Dominio;
using Calendario.Dominio.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace Calendario.Presentacion.Controladores
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioControlador: ControllerBase
    {
        private readonly IUsuarioServicio servicio;

        public UsuarioControlador(IUsuarioServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpPost("login")]
        public async Task<UsuarioDTO> Login([FromBody] UsuarioRequestLoginDTO request)
        {
            return await servicio.ValidarUsuario(request.NombreUsuario, request.Clave);
        }

        [HttpGet("listar/")]
        public async Task<ActionResult<IEnumerable<UsuarioResponseListDTO>>> ObtenerTodos()
        {
            try
            {
                var usuarios = await servicio.ObtenerTodos();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("obtener-por-id/{id}")]
        public async Task<ActionResult<Usuario>> Obtener(int id)
        {
            try
            {
                var usuario = await servicio.Obtener(id);
                if (usuario == null)
                    return NotFound($"Usuario con ID {id} no encontrado");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("crear-usuario/")]
        public async Task<ActionResult<Usuario>> Crear([FromBody] Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await servicio.Agregar(usuario);
                return CreatedAtAction(nameof(Obtener), new { id = usuario.Id }, usuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("modificar/{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (id != usuario.Id)
                    return BadRequest("El ID de la ruta no coincide con el ID del usuario");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await servicio.Modificar(usuario);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                await servicio.Eliminar(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}

    

