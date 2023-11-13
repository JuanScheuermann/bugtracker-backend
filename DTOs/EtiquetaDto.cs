using backend.Models.enums;

namespace backend.DTOs;

public class EtiquetaDto
{
    public long Id { get; set; }

    public string Titulo { get; set; }

    public string Detalles { get; set; }

    public EstadoApertura EstadoApertura { get; set; }

    public Prioridad Prioridad { get; set; }

    public string? AutorStr { get; set; }

    public string? Fecha { get; set; }

    public long ProyectoId { get; set; }

    public long MiembroId { get; set; }

}