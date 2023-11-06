using backend.Models.enums;

namespace backend.DTOs;

public class ProyectoEditarDto
{
    public long Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public long AutorId { get; set; }

    public EstadoDesarrollo EstadoDesarrollo { get; set; }
}