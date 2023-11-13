using backend.Data;
using backend.DTOs;
using backend.Models.enums;
using backend.Services.IServicio;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Servicio;

public class EtiquetaServicio : IEtiquetaServicio
{
    private readonly DataContext _context;

    public EtiquetaServicio(DataContext context)
    {
        _context = context;
    }

    public async Task CrearEtiqueta(EtiquetaDto etiquetaDto)
    {
        var etiqueta = new Models.Etiqueta
        {
            Titulo = etiquetaDto.Titulo,
            Detalles = etiquetaDto.Detalles,
            EstadoApertura = EstadoApertura.Abierto,
            Prioridad = etiquetaDto.Prioridad,
            ProyectoId = etiquetaDto.ProyectoId,
            MiembroId = etiquetaDto.MiembroId,
            Fecha = DateTime.UtcNow.Date.ToString("d")
        };

        _context.Etiquetas.Add(etiqueta);
        await _context.SaveChangesAsync();
    }


    public async Task EliminarEtiqueta(EtiquetaDto etiquetaDto)
    {
        var etiqueta = new Models.Etiqueta
        {
            Id = etiquetaDto.Id,
            Titulo = etiquetaDto.Titulo,
            Detalles = etiquetaDto.Detalles,
            EstadoApertura = etiquetaDto.EstadoApertura,
            Prioridad = etiquetaDto.Prioridad,
            MiembroId = etiquetaDto.MiembroId,
            ProyectoId = etiquetaDto.ProyectoId,
            Fecha = etiquetaDto.Fecha
        };

        etiqueta.Estado = Models.Estado.Eliminado;

        _context.Etiquetas.Update(etiqueta);
        await _context.SaveChangesAsync();
    }

    public async Task ModificarEtiqueta(EtiquetaDto etiquetaDto)
    {
        var etiqueta = new Models.Etiqueta
        {
            Id = etiquetaDto.Id,
            Titulo = etiquetaDto.Titulo,
            Detalles = etiquetaDto.Detalles,
            EstadoApertura = etiquetaDto.EstadoApertura,
            Prioridad = etiquetaDto.Prioridad,
            MiembroId = etiquetaDto.MiembroId,
            ProyectoId = etiquetaDto.ProyectoId,
            Fecha = etiquetaDto.Fecha
        };

        _context.Etiquetas.Update(etiqueta);
        await _context.SaveChangesAsync();
    }

    public async Task<EtiquetaDto?> ObtenerEtiqueta(long id)
    {

        var etiqueta = await _context.Etiquetas
        .AsNoTracking()
        .Where(x => x.Estado != Models.Estado.Eliminado)
        .FirstOrDefaultAsync(x => x.Id == id);

        if (etiqueta == null) return null;

        return new EtiquetaDto
        {
            Id = etiqueta.Id,
            Titulo = etiqueta.Titulo,
            Detalles = etiqueta.Detalles,
            EstadoApertura = etiqueta.EstadoApertura,
            Prioridad = etiqueta.Prioridad,
            ProyectoId = etiqueta.ProyectoId,
            MiembroId = etiqueta.MiembroId,
            Fecha = etiqueta.Fecha
        };
    }

    public async Task<List<EtiquetaDto>> ObtenerEtiquetas(long proyectoId, string cadenaBuscar)
    {
        /*  var miembros = await _context.Miembros
         .Where(x => x.ProyectoId == proyectoId)
         .FirstOrDefaultAsync(x => x.UserId == userId); */

        /* if (miembros == null) throw new Exception("No autorizado"); */

        return await _context.Etiquetas
        .Where(x => x.ProyectoId == proyectoId
        &&
        x.Titulo.Contains(cadenaBuscar)
        && x.Estado == Models.Estado.Activo)
        .Select(x => new EtiquetaDto
        {
            Id = x.Id,
            Titulo = x.Titulo,
            Detalles = x.Detalles,
            EstadoApertura = x.EstadoApertura,
            Prioridad = x.Prioridad,
            ProyectoId = x.ProyectoId,
            MiembroId = x.MiembroId
        }).ToListAsync();
    }

    public async Task<bool> etiquetaExiste(string titulo)
    {
        var etiquetaExiste = await _context.Etiquetas.
        AsNoTracking()
        .AnyAsync(x => x.Titulo == titulo && x.Estado == Models.Estado.Activo);

        return etiquetaExiste;
    }
}