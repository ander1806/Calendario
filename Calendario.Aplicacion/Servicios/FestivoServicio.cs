using Calendario.Core.Repositorios;
using Calendario.Core.Servicios;
using Calendario.Dominio.DTOS;
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
            // Si el festivo depende de Pascua, calcular la fecha
            if (festivo.DiasPascua != 0)
            {
                DateTime domingoPascua = FechasServicio.ObtenerDomingoPascua(DateTime.Now.Year);
                DateTime fechaFestivo = domingoPascua.AddDays(festivo.DiasPascua);
                // Asignar día y mes al festivo
                festivo.Dia = fechaFestivo.Day;
                festivo.Mes = fechaFestivo.Month;
                
            }

            return await repositorio.Agregar(festivo);
        }

        //Verificar si es festivo o no
        public async Task<bool> EsFestivo(int anio, int mes, int dia)
        {

            DateTime fecha;
            try
            {
                fecha = new DateTime(anio, mes, dia);
            }
            catch (Exception)
            {
                // Lanzamos una excepción controlada
                throw new ArgumentException("Fecha no válida");
            }

            var festivos = await repositorio.ObtenerTodos();
            
            DateTime domingoPascua = FechasServicio.ObtenerDomingoPascua(fecha.Year);

            foreach (var festivo in festivos)
            {
                var fechaFestivo = CalcularFechaFestivo(festivo, anio, domingoPascua);

                if (fechaFestivo == fecha.Date)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<List<FestivoResponseDTO>> ObtenerFestivosPorAnio(int anio)
        {
            var festivos = await repositorio.ObtenerTodos();
            var fechasFestivos = new List<FestivoResponseDTO>();
            DateTime domingoPascua = FechasServicio.ObtenerDomingoPascua(anio);

            foreach (var festivo in festivos)
            {
                var fechaFestivo = CalcularFechaFestivo(festivo, anio, domingoPascua);

                fechasFestivos.Add(new FestivoResponseDTO
                {
                    Nombre = festivo.Nombre,
                    Fecha = fechaFestivo.ToString("yyyy-MM-dd"),
                });
            }

            return fechasFestivos;
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



        private DateTime CalcularFechaFestivo(Festivo festivo, int año, DateTime domingoPascua)
        {
            DateTime fechaFestivo;

            switch (festivo.IdTipo)
            {
                case 1: // Fijo
                    fechaFestivo = new DateTime(año, festivo.Mes, festivo.Dia);
                    break;

                case 2: // Fijo trasladable
                    fechaFestivo = new DateTime(año, festivo.Mes, festivo.Dia);
                    fechaFestivo = FechasServicio.SiguienteLunes(fechaFestivo);
                    break;

                case 3: // Basado en Pascua
                    fechaFestivo = domingoPascua.AddDays(festivo.DiasPascua);
                    break;

                case 4: // Basado en Pascua trasladable
                    fechaFestivo = domingoPascua.AddDays(festivo.DiasPascua);
                    fechaFestivo = FechasServicio.SiguienteLunes(fechaFestivo);
                    break;

                default:
                    throw new ArgumentException("Tipo de festivo no soportado");
            }

            return fechaFestivo.Date;
        }

    }
    }