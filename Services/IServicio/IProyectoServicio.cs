using backend.DTOs;

namespace backend.Services.IServicio;

public interface IProyectoServicio
{
    Task CrearProyecto(ProyectoDto proyectoDto);

    Task Modificarproyecto(ProyectoDto proyectoDto);

    Task EliminarProyecto(long id);

    Task<List<ProyectoDto>> ObtenerMisProyectos(long id);

    Task<ProyectoDto> Obtener(long id);

}