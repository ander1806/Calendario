using Calendario.Presentacion.DI;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Cambia esto si tu frontend está en otro dominio
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Agregar esto si necesitas autenticación basada en cookies
    });
});

// Agregar dependencias y otros servicios
var configuracion = builder.Configuration;
builder.Services.AgregarDependencias(configuracion);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración para el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();