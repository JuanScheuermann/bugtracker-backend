using System.Security.Claims;
using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProyectoController : ControllerBase
{
    private readonly IProyectoServicio _proyectoServicio;

    /* var claimsIdentity = User.Identity as ClaimsIdentity;
    var userId = claimsIdentity.FindFirst(ClaimTypes.Sid)?.Value; */
    public ProyectoController(IProyectoServicio proyectoServicio)
    {
        _proyectoServicio = proyectoServicio;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult> Obtenerproyectos()
    {
        var proyectos = await _proyectoServicio.ObtenerMisProyectos(ObtenerUserId());
        return Ok(proyectos);
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult> CrearProyecto([FromBody] ProyectoDto dto)
    {
        dto.AutorId = ObtenerUserId();
        var pId = await _proyectoServicio.CrearProyecto(dto);

        return Ok(new
        {
            message = "Proyecto creado correctamenete",
            pId = pId
        });
    }

    [HttpPut]
    [Route("proyecto_edit/{pid}")]
    public async Task<ActionResult> EditarProyecto(long pid, [FromBody] ProyectoEditarDto dto)
    {
        var proyecto = await _proyectoServicio.Obtener(pid);

        if (proyecto == null)
            return BadRequest(new { message = "No se pudo encontrar el proyecto" });

        if (proyecto.AutorId != ObtenerUserId())
            return BadRequest(new { message = "Solo el autor del proyecto puede realizar cambios" });

        dto.Id = proyecto.Id;
        dto.AutorId = proyecto.AutorId;

        await _proyectoServicio.Modificarproyecto(dto);

        return Ok(new
        {
            message = "los cambios se realizaron correctamente"
        });

    }

    [HttpDelete]
    [Route("proyecto_delete/{id}")]
    public async Task<ActionResult> EliminarProyecto(long id)
    {
        var proyecto = await _proyectoServicio.Obtener(id);

        //verificar que el proyecto exista
        if (proyecto == null) return BadRequest(new { message = "El proyecto no existe" });

        //verificar si es el autor del proyecto
        if (ObtenerUserId() != proyecto.AutorId)
            return BadRequest(new { message = "Solo el autor del proyecto puede realizar cambios" });

        //realizar cambios
        await _proyectoServicio.EliminarProyecto(proyecto.Id);
        return Ok(new
        {
            message = "los cambios se realizaron correctamente"
        });
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> ObtenerProyecto(long id)
    {
        var proyecto = await _proyectoServicio.Obtener(id);

        if (proyecto == null) return NotFound(new { message = "Proyecto no encontrado" });

        return Ok(proyecto);
    }

    [HttpGet]
    [Route("participando")]
    public async Task<ActionResult> ObtenerProyectosParticipo()
    {

        var proyectos = await _proyectoServicio.ObtenerProyectosContribucion(ObtenerUserId());
        return Ok(proyectos);
    }

    private long ObtenerUserId()
    {
        var _claimsIdentity = User.Identity as ClaimsIdentity;
        var userId = Convert.ToInt64(_claimsIdentity.FindFirst(ClaimTypes.Sid)?.Value);
        return userId;
    }
}