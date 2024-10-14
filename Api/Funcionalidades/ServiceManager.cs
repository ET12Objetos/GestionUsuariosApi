using Api.Funcionalidades.Roles;
using Api.Funcionalidades.Usuarios;

namespace Api.Funcionalidades;

public static class ServiceManager
{
    public static IServiceCollection AddServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IRolService, RolService>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        return services;
    }
}