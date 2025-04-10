using Calendario.Core.Repositorios;
using Calendario.Dominio.Entidades;
using Calendario.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;


namespace Calendario.Infraestructura.Repositorios
{
    public class TipoFestivoRepositorio : ITipoFestivoRepositorio
    {
        private readonly CalendarioContext context;

        public TipoFestivoRepositorio(CalendarioContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TipoFestivo>> ObtenerTodos()
        {
            return await context.TiposFestivo.ToArrayAsync();
        }

        public async Task<TipoFestivo> Obtener(int Id)
        {
            return await context.TiposFestivo.FindAsync(Id);
        }

        public async Task<IEnumerable<TipoFestivo>> Buscar(int Tipo, string Dato)
        {
            return await context.TiposFestivo
                .Where(item => Tipo == 0 && item.Tipo.Contains(Dato))
                .ToArrayAsync();
        }

        public async Task<TipoFestivo> Agregar(TipoFestivo tipoFestivo)
        {
            context.TiposFestivo.Add(tipoFestivo);
            await context.SaveChangesAsync();
            return tipoFestivo;
        }

        public async Task<TipoFestivo> Modificar(TipoFestivo tipoFestivo)
        {
            context.Entry(tipoFestivo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return tipoFestivo;
        }

        public async Task<bool> Eliminar(int Id)
        {
            var tipoFestivo = await context.TiposFestivo.FindAsync(Id);
            if (tipoFestivo == null)
            {
                return false;
            }

            context.TiposFestivo.Remove(tipoFestivo);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
