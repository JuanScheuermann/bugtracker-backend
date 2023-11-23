using backend.DTOs;

namespace backend.Services.IServicio;

public interface IProyectoServicio
{
    Task<long> CrearProyecto(ProyectoDto proyectoDto);

    Task<ProyectoDto> Modificarproyecto(ProyectoEditarDto proyectoDto);

    Task EliminarProyecto(long id);

    Task<bool> TieneAccesoProyecto(long pId, long userId);

    Task<List<ProyectoInfoDto>> ObtenerMisProyectos(long id, string cadenabuscar = "");

    Task<ProyectoDto?> Obtener(long id);

    Task<List<ProyectoInfoDto>> ObtenerProyectosContribucion(long uid);

}