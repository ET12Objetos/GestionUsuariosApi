namespace Api.Funcionalidades.Roles;

public class RolQueryDto
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }
}

public class RolCommandDto
{
    public required string Nombre { get; set; }
}