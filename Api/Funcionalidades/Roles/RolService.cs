using Api.Funcionalidades.Usuarios;
using Api.Persistencia;
using Aplicacion.Dominio;
using Aplicacion.Validaciones;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Roles;

public interface IRolService
{
    void CreateRol(RolCommandDto rolDto);
    void DeleteRol(Guid idRol);
    void UpdateRol(Guid idRol, RolCommandDto rolDto);
    List<RolQueryDto> GetRoles();
    void AddUsuarioToRol(Guid idUsuario, Guid idRol);
    void RemoveUsuarioFromRol(Guid idUsuario, Guid idRol);
}

public class RolService : IRolService
{
    private readonly GestionUsuariosDbContext context;

    public RolService(GestionUsuariosDbContext context)
    {
        this.context = context;
    }

    public void AddUsuarioToRol(Guid idUsuario, Guid idRol)
    {
        var usuario = context.Usuarios.SingleOrDefault(usuario => usuario.Id == idUsuario);

        var rol = context.Roles.SingleOrDefault(rol => rol.Id == idRol);

        if (usuario is not null && rol is not null)
        {
            rol.Usuarios.Add(usuario);
            context.SaveChanges();
        }
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
            Nombre = rol.Nombre,
            Usuarios = rol.Usuarios.Select(usuario => new UsuarioQueryDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                NombreCompleto = usuario.NombreCompleto,
                Contraseña = usuario.Contraseña,
                Email = usuario.Email
            }).ToList()
        }).ToList();
    }

    public void RemoveUsuarioFromRol(Guid idUsuario, Guid idRol)
    {
        var usuario = context.Usuarios.SingleOrDefault(usuario => usuario.Id == idUsuario);

        var rol = context.Roles.Include(x => x.Usuarios).SingleOrDefault(rol => rol.Id == idRol);

        if (usuario is not null && rol is not null)
        {
            rol.Usuarios.Remove(usuario);
            context.SaveChanges();
        }
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