namespace Calendario.Dominio.Entidades
{
    public class Festivo
    {
        public int Id { get; set; } // Identificador único del festivo
        public string Nombre { get; set; } // Nombre del festivo
        public int Dia { get; set; } // Día del festivo
        public int Mes { get; set; } // Mes del festivo       
        public int DiasPascua { get; set; } // Días relativos al Domingo de Pascua
        public int IdTipo { get; set; } // Clave foránea que referencia al tipo de festivo
        public TipoFestivo TipoFestivo { get; set; } // Navegación al tipo de festivo

        // Constructor por defecto
        public Festivo()
        {
            
        }
        
        public Festivo(int id, string nombre, int dia, int mes, int diasPascua, int idTipo)
        {
            Id = id;
            Nombre = nombre;
            Dia = dia;
            Mes = mes;
            DiasPascua = diasPascua;
            IdTipo = idTipo;
        }

    }
}