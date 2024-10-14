using Api.Funcionalidades.Usuarios;

namespace Api.Funcionalidades.Roles;

public class RolQueryDto
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }
    public List<UsuarioQueryDto> Usuarios { get; set; } = new List<UsuarioQueryDto>();
}

public class RolCommandDto
{
    public required string Nombre { get; set; }
}