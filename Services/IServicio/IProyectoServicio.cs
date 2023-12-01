using backend.DTOs;
using backend.Models.enums;

namespace backend.Services.IServicio;

public interface IProyectoServicio
{
    Task<long> CrearProyecto(ProyectoDto proyectoDto);

    Task<ProyectoDto> Modificarproyecto(ProyectoEditarDto proyectoDto);

    Task EliminarProyecto(long id);

    Task<bool> TieneAccesoProyecto(long pId, long userId);

    Task<List<ProyectoInfoDto>> ObtenerMisProyectos(long id, string cadenabuscar = "", EstadoDesarrollo estadoD = EstadoDesarrollo.Ninguno);

    Task<ProyectoDto?> Obtener(long id);

    Task<List<ProyectoInfoDto>> ObtenerProyectosContribucion(long uid, string cadenaBuscar = "", EstadoDesarrollo estadoD = EstadoDesarrollo.Ninguno);

}