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

    public async Task CrearProyecto(ProyectoDto proyectoDto)
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
                    UserId = item.UsuarioId
                };

                _context.Miembros.Add(miembro);
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task EliminarProyecto(long id)
    {
        var proyecto = await _context.Proyectos.FindAsync(id);
        proyecto.Estado = "Eliminado";

        _context.Proyectos.Update(proyecto);
        await _context.SaveChangesAsync();
    }

    public async Task Modificarproyecto(ProyectoDto proyectoDto)
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

    public async Task<List<ProyectoDto>> ObtenerMisProyectos(long id)
    {
        return await _context.Proyectos
        .Include(x => x.Miembros)
        .Where(p => p.AutorId == id)
        .Select(x => new ProyectoDto
        {
            Id = x.Id,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            AutorId = x.AutorId,
            EstadoDesarrollo = x.EstadoDesarrollo,
            Miembros = x.Miembros.Select(m => new MiembroDto
            {
                Id = m.Id,
                ProyectoId = m.ProyectoId,
                UsuarioId = m.UserId
            }).ToList()

        }).ToListAsync();
    }

    public async Task<ProyectoDto> Obtener(long id)
    {
        var proyecto = await _context.Proyectos
        .Include(x => x.Miembros).AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

        return new ProyectoDto
        {
            Id = proyecto.Id,
            Titulo = proyecto.Titulo,
            Descripcion = proyecto.Descripcion,
            AutorId = proyecto.AutorId,
            EstadoDesarrollo = proyecto.EstadoDesarrollo,
            Miembros = proyecto.Miembros
            .Select(m => new MiembroDto
            {
                Id = m.Id,
                ProyectoId = m.ProyectoId,
                UsuarioId = m.UserId

            }).ToList()
        };
    }
}