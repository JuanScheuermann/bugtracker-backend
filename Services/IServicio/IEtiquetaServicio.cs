using backend.DTOs;
using backend.Models.enums;

namespace backend.Services.IServicio;

public interface IEtiquetaServicio
{
    Task CrearEtiqueta(EtiquetaDto etiquetaDto);

    Task ModificarEtiqueta(EtiquetaDto etiquetaDto);

    Task EliminarEtiqueta(EtiquetaDto etiqueta);

    Task<List<EtiquetaDto>> ObtenerEtiquetas(long proyectoId, string cadenaBuscar = "", Prioridad prioridad = Prioridad.Ninguna);

    Task<EtiquetaDto?> ObtenerEtiqueta(long id);

    Task<bool> etiquetaExiste(string titulo);
}