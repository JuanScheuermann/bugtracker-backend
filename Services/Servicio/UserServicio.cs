using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services.IServicio;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Crypto.Generators;

namespace backend.Services.Servicio;

public class UserServicio : IUserServicio
{
    private readonly DataContext _context;

    public UserServicio(DataContext context)
    {
        _context = context;
    }

    public async Task Crear(UserDto userDto, string token)
    {
        string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);


        User usuario = new User
        {
            Email = userDto.Email,
            PasswordHash = contrasenaHash,
            Nombre = userDto.Nombre,
            Apellido = userDto.Apellido,
            Rol = userDto.Rol,
            Estado = Models.Estado.Activo
        };

        var userToken = new ReiniciarContrasena
        {
            Email = userDto.Email,
            Token = token
        };

        _context.users.Add(usuario);
        _context.reiniciarContrasenas.Add(userToken);

        await _context.SaveChangesAsync();

    }

    public async Task Editar(UserDto userDto)
    {
        var user = new Models.User
        {
            Id = userDto.Id,
            Email = userDto.Email,
            PasswordHash = userDto.Password,
            Nombre = userDto.Nombre,
            Apellido = userDto.Apellido,
            Rol = userDto.Rol,
            Estado = userDto.Estado
        };

        _context.users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserDto> ObtenerPorMail(string email = "")
    {
        var usuario = await _context.users.
       FirstOrDefaultAsync(u => u.Email == email.Trim());

        if (usuario == null) return null;

        return new UserDto
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Password = usuario.PasswordHash,
            Rol = usuario.Rol,
            Estado = usuario.Estado
        };
    }

    public async Task<UserDto> ObtenerPorId(long id)
    {
        var user = await _context.users.AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Nombre = user.Nombre,
            Apellido = user.Apellido,
            Rol = user.Rol,
            Estado = user.Estado,
            Password = user.PasswordHash
        };
    }

    public async Task<List<UserDto>> ObtenerUsuarios(string cadenaBuscar = "")
    {
        return await _context.users.
        Where(x => x.Email.Contains(cadenaBuscar)).
        Select(x => new UserDto
        {
            Id = x.Id,
            Email = x.Email,
            Nombre = x.Nombre,
            Apellido = x.Apellido,
            Rol = x.Rol,
            Estado = x.Estado
        }).ToListAsync();
    }
}