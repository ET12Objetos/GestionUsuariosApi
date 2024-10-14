using Api.Funcionalidades.Roles;
using Api.Persistencia;
using Aplicacion.Dominio;
using Aplicacion.Validaciones;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Usuarios;

public interface IUsuarioService
{
    List<UsuarioQueryDto> GetUsuarios();
    void CreateUsuario(UsuarioCommandDto usuarioDto);
    void UpdateUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto);
    void DeleteUsuario(Guid idUsuario);
    void AddRolToUsuario(Guid idRol, Guid idUsuario);
    void RemoveRolFromUsuario(Guid idRol, Guid idUsuario);
}

public class UsuarioService : IUsuarioService
{
    private readonly GestionUsuariosDbContext context;

    public UsuarioService(GestionUsuariosDbContext context)
    {
        this.context = context;
    }

    public void AddRolToUsuario(Guid idRol, Guid idUsuario)
    {
        var usuario = context.Usuarios.SingleOrDefault(usuario => usuario.Id == idUsuario);

        var rol = context.Roles.SingleOrDefault(rol => rol.Id == idRol);

        if (usuario is not null && rol is not null)
        {
            usuario.Roles.Add(rol);
            context.SaveChanges();
        }
    }

    public void CreateUsuario(UsuarioCommandDto usuarioDto)
    {
        Guard.ValidarCadena(usuarioDto.Nombre, "El nombre del usuario no puede ser vacío");
        Guard.ValidarCadena(usuarioDto.NombreCompleto, "El Nombre Completo del usuario no puede ser vacío");
        Guard.ValidarCadena(usuarioDto.Email, "El Email del usuario no puede ser vacío");
        Guard.ValidarCadena(usuarioDto.Contraseña, "La Contraseña del usuario no puede ser vacía");

        var usuario = new Usuario
        {
            Nombre = usuarioDto.Nombre,
            NombreCompleto = usuarioDto.NombreCompleto,
            Contraseña = usuarioDto.Contraseña,
            Email = usuarioDto.Email
        };

        context.Usuarios.Add(usuario);
        context.SaveChanges();
    }

    public void DeleteUsuario(Guid idUsuario)
    {
        var usuario = context.Usuarios.SingleOrDefault(usuario => usuario.Id == idUsuario);

        if (usuario is not null)
        {
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
        }
    }

    public List<UsuarioQueryDto> GetUsuarios()
    {
        return context.Usuarios.Select(usuario => new UsuarioQueryDto
        {
            Id = usuario.Id,
            NombreCompleto = usuario.NombreCompleto,
            Nombre = usuario.Nombre,
            Contraseña = usuario.Contraseña,
            Email = usuario.Email,
            Roles = usuario.Roles.Select(rol => new RolQueryDto { Id = rol.Id, Nombre = rol.Nombre }).ToList()
        }).ToList();
    }

    public void RemoveRolFromUsuario(Guid idRol, Guid idUsuario)
    {
        var usuario = context.Usuarios.Include(x => x.Roles).SingleOrDefault(usuario => usuario.Id == idUsuario);

        var rol = context.Roles.SingleOrDefault(rol => rol.Id == idRol);

        if (usuario is not null && rol is not null)
        {
            usuario.Roles.Remove(rol);
            context.SaveChanges();
        }
    }

    public void UpdateUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto)
    {
        var usuario = context.Usuarios.SingleOrDefault(usuario => usuario.Id == idUsuario);

        if (usuario is not null)
        {
            usuario.NombreCompleto = usuarioDto.NombreCompleto;
            usuario.Email = usuarioDto.Email;
            usuario.Contraseña = usuarioDto.Contraseña;

            context.SaveChanges();
        }
    }
}