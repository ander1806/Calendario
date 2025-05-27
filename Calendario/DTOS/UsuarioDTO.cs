using Calendario.Dominio.Dominio;

namespace Calendario.Dominio.DTOS
{
    public class UsuarioDTO
    {
        public Usuario usuario { get; set; }
        public String token { get; set; }
    }
}
