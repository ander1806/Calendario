using Calendario.Core.Servicios;
using Calendario.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Calendario.Presentacion.Controladores
{
    [ApiController]
    [Route("api/tipofestivos")]
    public class TipoFestivoControlador
    {
        private readonly ITipoFestivoServicio servicio;

        public TipoFestivoControlador(ITipoFestivoServicio servicio)
        {
            this.servicio = servicio;
        }

        // Obtener todos los tipos de festivos
        [HttpGet("listar")]
        public async Task<IEnumerable<TipoFestivo>> ObtenerTodos()
        {
            return await servicio.ObtenerTodos();
        }

        // Obtener un tipo de festivo por su Id
        [HttpGet("obtener/{id}")]
        public async Task<TipoFestivo> Obtener(int id)
        {
            return await servicio.Obtener(id);
        }

        // Agregar un nuevo tipo de festivo
        [HttpPost("agregar")]
        public async Task<TipoFestivo> Agregar([FromBody] TipoFestivo tipoFestivo)
        {
            return await servicio.Agregar(tipoFestivo);
        }

        // Modificar un tipo de festivo existente
        [HttpPut("modificar")]
        public async Task<TipoFestivo> Modificar([FromBody] TipoFestivo tipoFestivo)
        {
            return await servicio.Modificar(tipoFestivo);
        }


        // Eliminar un tipo de festivo por su Id
        [HttpDelete("eliminar/{id}")]
        public async Task<bool> Eliminar(int id)
        {
            return await servicio.Eliminar(id);
        }

        // Buscar tipos de festivo según un tipo y un dato específico
        [HttpGet("buscar/{tipo}/{dato}")]
        public async Task<IEnumerable<TipoFestivo>> Buscar(int tipo, string dato)
        {
            return await servicio.Buscar(tipo, dato);
        }


    }
}
