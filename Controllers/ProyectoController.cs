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
}