namespace Aplicacion.Dominio;

public class Auditoria
{
    public string CreacionUsuario { get; set; } = string.Empty;
    public DateTime CreacionFecha { get; set; } = DateTime.UtcNow;
}