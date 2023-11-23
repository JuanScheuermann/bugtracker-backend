using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

namespace backend.Models;
public class Comentario : Base
{
    [Required]
    [MaxLength(300)]
    public string Cuerpo { get; set; }

    [Required]
    public string? Fecha { get; set; }

    [Required]
    [ForeignKey("Miembro")]
    public long MiembroId { get; set; }

    [Required]
    [ForeignKey("Etiqueta")]
    public long EtiquetaId { get; set; }

    //props navegacion
    public virtual Miembro Miembro { get; set; }
    public virtual Etiqueta Etiqueta { get; set; }
}