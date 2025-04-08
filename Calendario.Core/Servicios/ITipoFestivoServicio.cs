using Calendario.Dominio.Entidades;

namespace Calendario.Core.Servicios
{
    public interface ITipoFestivoServicio
    {
        // Obtener todos los tipos de festivo
        Task<IEnumerable<TipoFestivo>> ObtenerTodos();

        // Obtener un tipo de festivo por su Id
        Task<TipoFestivo> Obtener(int Id);

        // Buscar tipos de festivo según un tipo y un dato específico
        Task<IEnumerable<TipoFestivo>> Buscar(int Tipo, string Dato);

        // Agregar un nuevo tipo de festivo
        Task<TipoFestivo> Agregar(TipoFestivo tipoFestivo);

        // Modificar un tipo de festivo existente
        Task<TipoFestivo> Modificar(TipoFestivo tipoFestivo);

        // Eliminar un tipo de festivo por su Id
        Task<bool> Eliminar(int Id);
    }
}
