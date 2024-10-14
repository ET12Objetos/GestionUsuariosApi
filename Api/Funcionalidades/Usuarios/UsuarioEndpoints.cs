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

        app.MapPost("/usuarios", ([FromServices] IUsuarioService usuarioService, UsuarioCommandDto usuarioDto) =>
        {
            usuarioService.CreateUsuario(usuarioDto);
            return Results.Ok();
        });

        app.MapPut("/usuarios/{idUsuario}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario, UsuarioCommandDto usuarioDto) =>
        {
            usuarioService.UpdateUsuario(idUsuario, usuarioDto);
            return Results.Ok();
        });

         app.MapDelete("/usuarios/{idUsuario}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario) =>
        {
            usuarioService.DeleteUsuario(idUsuario);
            return Results.Ok();
        });

        return app;
    }
}