using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.enums;

namespace backend.Models;

public class Proyecto : Base
{
    [Required]
    [MaxLength(50)]
    public string Titulo { get; set; }

    [MaxLength(150)]
    public string? Descripcion { get; set; }

    [Required]
    [ForeignKey("User")]
    public long AutorId { get; set; }

    [Required]
    public EstadoDesarrollo EstadoDesarrollo { get; set; } = 0;

    //props navegacion
    public virtual ICollection<Miembro> Miembros { get; set; }
    public virtual User User { get; set; }
}