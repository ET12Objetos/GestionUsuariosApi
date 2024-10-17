using Api.Funcionalidades;
using Api.Funcionalidades.Roles;
using Api.Funcionalidades.Usuarios;
using Api.Persistencia;
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

context.Database.Migrate();

builder.Services.AddServiceManager();

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

app.MapGroup("/api")
   .MapUsuarioEndpoints()
   .WithTags("Usuario");

app.Run();