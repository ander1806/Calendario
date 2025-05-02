using Calendario.Dominio.DTOS;
using Calendario.Dominio.Entidades;



namespace Calendario.Core.Servicios
{
    public interface IFestivoServicio
    {
        // Obtener todos los festivos
        Task<IEnumerable<Festivo>> ObtenerTodos();

        // Obtener un festivo por su Id
        Task<Festivo> Obtener(int Id);

        // Verificar si una fecha es festivo
        Task<bool> EsFestivo(int anio, int mes, int dia);

       Task<List<FestivoResponseDTO>> ObtenerFestivosPorAnio(int anio);

        // Buscar festivos según un tipo y un dato específico
        Task<IEnumerable<Festivo>> Buscar(int Tipo, string Dato);

        // Agregar un nuevo festivo
        Task<Festivo> Agregar(Festivo festivo);

        // Modificar un festivo existente
        Task<Festivo> Modificar(Festivo festivo);

        // Eliminar un festivo por su Id
        Task<bool> Eliminar(int Id);
    }
}
