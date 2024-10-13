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
}