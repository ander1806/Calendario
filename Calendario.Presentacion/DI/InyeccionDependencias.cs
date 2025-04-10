using Calendario.Aplicacion.Servicios;
using Calendario.Core.Repositorios;
using Calendario.Core.Servicios;
using Calendario.Infraestructura.Persistencia.Contexto;
using Calendario.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calendario.Presentacion.DI
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AgregarDependencias(this IServiceCollection servicios,
                                                           IConfiguration configuracion)
        {
            servicios.AddDbContext<CalendarioContext>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("Calendario")));

            // Repositorios
            servicios.AddTransient<IFestivoRepositorio, FestivoRepositorio>();
            servicios.AddTransient<ITipoFestivoRepositorio, ITipoFestivoRepositorio>();
            


            // Servicios
            servicios.AddTransient<IFestivoServicio, FestivoServicio>();
            servicios.AddTransient<ITipoFestivoServicio, TipoFestivoServicio>();

            return servicios;
        }
    }
}
