using Calendario.Core.Repositorios;
using Calendario.Core.Servicios;
using Calendario.Dominio.Entidades;

namespace Calendario.Aplicacion.Servicios
{
    public class FestivoServicio : IFestivoServicio

    {
        private readonly IFestivoRepositorio repositorio;

        public FestivoServicio(IFestivoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<Festivo> Agregar(Festivo festivo)
        {
            return await repositorio.Agregar(festivo);
        }

        public async Task<IEnumerable<Festivo>> Buscar(int Tipo, string Dato)
        {
            return await repositorio.Buscar(Tipo, Dato);
        }

        public async Task<bool> Eliminar(int Id)
        {
            return await repositorio.Eliminar(Id);
        }

        public async Task<Festivo> Modificar(Festivo festivo)
        {
            return await repositorio.Modificar(festivo);
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return await repositorio.Obtener(Id);
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await repositorio.ObtenerTodos();
        }
    }
    }