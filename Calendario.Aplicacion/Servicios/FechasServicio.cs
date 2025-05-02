namespace Calendario.Aplicacion.Servicios
{
    public class FechasServicio
    {
        public static  DateTime ObtenerDomingoPascua(int año)
        {
            int a = año % 19;
            int b = año % 4;
            int c = año % 7;
            int d = (19 * a + 24) % 30;

            int dias = d + (2 * b + 4 * c + 6 * d + 5) % 7;
            int mes = 3;
            int dia = 15 + dias;
            if (dia > 31)
            {
                mes = 4;
                dia = dia - 31;
            }


            return new DateTime(año, mes, dia);
        }

        public static DateTime agregarDias(DateTime fecha, int dias)
        {
            return fecha.AddDays(dias);

        }

        public static DateTime SiguienteLunes(DateTime fecha)
        {
            DayOfWeek diaSemana = fecha.DayOfWeek;
            int diasLunes = (DayOfWeek.Monday - diaSemana + 7) % 7;
            return agregarDias(fecha, diasLunes);
        }



    }
}
