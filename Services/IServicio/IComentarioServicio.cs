using backend.DTOs;

namespace backend.Services.IServicio;

public interface IComentarioServicio
{
    Task<ComentarioDto> AgregarComentario(ComentarioDto comentarioDto);

    Task<ComentarioDto> ModificarComentario(ComentarioModificarDto comentarioDto);

    Task EliminarComentario(long cid);

    Task<List<ComentarioDto>> ObtenerComentarios(long eid);
}