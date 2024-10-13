using Api.Funcionalidades.Roles;
using Api.Persistencia;
using Aplicacion.Dominio;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("gestionusuarios_db");

builder.Services.AddDbContext<GestionUsuariosDbContext>(option => option.UseMySql(connectionString, new MySqlServerVersion("8.0.39")));

var options = new DbContextOptionsBuilder<GestionUsuariosDbContext>();

options.UseMySql(connectionString, new MySqlServerVersion("8.0.39"));

var context = new GestionUsuariosDbContext(options.Options);

builder.Services.AddScoped<IRolService, RolService>();

context.Database.EnsureCreated();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGroup("/api")
   .MapRolEndpoints()
   .WithTags("Rol");

app.Run();