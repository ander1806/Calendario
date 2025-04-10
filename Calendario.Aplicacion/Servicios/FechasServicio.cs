namespace Calendario.Aplicacion.Servicios
{
    public class FechasServicio
    {
        public DateTime ObtenerDomingoPascua(int año)
        {
            // Algoritmo gregoriano para cálculo preciso de Pascua
            int a = año % 19;
            int b = año / 100;
            int c = año % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;

            int mes = (h + l - 7 * m + 114) / 31;
            int dia = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateTime(año, mes, dia);
        }

        public DateTime CalcularFechaFestivo(int año, int diasPascua)
        {
            DateTime domingoPascua = ObtenerDomingoPascua(año);
            return domingoPascua.AddDays(diasPascua);
        }

        public DateTime SiguienteLunes(DateTime fecha)
        {
            int diasHastaLunes = ((int)DayOfWeek.Monday - (int)fecha.DayOfWeek + 7) % 7;
            return diasHastaLunes == 0 ? fecha.AddDays(7) : fecha.AddDays(diasHastaLunes);
        }



    }
}
