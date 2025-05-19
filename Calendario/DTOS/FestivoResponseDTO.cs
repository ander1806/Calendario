namespace Calendario.Dominio.DTOS
{
    public class FestivoResponseDTO
    {
        public string Nombre { get; set; }
        public string Fecha { get; set; }

        public FestivoResponseDTO() 
        {
        }

        public FestivoResponseDTO(string nombre, string fecha)
        {
            Nombre = nombre;
            Fecha = fecha;
        }

    }
}
