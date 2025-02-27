using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}