using Aplicacion.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistencia;

public class GestionUsuariosDbContext : DbContext
{
    public GestionUsuariosDbContext(DbContextOptions<GestionUsuariosDbContext> options) : base(options)
    {

    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Usuario>().HasData(
            new Usuario { NombreCompleto = "Juan Perez", Nombre = "Juan123", Contraseña = "pass123" },
            new Usuario { NombreCompleto = "Pablo Lopez", Nombre = "Pablo123", Contraseña = "pass123" }
        );

        builder.Entity<Rol>().HasData(
            new Rol { Nombre = "Administrador" },
            new Rol { Nombre = "Vendedor" },
            new Rol { Nombre = "Legales" }
        );
    }
}