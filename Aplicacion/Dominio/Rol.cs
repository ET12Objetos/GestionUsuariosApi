namespace Aplicacion.Dominio;

public class Rol : Auditoria
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Nombre { get; set; }

    public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
}