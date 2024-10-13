namespace Aplicacion.Dominio;

public class Usuario : Auditoria
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string NombreCompleto { get; set; }

    public required string Nombre { get; set; }

    public required string Contraseña { get; set; }

    public string Email { get; set; } = string.Empty;

    public List<Rol> Roles { get; set; } = new List<Rol>();
}