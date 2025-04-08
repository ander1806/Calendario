using Calendario.Core.Repositorios;
using Calendario.Dominio.Entidades;
using Calendario.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Calendario.Infraestructura.Repositorios
{
    public class FestivoRepositorio : IFestivoRepositorio
    {
        private readonly CalendarioContext _context;

        public FestivoRepositorio(CalendarioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await _context.Festivos.ToArrayAsync();
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return await _context.Festivos.FindAsync(Id);
        }

        public async Task<IEnumerable<Festivo>> Buscar(int Tipo, string Dato)
        {
            return await _context.Festivos
                .Where(item => Tipo == 0 && item.Nombre.Contains(Dato)
                            || Tipo == 1 && item.TipoFestivo.Tipo.Contains(Dato))
                .ToArrayAsync();
        }

        public async Task<Festivo> Agregar(Festivo festivo)
        {
            _context.Festivos.Add(festivo);
            await _context.SaveChangesAsync();
            return festivo;
        }

        public async Task<Festivo> Modificar(Festivo festivo)
        {
            _context.Entry(festivo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return festivo;
        }

        public async Task<bool> Eliminar(int Id)
        {
            var festivo = await _context.Festivos.FindAsync(Id);
            if (festivo == null)
            {
                return false;
            }

            _context.Festivos.Remove(festivo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
