using Calendario.Core.Repositorios;
using Calendario.Core.Servicios;
using Calendario.Dominio.Entidades;

namespace Calendario.Aplicacion.Servicios
{
    public class FestivoServicio : IFestivoServicio

    {
        private readonly IFestivoRepositorio repositorio;
        private readonly FechasServicio fechasServicio;

        public FestivoServicio(IFestivoRepositorio repositorio, FechasServicio fechasServicio)
        {
            this.repositorio = repositorio;
            this.fechasServicio = fechasServicio;
        }

        public async Task<Festivo> Agregar(Festivo festivo)
        {
            // Si el festivo depende de Pascua, calcular la fecha
            if (festivo.DiasPascua != 0)
            {
                DateTime domingoPascua = fechasServicio.ObtenerDomingoPascua(DateTime.Now.Year);
                DateTime fechaFestivo = domingoPascua.AddDays(festivo.DiasPascua);
                // Asignar día y mes al festivo
                festivo.Dia = fechaFestivo.Day;
                festivo.Mes = fechaFestivo.Month;
            }

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