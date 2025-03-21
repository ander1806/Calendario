using Calendario.Core.Repositorios;
using Calendario.Dominio.Entidades;
using Calendario.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;


namespace Calendario.Repositorios
{
    public class TipoFestivoRepositorio : ITipoFestivoRepositorio
    {
        private readonly CalendarioContext _context;

        public TipoFestivoRepositorio(CalendarioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoFestivo>> ObtenerTodos()
        {
            return await _context.TiposFestivo.ToArrayAsync();
        }

        public async Task<TipoFestivo> Obtener(int Id)
        {
            return await _context.TiposFestivo.FindAsync(Id);
        }

        public async Task<IEnumerable<TipoFestivo>> Buscar(int Tipo, string Dato)
        {
            return await _context.TiposFestivo
                .Where(item => (Tipo == 0 && item.Tipo.Contains(Dato)))
                .ToArrayAsync();
        }

        public async Task<TipoFestivo> Agregar(TipoFestivo tipoFestivo)
        {
            _context.TiposFestivo.Add(tipoFestivo);
            await _context.SaveChangesAsync();
            return tipoFestivo;
        }

        public async Task<TipoFestivo> Modificar(TipoFestivo tipoFestivo)
        {
            _context.Entry(tipoFestivo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tipoFestivo;
        }

        public async Task<bool> Eliminar(int Id)
        {
            var tipoFestivo = await _context.TiposFestivo.FindAsync(Id);
            if (tipoFestivo == null)
            {
                return false;
            }

            _context.TiposFestivo.Remove(tipoFestivo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
