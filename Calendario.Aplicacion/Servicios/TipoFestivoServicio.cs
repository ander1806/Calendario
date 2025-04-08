using Calendario.Core.Repositorios;
using Calendario.Core.Servicios;
using Calendario.Dominio.Entidades;

namespace Calendario.Aplicacion.Servicios
{
    public class TipoFestivoServicio : ITipoFestivoServicio
    {
        private readonly ITipoFestivoRepositorio repositorio;

        public TipoFestivoServicio(ITipoFestivoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<TipoFestivo> Agregar(TipoFestivo tipoFestivo)
        {
           return await repositorio.Agregar(tipoFestivo);
        }

        public async Task<IEnumerable<TipoFestivo>> Buscar(int Tipo, string Dato)
        {
            return await repositorio.Buscar(Tipo, Dato);
        }

        public async Task<bool> Eliminar(int Id)
        {
            return await repositorio.Eliminar(Id);
        }

        public async Task<TipoFestivo> Modificar(TipoFestivo tipoFestivo)
        {
            return await repositorio.Modificar(tipoFestivo);
        }

        public async Task<TipoFestivo> Obtener(int Id)
        {
            return await repositorio.Obtener(Id);
        }

        public async Task<IEnumerable<TipoFestivo>> ObtenerTodos()
        {
            return await repositorio.ObtenerTodos();
        }
    }
}
