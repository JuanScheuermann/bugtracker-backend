using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.enums;

namespace backend.Models;

public class Etiqueta : Base
{
    [Required]
    [MaxLength(70)]
    public string? Titulo { get; set; }

    [Required]
    [MaxLength(250)]
    public string? Detalles { get; set; }

    [Required]
    public EstadoApertura EstadoApertura { get; set; }

    [Required]
    public Prioridad Prioridad { get; set; }

    [Required]
    public string? Fecha { get; set; }

    [ForeignKey("Proyecto")]
    public long ProyectoId { get; set; }

    [ForeignKey("Miembro")]
    public long MiembroId { get; set; }

    //props navegacion
    public virtual Miembro Miembro { get; set; }

    public virtual Proyecto Proyecto { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; }
}