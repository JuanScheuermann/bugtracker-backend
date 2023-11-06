using backend.DTOs;

namespace backend.Services.IServicio;

public interface IMiembroServicio
{
    Task AgregarMiembro(MiembroAgregarDto miembroDto);

    Task EliminarMiembro(long id);

    Task<MiembroDto?> ObtenerMiembro(long id);

    Task<List<MiembroDto>> ObtenerMiembros(long proyectoId);
}