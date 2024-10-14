using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Usuarios;

public static class UsuarioEndpoints
{
    public static RouteGroupBuilder MapUsuarioEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/usuarios", ([FromServices] IUsuarioService usuarioService) =>
        {
            var usuarios = usuarioService.GetUsuarios();
            return Results.Ok(usuarios);
        });

        app.MapPost("/usuario", ([FromServices] IUsuarioService usuarioService, UsuarioCommandDto usuarioDto) =>
        {
            usuarioService.CreateUsuario(usuarioDto);
            return Results.Ok();
        });

        app.MapPut("/usuario/{idUsuario}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario, UsuarioCommandDto usuarioDto) =>
        {
            usuarioService.UpdateUsuario(idUsuario, usuarioDto);
            return Results.Ok();
        });

        app.MapDelete("/usuario/{idUsuario}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario) =>
        {
            usuarioService.DeleteUsuario(idUsuario);
            return Results.Ok();
        });

        app.MapPost("/usuario/{idUsuario}/rol/{idRol}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario, Guid idRol) =>
        {
            usuarioService.AddRolToUsuario(idRol, idUsuario);
            return Results.Ok();
        });

        app.MapDelete("/usuario/{idUsuario}/rol/{idRol}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario, Guid idRol) =>
        {
            usuarioService.RemoveRolFromUsuario(idRol, idUsuario);
            return Results.Ok();
        });

        return app;
    }
}