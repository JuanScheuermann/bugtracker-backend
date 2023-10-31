using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectoController : ControllerBase
{
    private readonly IProyectoServicio _proyectoServicio;
    public ProyectoController(IProyectoServicio proyectoServicio)
    {
        _proyectoServicio = proyectoServicio;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> Obtenerproyectos(long id)
    {
        var proyectos = await _proyectoServicio.ObtenerMisProyectos(id);

        return Ok(new
        {
            Proyectos = proyectos
        });
    }

    [HttpPost]
    [Route("proyecto_add")]
    public async Task<ActionResult> CrearProyecto([FromBody] ProyectoDto dto)
    {
        await _proyectoServicio.CrearProyecto(dto);

        return Ok(new
        {
            message = "Proyecto creado correctamenete"
        });
    }

    [HttpPut]
    [Route("proyecto_edit")]
    public async Task<ActionResult> EditarProyecto([FromBody] ProyectoDto dto)
    {
        var proyecto = await _proyectoServicio.Obtener(dto.Id);

        if (proyecto == null)
            return BadRequest(new { message = "No se pudo encontrar el proyecto" });

        await _proyectoServicio.Modificarproyecto(dto);

        return Ok(new
        {
            message = "los cambios se realizaron correctamente"
        });

    }

    [HttpDelete]
    [Route("proyecto_delete")]
    public async Task<ActionResult> EliminarProyecto(long id)
    {
        var proyecto = await _proyectoServicio.Obtener(id);
        if (proyecto == null) return BadRequest(new
        {
            message = "El proyecto no existe"
        });

        await _proyectoServicio.EliminarProyecto(proyecto.Id);
        return Ok(new
        {
            message = "los cambios se realizaron correctamente"
        });
    }
}