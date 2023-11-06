using System.Net.Http;
using System.Security.Claims;
using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EtiquetaController : ControllerBase
{
    private readonly IEtiquetaServicio _etiquetaServicio;
    private readonly IProyectoServicio _proyectoServicio;


    public EtiquetaController(IEtiquetaServicio etiquetaServicio,
        IProyectoServicio proyectoServicio
    )
    {
        _etiquetaServicio = etiquetaServicio;
        _proyectoServicio = proyectoServicio;
    }

    [HttpGet]
    [Route("{id}/all")]
    public async Task<ActionResult> ObtenerEtiquetas(long id, string cadenaBuscar = "")
    {
        var etiquetas = await _etiquetaServicio.ObtenerEtiquetas(id, cadenaBuscar);

        return Ok(etiquetas);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> ObtenerEtiqueta(long id)
    {

        var etiqueta = await _etiquetaServicio.ObtenerEtiqueta(id);

        if (etiqueta == null) return NotFound(new
        {
            message = "etiqueta no encontrada"
        });

        return Ok(etiqueta);
    }

    [HttpPost]
    [Route("etiqueta_add")]
    public async Task<ActionResult> AgregarEtiqueta([FromBody] EtiquetaDto etiquetaDto)
    {
        var userId = ObtenerUserId();

        //obtener proyecto
        var proyecto = await _proyectoServicio.Obtener(etiquetaDto.ProyectoId);
        if (proyecto == null) return NotFound();

        //verificar acceso
        if (await _proyectoServicio
        .TieneAccesoProyecto(etiquetaDto.ProyectoId, userId) == false) return Unauthorized();

        //verificar si la etiqueta ya existe
        var etiquetaExiste = await _etiquetaServicio.etiquetaExiste(etiquetaDto.Titulo);
        if (etiquetaExiste) return BadRequest(new { message = "Ya existe esta etiqueta" });


        var miembro = proyecto.Miembros.FirstOrDefault(x => x.UsuarioId == userId);

        etiquetaDto.MiembroId = miembro.Id;
        await _etiquetaServicio.CrearEtiqueta(etiquetaDto);
        return Ok(new
        {
            message = "Etiqueta creada correctamente"
        });
    }

    [HttpPut]
    [Route("etiqueta_mod/{id}")]
    public async Task<ActionResult> ModificarEtiqueta(long id, [FromBody] EtiquetaDto etiquetaDto)
    {
        var etiqueta = await _etiquetaServicio.ObtenerEtiqueta(id);

        if (etiqueta == null) return BadRequest(new
        {
            message = "etiqueta inexistente"
        });

        await _etiquetaServicio.ModificarEtiqueta(etiquetaDto);

        return Ok(new
        {
            message = "Cambios realizados correctamente"
        });
    }

    [HttpDelete]
    [Route("etiqueta_del/{id}")]
    public async Task<ActionResult> CerrarEtiqueta(long id)
    {
        await _etiquetaServicio.EliminarEtiqueta(id);
        return Ok();
    }

    private long ObtenerUserId()
    {
        var _claimsIdentity = User.Identity as ClaimsIdentity;
        var userId = Convert.ToInt64(_claimsIdentity.FindFirst(ClaimTypes.Sid)?.Value);
        return userId;
    }

}