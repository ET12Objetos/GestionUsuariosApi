using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacion.Dominio;


[Table("Usuario")]
public class Usuario : Auditoria
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50)]
    public required string NombreCompleto { get; set; }

    [Required]
    [StringLength(50)]
    public required string Nombre { get; set; }

    [Required]
    [StringLength(50)]
    public required string Contraseña { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    public List<Rol> Roles { get; set; } = new List<Rol>();
}