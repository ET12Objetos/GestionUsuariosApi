namespace Api.Funcionalidades.Usuarios;

public class UsuarioQueryDto : UsuarioCommandDto
{
    public Guid Id { get; set; }
}

public class UsuarioCommandDto
{
    public required string NombreCompleto { get; set; }
    public required string Nombre { get; set; }
    public required string Contraseña { get; set; }
    public required string Email { get; set; }
}