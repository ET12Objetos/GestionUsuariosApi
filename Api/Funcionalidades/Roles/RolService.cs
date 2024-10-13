using Api.Persistencia;
using Aplicacion.Dominio;
using Aplicacion.Validaciones;

namespace Api.Funcionalidades.Roles;

public interface IRolService
{
    void CreateRol(RolCommandDto rolDto);
    void DeleteRol(Guid idRol);
    void UpdateRol(Guid idRol, RolCommandDto rolDto);
    List<RolQueryDto> GetRoles();
}

public class RolService : IRolService
{
    private readonly GestionUsuariosDbContext context;

    public RolService(GestionUsuariosDbContext context)
    {
        this.context = context;
    }

    public void CreateRol(RolCommandDto rolDto)
    {
        Guard.ValidarCadena(rolDto.Nombre, "El nombre del rol no puede ser vacío");
        Rol rol = new Rol() { Nombre = rolDto.Nombre };
        context.Roles.Add(rol);
        context.SaveChanges();
    }

    public void DeleteRol(Guid idRol)
    {
        var rol = context.Roles.SingleOrDefault(x => x.Id == idRol);

        if (rol is not null)
        {
            context.Roles.Remove(rol);
            context.SaveChanges();
        }
    }

    public List<RolQueryDto> GetRoles()
    {
        return context.Roles.Select(rol => new RolQueryDto
        {
            Id = rol.Id,
            Nombre = rol.Nombre
        }).ToList();
    }

    public void UpdateRol(Guid idRol, RolCommandDto rolDto)
    {
        var rol = context.Roles.SingleOrDefault(x => x.Id == idRol);

        if (rol is not null)
        {
            rol.Nombre = rolDto.Nombre;
            context.SaveChanges();
        }
    }
}