using backend.Data;
using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Servicio;

public class ContrasenaServicio : IContrasenaServicio
{
    private readonly DataContext _context;

    public ContrasenaServicio(DataContext context)
    {
        _context = context;
    }

    public async Task<string> Obtener(string Email)
    {
        var token = await _context.reiniciarContrasenas
        .FirstOrDefaultAsync(x => x.Email == Email);

        return token.Token;
    }

    public async Task<UserDto> ObtenerporToken(string token)
    {
        var ser = await _context.reiniciarContrasenas.AsNoTracking()
        .FirstOrDefaultAsync(x => x.Token == token);

        var usuario = await _context.users.AsNoTracking().
        FirstOrDefaultAsync(x => x.Email == ser.Email);

        return new UserDto
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Estado = usuario.Estado,
            Rol = usuario.Rol
        };
    }
}