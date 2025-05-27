using Calendario.Core.Repositorios;
using Calendario.Dominio.Dominio;
using Calendario.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Calendario.Infraestructura.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly CalendarioContext  context;

        public UsuarioRepositorio(CalendarioContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> Agregar(Usuario Usuario)
        {
            await context.Usuarios.AddAsync(Usuario);
            await context.SaveChangesAsync();
            return Usuario;
        }

        public async Task<IEnumerable<Usuario>> BuscarPorNombreUsuario(string NombreUsuario)
        {
            if (string.IsNullOrEmpty(NombreUsuario))
                return new List<Usuario>();

            return await context.Usuarios
                .Where(u => u.NombreUsuario.Contains(NombreUsuario))
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int Id)
        {
            var usuario = await context.Usuarios.FindAsync(Id);
            if (usuario == null) return false;

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> Modificar(Usuario Usuario)
        {
            var usuarioExistente = await context.Usuarios.FindAsync(Usuario.Id);
            if (usuarioExistente == null) return null;

            context.Entry(usuarioExistente).CurrentValues.SetValues(Usuario);
            await context.SaveChangesAsync();
            return Usuario;
        }

        public async Task<Usuario> Obtener(int Id)
        {
            return await context.Usuarios.FindAsync(Id);
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodos()
        {
            return await context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> ValidarUsuario(string NombreUsuario, string Clave)
        {
            return await context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == NombreUsuario && u.Clave == Clave);
        }
    }
}
