using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MiembroController : ControllerBase
{

    private readonly IMiembroServicio _miembroServicio;
    private readonly IProyectoServicio _proyectoServicio;

    public MiembroController(IMiembroServicio miembroServicio, IProyectoServicio proyectoServicio)
    {
        _miembroServicio = miembroServicio;
        _proyectoServicio = proyectoServicio;
    }

    [HttpPost]
    [Route("{pid}/miembro_add")]
    public async Task<ActionResult> AgregarMiembro(long pid, [FromBody] List<MiembroAgregarDto> miembrosDto)
    {

        var proyectoActual = await _proyectoServicio.Obtener(pid);

        if (proyectoActual == null) return NotFound();

        /* foreach (var miembro in miembrosDto)
        {
            miembro.ProyectoId = pid;
        } */

        await _miembroServicio.AgregarMiembros(miembrosDto);

        return Ok(new
        {
            message = $"Nuevo/s miembro/s agregado/s"
        });
    }

    [HttpGet]
    [Route("{pid}/obtener_miembro/{id}")]
    public async Task<ActionResult> ObtenerMiembro(long pid, long id)
    {
        var proyectoActual = await _proyectoServicio.Obtener(pid);

        if (proyectoActual == null)
            return NotFound();

        var miembro = await _miembroServicio.ObtenerMiembro(id);

        if (miembro == null) return NotFound();

        return Ok(miembro);
    }

    [HttpDelete]
    [Route("{pid}/eliminar_miembro/{id}")]

    public async Task<ActionResult> EliminarMiembro(long pid, long id)
    {
        var proyectoActual = await _proyectoServicio.Obtener(pid);

        if (proyectoActual == null)
            return NotFound();

        await _miembroServicio.EliminarMiembro(id);
        return Ok(new
        {
            message = "Miembro eliminado correctamente"
        });
    }

    [HttpGet]
    [Route("{pid}")]
    public async Task<ActionResult> ObtenerMiembros(long pid)
    {
        var proyectoActual = await _proyectoServicio.Obtener(pid);

        if (proyectoActual == null)
            return NotFound();

        var miembros = await _miembroServicio.ObtenerMiembros(proyectoActual.Id);

        return Ok(miembros);
    }
    /* [HttpGet]
    [Route("{pid}/obtener_miembros")]
    public async Task<ActionResult> ObtenerMiembros(long id)
    {
        var miembros = await _miembroServicio.ObtenerMiembros(id);
        return Ok(miembros);
    } */
}