using backend.Models;
using backend.Models.enums;

namespace backend.DTOs;

public class ProyectoInfoDto
{
    public long Id { get; set; }

    public string Titulo { get; set; }

    public string Descripcion { get; set; }

    public long AutorId { get; set; }

    public string AutorNombre { get; set; }

    public EstadoDesarrollo EstadoDesarrollo { get; set; }

    public string Estado { get; set; }

}