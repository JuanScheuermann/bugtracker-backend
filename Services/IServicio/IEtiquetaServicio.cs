using backend.DTOs;

namespace backend.Services.IServicio;

public interface IEtiquetaServicio
{
    Task CrearEtiqueta(EtiquetaDto etiquetaDto);

    Task ModificarEtiqueta(EtiquetaDto etiquetaDto);

    Task EliminarEtiqueta(long id);

    Task<List<EtiquetaDto>> ObtenerEtiquetas(long proyectoId, string cadenaBuscar);

    Task<EtiquetaDto?> ObtenerEtiqueta(long id);

    Task<bool> etiquetaExiste(string titulo);
}