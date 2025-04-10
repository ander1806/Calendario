using Calendario.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Calendario.Infraestructura.Persistencia.Contexto
{
    public class CalendarioContext : DbContext
    {
        public CalendarioContext(DbContextOptions<CalendarioContext> options) : base(options) { }

        // DbSets para cada entidad
        public DbSet<Festivo> Festivos { get; set; }
        public DbSet<TipoFestivo> TiposFestivo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de TipoFestivo
            modelBuilder.Entity<TipoFestivo>(entidad =>
            {
                entidad.ToTable("Tipo"); // Nombre exacto de la tabla en la base de datos
                entidad.HasKey(e => e.Id); // Clave primaria
                entidad.HasIndex(e => e.Tipo).IsUnique(); // Índice único en el nombre del tipo
            });

            // Configuración de Festivo
            modelBuilder.Entity<Festivo>(entidad =>
            {
                entidad.ToTable("Festivo"); // Nombre exacto de la tabla en la base de datos
                entidad.HasKey(e => e.Id); // Clave primaria
                entidad.HasIndex(e => e.Nombre).IsUnique(); // Índice único en el nombre del festivo

                // Relación entre Festivo y TipoFestivo
                entidad.HasOne(f => f.TipoFestivo) // Un festivo tiene un tipo de festivo
                       .WithMany() // Un tipo de festivo puede tener muchos festivos
                       .HasForeignKey(f => f.IdTipo); // Clave foránea

            });
        }
    }
}