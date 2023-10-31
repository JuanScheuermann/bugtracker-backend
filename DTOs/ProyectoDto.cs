using backend.Models.enums;

namespace backend.DTOs;
public class ProyectoDto
{
    public long Id { get; set; }

    public string Titulo { get; set; }
    public string? Descripcion { get; set; }

    public long AutorId { get; set; }

    public EstadoDesarrollo EstadoDesarrollo { get; set; }

    public List<MiembroDto>? Miembros { get; set; }

}