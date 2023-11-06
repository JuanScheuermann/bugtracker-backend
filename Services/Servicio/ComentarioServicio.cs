using backend.Data;
using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Servicio;

public class ComentarioServicio : IComentarioServicio
{
    private readonly DataContext _context;

    public ComentarioServicio(DataContext context)
    {
        _context = context;
    }

    public async Task<ComentarioDto> AgregarComentario(ComentarioDto comentarioDto)
    {
        var comentario = new Models.Comentario
        {
            Cuerpo = comentarioDto.Cuerpo,
            Fecha = DateTime.Now,
            MiembroId = comentarioDto.MiembroId,
            EtiquetaId = comentarioDto.EtiquetaId,
            Estado = Models.Estado.Activo
        };

        _context.Comentarios.Add(comentario);
        await _context.SaveChangesAsync();

        return new ComentarioDto
        {
            Id = comentario.Id,
            Cuerpo = comentario.Cuerpo,
            Fecha = comentario.Fecha,
            MiembroId = comentario.MiembroId,
            EtiquetaId = comentario.EtiquetaId,
            Estado = comentario.Estado,
            MiembroStr = comentarioDto.MiembroStr
        };
    }

    public async Task EliminarComentario(long cid)
    {
        var comentario = await _context.Comentarios.FindAsync(cid);
        if (comentario == null) throw new Exception("Comentario No encontrado");

        comentario.Estado = Models.Estado.Eliminado;
        _context.Comentarios.Update(comentario);
        await _context.SaveChangesAsync();
    }

    public async Task<ComentarioDto> ModificarComentario(ComentarioModificarDto comentarioDto)
    {
        var comentario = await _context.Comentarios.FindAsync(comentarioDto.Id);
        if (comentario == null) throw new Exception("Comentario No encontrado");

        comentario.Cuerpo = comentarioDto.Cuerpo;

        _context.Comentarios.Update(comentario);
        await _context.SaveChangesAsync();

        return new ComentarioDto
        {
            Id = comentario.Id,
            Cuerpo = comentario.Cuerpo,
            Fecha = comentario.Fecha,
            MiembroId = comentario.MiembroId,
            EtiquetaId = comentario.EtiquetaId,
            Estado = comentario.Estado
        };
    }

    public Task<List<ComentarioDto>> ObtenerComentarios(long eid)
    {
        return _context.Comentarios
        .Include(x => x.Miembro.Usuarior)
        .Where(x => x.EtiquetaId == eid)
        .Select(x => new ComentarioDto
        {
            Id = x.Id,
            Cuerpo = x.Cuerpo,
            Fecha = x.Fecha,
            MiembroId = x.MiembroId,
            EtiquetaId = x.EtiquetaId,
            MiembroStr = x.Miembro.Usuarior.Email,
            Estado = x.Estado
        }).ToListAsync();
    }
}