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

    public Task EliminarProyecto(long id)
    {
        throw new NotImplementedException();
    }

    public async Task Modificarproyecto(ProyectoDto proyectoDto)
    {

        var proyecto = new Models.Proyecto
        {
            Id = proyectoDto.Id,
            Titulo = proyectoDto.Titulo,
            Descripcion = proyectoDto.Descripcion,
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
            EstadoDesarrollo = x.EstadoDesarrollo,
            Miembros = x.Miembros.Select(m => new MiembroDto
            {
                Id = m.Id,
                ProyectoId = m.ProyectoId,
                UsuarioId = m.UserId
            }).ToList()

        }).ToListAsync();
    }

    public Task<ProyectoDto> Obtener(long id)
    {
        throw new NotImplementedException();
    }
}