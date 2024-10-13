using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacion.Dominio;

[Table("Rol")]
public class Rol : Auditoria
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50)]
    public required string Nombre { get; set; }

    public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
}