using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendario.Dominio.Entidades
{
    public class TipoFestivo
    {
        public int Id { get; set; } // Identificador único del tipo de festivo
        public string Tipo { get; set; } // Nombre del tipo de festivo
    }
}