using System.Net;
using backend.Data;
using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Servicio;

public class MiembroServicio : IMiembroServicio
{

    private readonly DataContext _context;

    public MiembroServicio(DataContext context)
    {
        _context = context;
    }

    public async Task AgregarMiembros(List<MiembroAgregarDto> miembrosDto)
    {
        foreach (var miembroDto in miembrosDto)
        {
            var miembro = new Models.Miembro
            {
                UserId = miembroDto.UsuarioId,
                ProyectoId = miembroDto.ProyectoId,
                Estado = Models.Estado.Activo
            };

            _context.Miembros.Add(miembro);
        }

        await _context.SaveChangesAsync();
    }

    public async Task EliminarMiembro(long id)
    {
        var miembro = await _context.Miembros
        .Include(x => x.Proyecto)
        .FirstOrDefaultAsync(x => x.Id == id);

        if (miembro == null)
            throw new Exception("miembro no encontrado");

        if (miembro.Proyecto.AutorId == miembro.UserId)
            throw new Exception("No se puede eliminar al autor del proyecto");

        miembro.Estado = Models.Estado.Eliminado;

        _context.Miembros.Update(miembro);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MiembroDto>> ObtenerMiembros(long proyectoId)
    {
        return await _context.Miembros
        .Include(x => x.Usuarior)
        .Where(x => x.ProyectoId == proyectoId && x.Estado == Models.Estado.Activo)
        .Select(x => new MiembroDto
        {
            Id = x.Id,
            ProyectoId = x.ProyectoId,
            UsuarioId = x.UserId,
            Email = x.Usuarior.Email,
            ApiNom = $"{x.Usuarior.Nombre} {x.Usuarior.Apellido}",
            Estado = x.Estado

        }).ToListAsync();
    }

    public async Task<MiembroDto?> ObtenerMiembro(long id)
    {
        var miembro = await _context.Miembros
        .Include(x => x.Usuarior)
        .FirstOrDefaultAsync(x => x.Id == id);

        if (miembro == null) return null;

        return new MiembroDto
        {
            Id = miembro.Id,
            ProyectoId = miembro.ProyectoId,
            UsuarioId = miembro.UserId,
            ApiNom = $"{miembro.Usuarior.Nombre}  {miembro.Usuarior.Apellido}",
            Email = miembro.Usuarior.Email,
            Estado = miembro.Estado
        };
    }
}