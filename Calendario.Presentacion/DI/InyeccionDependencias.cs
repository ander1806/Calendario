using Calendario.Aplicacion.Servicios;
using Calendario.Core.Repositorios;
using Calendario.Core.Servicios;
using Calendario.Infraestructura.Persistencia.Contexto;
using Calendario.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Calendario.Presentacion.DI
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AgregarDependencias(this IServiceCollection servicios,
                                                           IConfiguration configuracion)
        {
            servicios.AddDbContext<CalendarioContext>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("Festivos")));

            // Repositorios
            servicios.AddTransient<IFestivoRepositorio, FestivoRepositorio>();
            servicios.AddTransient<ITipoFestivoRepositorio, TipoFestivoRepositorio>();
            servicios.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();




            // Servicios
            servicios.AddTransient<IFestivoServicio, FestivoServicio>();
            servicios.AddTransient<ITipoFestivoServicio, TipoFestivoServicio>();
            servicios.AddTransient<FechasServicio>();
            servicios.AddTransient<IUsuarioServicio, UsuarioServicio>();


            return servicios;
        }
    }
}
