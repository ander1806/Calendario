using Calendario.Core.Repositorios;
using Calendario.Dominio.Entidades;
using Calendario.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Calendario.Infraestructura.Repositorios
{
    public class FestivoRepositorio : IFestivoRepositorio
    {
        private readonly CalendarioContext context;

        public FestivoRepositorio(CalendarioContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await context.Festivos.ToArrayAsync();
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return await context.Festivos.FindAsync(Id);
        }

        public async Task<IEnumerable<Festivo>> Buscar(int Tipo, string Dato)
        {
            return await context.Festivos
                .Where(item => Tipo == 0 && item.Nombre.Contains(Dato)
                            || Tipo == 1 && item.TipoFestivo.Tipo.Contains(Dato))
                .ToArrayAsync();
        }

        public async Task<Festivo> Agregar(Festivo festivo)
        {
            context.Festivos.Add(festivo);
            await context.SaveChangesAsync();
            return festivo;
        }

        public async Task<Festivo> Modificar(Festivo festivo)
        {
            context.Entry(festivo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return festivo;
        }

        public async Task<bool> Eliminar(int Id)
        {
            var festivo = await context.Festivos.FindAsync(Id);
            if (festivo == null)
            {
                return false;
            }

            context.Festivos.Remove(festivo);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
