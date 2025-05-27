using Calendario.Dominio.Dominio;
using Calendario.Dominio.DTOS;

namespace Calendario.Core.Servicios
{
    public interface IUsuarioServicio
    {

        Task<UsuarioDTO> ValidarUsuario(String NombreUsuario, String Clave);

        Task<IEnumerable<List<UsuarioResponseListDTO>>> ObtenerTodos();

        Task<Usuario> Obtener(int Id);

        Task<Usuario> Agregar(Usuario Usuario);

        Task<Usuario> Modificar(Usuario Usuario);

        Task<bool> Eliminar(int Id);

        Task<IEnumerable<Usuario>> BuscarPorNombreUsuario(string NombreUsuario);
    }
}
