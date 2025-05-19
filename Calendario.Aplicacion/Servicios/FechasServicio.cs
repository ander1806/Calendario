namespace Calendario.Aplicacion.Servicios
{
    public class FechasServicio
    {
        public static DateTime ObtenerDomingoPascua(int año)
        {
            // Algoritmo de Gauss para calcular la Pascua
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

            var fecha = new DateTime(año, mes, dia);

            // Validación adicional para asegurar que es el domingo correcto
            if (fecha.DayOfWeek != DayOfWeek.Sunday)
            {
                throw new Exception("Error en el cálculo de la Pascua: la fecha calculada no es domingo");
            }

            return fecha;
        }

        public static DateTime SiguienteLunes(DateTime fecha)
        {
            if (fecha.DayOfWeek == DayOfWeek.Sunday)
            {
                return fecha.AddDays(1);
            }
            return fecha;
        }
    }
}
