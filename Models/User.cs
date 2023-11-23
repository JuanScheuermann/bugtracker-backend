using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("Usuarios")]
public class User : Base
{

    [Required]
    [MaxLength(30)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.EmailAddress)]
    [MaxLength(30)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Rol { get; set; }

    //props navegacion
    public virtual ICollection<Proyecto> Proyectos { get; set; }

    public virtual ICollection<Miembro> Miembros { get; set; }
}