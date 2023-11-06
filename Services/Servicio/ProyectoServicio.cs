using System.Transactions;
using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services.IServicio;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Servicio;

public class ProyectoServicio : IProyectoServicio
{
    private readonly DataContext _context;

    public ProyectoServicio(DataContext context)
    {
        _context = context;
    }

    public async Task<long> CrearProyecto(ProyectoDto proyectoDto)
    {

        var proyecto = new backend.Models.Proyecto
        {
            Titulo = proyectoDto.Titulo,
            Descripcion = proyectoDto.Descripcion,
            AutorId = proyectoDto.AutorId,
            EstadoDesarrollo = Models.enums.EstadoDesarrollo.Desarrollo,
        };

        _context.Proyectos.Add(proyecto);
        await _context.SaveChangesAsync();

        if (proyectoDto.Miembros.Count > 0)
        {

            foreach (var item in proyectoDto.Miembros)
            {
                var miembro = new Models.Miembro
                {
                    ProyectoId = proyecto.Id,
                    UserId = item.UsuarioId,
                    Estado = Models.Estado.Activo
                };

                _context.Miembros.Add(miembro);
            }

        }

        //agregar al autor siempre como miembro
        _context.Miembros.Add(new Miembro
        {
            ProyectoId = proyecto.Id,
            UserId = proyectoDto.AutorId,
            Estado = Models.Estado.Activo
        });
        await _context.SaveChangesAsync();

        return proyecto.Id;
    }

    public async Task EliminarProyecto(long id)
    {
        var proyecto = await _context.Proyectos.FindAsync(id);
        proyecto.Estado = "Eliminado";

        _context.Proyectos.Update(proyecto);
        await _context.SaveChangesAsync();
    }

    public async Task Modificarproyecto(ProyectoEditarDto proyectoDto)
    {

        var proyecto = new Models.Proyecto
        {
            Id = proyectoDto.Id,
            Titulo = proyectoDto.Titulo,
            Descripcion = proyectoDto.Descripcion,
            AutorId = proyectoDto.AutorId,
            EstadoDesarrollo = proyectoDto.EstadoDesarrollo
        };

        _context.Proyectos.Update(proyecto);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProyectoInfoDto>> ObtenerMisProyectos(long id)
    {
        return await _context.Proyectos
        .Include(x => x.Miembros)
        .Where(p => p.AutorId == id)
        .Select(x => new ProyectoInfoDto
        {
            Id = x.Id,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            AutorId = x.AutorId,
            AutorNombre = $"{x.User.Nombre} {x.User.Apellido}",
            EstadoDesarrollo = x.EstadoDesarrollo,
            Estado = x.Estado
            /* Miembros = x.Miembros.Select(m => new MiembroDto
            {
                Id = m.Id,
                ProyectoId = m.ProyectoId,
                UsuarioId = m.UserId
            }).ToList() */

        }).ToListAsync();
    }

    public async Task<ProyectoDto?> Obtener(long id)
    {
        var proyecto = await _context.Proyectos
        .Include(x => x.Miembros)
        .Include(x => x.User)
        .AsNoTracking()
        .Where(x => x.Estado == Models.Estado.Activo)
        .FirstOrDefaultAsync(x => x.Id == id);

        if (proyecto == null) return null;

        return new ProyectoDto
        {
            Id = proyecto.Id,
            Titulo = proyecto.Titulo,
            Descripcion = proyecto.Descripcion,
            AutorId = proyecto.AutorId,
            EstadoDesarrollo = proyecto.EstadoDesarrollo,
            AutorNombre = proyecto.User.Email,
            Miembros = proyecto.Miembros
             .Select(m => new MiembroDto
             {
                 Id = m.Id,
                 ProyectoId = m.ProyectoId,
                 UsuarioId = m.UserId,
                 Estado = m.Estado

             }).ToList()
        };
    }

    public async Task<bool> TieneAccesoProyecto(long pId, long userId)
    {
        var miembros = await _context
        .Miembros
        .AsNoTracking()
        .Where(x => x.ProyectoId == pId)
        .Select(x => x)
        .ToArrayAsync();

        foreach (var miembro in miembros)
        {
            if (miembro.UserId == userId) return true;
        }

        return false;
    }

    public async Task<List<ProyectoInfoDto>> ObtenerProyectosContribucion(long uid)
    {
        var miembros = await _context.Miembros
        .Include(x => x.Proyecto)
        .Where(x => x.UserId == uid && x.Proyecto.AutorId != uid)
        .Select(x => x.ProyectoId).ToArrayAsync();

        return await _context.Proyectos
        .Include(x => x.User)
        .Where(x => miembros.Contains(x.Id))
        .Select(x => new ProyectoInfoDto
        {
            Id = x.Id,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            EstadoDesarrollo = x.EstadoDesarrollo,
            AutorId = x.AutorId,
            AutorNombre = x.User.Email,
            Estado = x.Estado
        }).ToListAsync();
    }
}