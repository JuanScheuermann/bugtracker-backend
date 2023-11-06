using backend.DTOs;

namespace backend.Services.IServicio;

public interface IProyectoServicio
{
    Task<long> CrearProyecto(ProyectoDto proyectoDto);

    Task Modificarproyecto(ProyectoEditarDto proyectoDto);

    Task EliminarProyecto(long id);

    Task<bool> TieneAccesoProyecto(long pId, long userId);

    Task<List<ProyectoInfoDto>> ObtenerMisProyectos(long id);

    Task<ProyectoDto?> Obtener(long id);

    Task<List<ProyectoInfoDto>> ObtenerProyectosContribucion(long uid);

}