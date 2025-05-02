using Calendario.Core.Servicios;
using Calendario.Dominio.DTOS;
using Calendario.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Calendario.Presentacion.Controladores
{
    [ApiController]
    [Route("api/festivos")]
    public class FestivoControlador : ControllerBase
    {

        private readonly IFestivoServicio servicio;

        public FestivoControlador(IFestivoServicio servicio)
        {
            this.servicio = servicio;
        }

        // Obtener todos los festivos
        [HttpGet("listar")]
        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await servicio.ObtenerTodos();
        }

        // Verificar si una fecha es festivo
        [HttpGet("es-festivo/{anio}/{mes}/{dia}")]
        public async Task<IActionResult> EsFestivo(int anio, int mes, int dia)
        {

            try
            {
                var esFestivo = await servicio.EsFestivo(anio, mes, dia);
                return Ok(esFestivo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        // Obtener festivos por año
        [HttpGet("festivos-por-anio/{anio}")]
        public async Task<ActionResult<List<FestivoResponseDTO>>> ObtenerFestivosPorAño(int anio)
        {
            var festivos = await servicio.ObtenerFestivosPorAnio(anio);
            return Ok(festivos);
        }

        // Obtener un festivo por su Id
        [HttpGet("obtener/{id}")]
        public async Task<Festivo> Obtener(int id)
        {
            return await servicio.Obtener(id);
        }

        // Buscar festivos según un tipo y un dato específico
        [HttpGet("buscar/{tipo}/{dato}")]
        public async Task<IEnumerable<Festivo>> Buscar(int tipo, string dato)
        {
            return await servicio.Buscar(tipo, dato);
        }

        // Agregar un nuevo festivo
        [HttpPost("agregar")]
        public async Task<Festivo> Agregar([FromBody] Festivo festivo)
        {
            return await servicio.Agregar(festivo);
        }

        // Modificar un festivo existente
        [HttpPut("modificar")]
        public async Task<Festivo> Modificar([FromBody] Festivo festivo)
        {
            return await servicio.Modificar(festivo);
        }

        // Eliminar un festivo por su Id
        [HttpDelete("eliminar/{id}")]
        public async Task<bool> Eliminar(int id)
        {
            return await servicio.Eliminar(id);
        }
    }
}
