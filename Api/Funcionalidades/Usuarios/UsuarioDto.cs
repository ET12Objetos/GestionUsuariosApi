using Api.Funcionalidades.Roles;

namespace Api.Funcionalidades.Usuarios;

public class UsuarioQueryDto : UsuarioCommandDto
{
    public Guid Id { get; set; }
    public List<RolQueryDto> Roles { get; set; } = new List<RolQueryDto>();
}

public class UsuarioCommandDto
{
    public required string NombreCompleto { get; set; }
    public required string Nombre { get; set; }
    public required string Contraseña { get; set; }
    public required string Email { get; set; }
}