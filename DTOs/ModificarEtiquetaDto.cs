using backend.Models.enums;

namespace backend.DTOs;


public class ModificarEtiquetaDto
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;

    public string Detalles { get; set; } = string.Empty;

    public EstadoApertura EstadoApertura { get; set; }

    public Prioridad Prioridad { get; set; }

    public string Estado { get; set; } = string.Empty;
}