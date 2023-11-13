using backend.DTOs;
using backend.Models;
using backend.Services.IServicio;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentarioController : ControllerBase
{

    private readonly IComentarioServicio _comentarioServicio;
    private readonly IEtiquetaServicio _etiquetaServicio;

    public ComentarioController(IComentarioServicio comentarioServicio,
    IEtiquetaServicio etiquetaServicio)
    {
        _comentarioServicio = comentarioServicio;
        _etiquetaServicio = etiquetaServicio;
    }

    [HttpPost]
    [Route("{eid}/agregar")]
    public async Task<ActionResult> AgregarCometaio(long eid, ComentarioDto comentarioDto)
    {
        if (await _etiquetaServicio.ObtenerEtiqueta(eid) == null)
            return NotFound();

        comentarioDto.EtiquetaId = eid;

        var comentario = await _comentarioServicio
        .AgregarComentario(comentarioDto);

        return Ok(comentario);
    }

    [HttpGet]
    [Route("{eid}")]
    public async Task<ActionResult> ObtenerComentarios(long eid)
    {
        if (await _etiquetaServicio.ObtenerEtiqueta(eid) == null)
            return NotFound();

        var comentarios = await _comentarioServicio.ObtenerComentarios(eid);

        return Ok(comentarios);
    }

    [HttpPut]
    [Route("{eid}/mod_comentario/{cid}")]

    public async Task<ActionResult> ModificarComentario(long eid, long cid, ComentarioModificarDto dto)
    {
        if (await _etiquetaServicio.ObtenerEtiqueta(eid) == null)
            return NotFound();

        dto.Id = cid;
        var comentario = await _comentarioServicio.ModificarComentario(dto);

        return Ok(comentario);
    }

    [HttpDelete]
    [Route("{eid}/del_comentario/{cid}")]

    public async Task<ActionResult> EliminarComentario(long eid, long cid)
    {
        if (await _etiquetaServicio.ObtenerEtiqueta(eid) == null)
            return NotFound();

        await _comentarioServicio.EliminarComentario(cid);

        return Ok(new { message = "Comentario eliminado" });

    }
}