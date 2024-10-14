using Api.Persistencia;
using Aplicacion.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Roles;

public static class RolEndpoints
{
    public static RouteGroupBuilder MapRolEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/roles", ([FromServices] IRolService rolService) =>
        {
            var roles = rolService.GetRoles();
            return Results.Ok(roles);
        });

        app.MapPost("/roles", ([FromServices] IRolService rolService, RolCommandDto rolDto) =>
        {
            rolService.CreateRol(rolDto);
            return Results.Ok();
        });

        app.MapPut("/roles/{idRol}", ([FromServices] IRolService rolService, Guid idRol, RolCommandDto rolDto) =>
        {
            rolService.UpdateRol(idRol, rolDto);
            return Results.Ok();
        });

        app.MapDelete("/roles/{idRol}", ([FromServices] IRolService rolService, Guid idRol) =>
        {
            rolService.DeleteRol(idRol);
            return Results.Ok();
        });

        app.MapPost("/rol/{idRol}/usuario/{idUsuario}", ([FromServices] IRolService rolService, Guid idRol, Guid idUsuario) =>
        {
            rolService.AddUsuarioToRol(idUsuario, idRol);
            return Results.Ok();
        });

        app.MapDelete("/rol/{idRol}/usuario/{idUsuario}", ([FromServices] IRolService rolService, Guid idRol, Guid idUsuario) =>
        {
            rolService.RemoveUsuarioFromRol(idUsuario, idRol);
            return Results.Ok();
        });

        return app;
    }
}