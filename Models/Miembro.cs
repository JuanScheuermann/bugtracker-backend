using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Miembro : Base
{
    [ForeignKey("Proyecto")]
    public long ProyectoId { get; set; }

    [ForeignKey("User")]
    public long UserId { get; set; }

    //props navegacion 
    public virtual Proyecto Proyecto { get; set; }

    public virtual User Usuarior { get; set; }

    public virtual ICollection<Etiqueta> Etiquetas { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; }
}