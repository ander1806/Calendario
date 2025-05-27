using Calendario.Dominio.Dominio;

namespace Calendario.Core.Repositorios
{
    public interface IUsuarioRepositorio
    {

        Task<Usuario> ValidarUsuario(String NombreUsuario, String Clave);

        Task<IEnumerable<Usuario>> ObtenerTodos();

        Task<Usuario> Obtener(int Id);

        Task<Usuario> Agregar(Usuario Usuario);

        Task<Usuario> Modificar(Usuario Usuario);

        Task<bool> Eliminar(int Id);

        Task<IEnumerable<Usuario>> BuscarPorNombreUsuario(string NombreUsuario);

    }
}
