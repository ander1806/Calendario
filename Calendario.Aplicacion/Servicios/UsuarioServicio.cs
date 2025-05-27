using Calendario.Core.Repositorios;
using Calendario.Core.Servicios;
using Calendario.Dominio.Dominio;
using Calendario.Dominio.DTOS;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Calendario.Aplicacion.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {

        private readonly IUsuarioRepositorio repositorio;
        private readonly string claveSecreta;

        public UsuarioServicio(IUsuarioRepositorio repositorio, IConfiguration config)
        {
            this.repositorio = repositorio;
            this.claveSecreta = config["Jwt:Key"];
        }
        public async Task<Usuario> Agregar(Usuario Usuario)
        {
            if(Usuario == null)
            {
                throw new ArgumentNullException(nameof(Usuario), "El usuario no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(Usuario.NombreUsuario) || string.IsNullOrWhiteSpace(Usuario.Clave))
            {
                throw new ArgumentException("El nombre de usuario y la clave no pueden estar vacíos");
            }
            // validar si ya existe un usuario con el mismo nombre de usuario
            var usuarioExistente = repositorio.BuscarPorNombreUsuario(Usuario.NombreUsuario).Result.FirstOrDefault();
            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("El usuario ya existe");
            }
            return await repositorio.Agregar(Usuario);
        }

        public async Task<IEnumerable<Usuario>> BuscarPorNombreUsuario(string NombreUsuario)
        {
            if (NombreUsuario == null)
            {
                throw new ArgumentNullException(nameof(NombreUsuario), "El nombre de usuario no puede ser nulo");
            }
            var usuarioExistente = repositorio.BuscarPorNombreUsuario(NombreUsuario).Result;
            if (usuarioExistente == null || !usuarioExistente.Any())
            {
                throw new InvalidOperationException("No se encontró ningún usuario con ese nombre de usuario");
            }
            return await Task.FromResult(usuarioExistente);
        }

        public async Task<bool> Eliminar(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que 0");
            }

            var usuario = await repositorio.Obtener(Id);
            if (usuario == null)
            {
                throw new InvalidOperationException($"No se encontró ningún usuario con el ID {Id}");
            }

            return await repositorio.Eliminar(Id);
            
        }

        public async Task<Usuario> Modificar(Usuario Usuario)
        {
            if (Usuario == null)
            {
                throw new ArgumentNullException(nameof(Usuario), "El usuario no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(Usuario.NombreUsuario) || string.IsNullOrWhiteSpace(Usuario.Clave))
            {
                throw new ArgumentException("El nombre de usuario y la clave no pueden estar vacíos");
            }

            var usuarioExistente = await repositorio.Obtener(Usuario.Id);
            if (usuarioExistente == null)
            {
                throw new InvalidOperationException("El usuario no existe");
            }

            return await repositorio.Modificar(Usuario);
        }

        public async Task<Usuario> Obtener(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que 0");
            }

            var usuario = await repositorio.Obtener(Id);
            if (usuario == null)
            {
                throw new InvalidOperationException($"No se encontró ningún usuario con el ID {Id}");
            }

            return usuario;
        }

        

        private string GenerarToken(string nombreUsuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, nombreUsuario)
        };

            var token = new JwtSecurityToken(
                issuer: "apiCalendario",
                audience: "apiCalendario",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task<UsuarioDTO> ValidarUsuario(string NombreUsuario, string Clave)
        {
            var usuarioDto = new UsuarioDTO();
            var usuario = await repositorio.ValidarUsuario(NombreUsuario, Clave);
            if (usuario != null)
            {
                usuarioDto.usuario = usuario;
                usuarioDto.token = GenerarToken(NombreUsuario);
            }
            return usuarioDto;
        }

        public Task<IEnumerable<List<UsuarioResponseListDTO>>> ObtenerTodos()
        {
            return Task.FromResult(repositorio.ObtenerTodos().Result.Select(u => new List<UsuarioResponseListDTO>
            {
                new UsuarioResponseListDTO
                {
                    Nombre = u.Nombre,
                    Usuario = u.NombreUsuario,
                    Role = u.Roles
                }
            }));


        }
    }
}
