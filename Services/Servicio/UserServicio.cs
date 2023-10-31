using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services.IServicio;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Servicio;

public class UserServicio : IUserServicio
{
    private readonly DataContext _context;

    public UserServicio(DataContext context)
    {
        _context = context;
    }

    public async Task Crear(UserDto userDto)
    {
        string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        User usuario = new User
        {
            Email = userDto.Email,
            PasswordHash = contrasenaHash,
            Rol = userDto.Rol
        };

        _context.users.Add(usuario);
        await _context.SaveChangesAsync();

    }

    public async Task<UserDto> ObtenerPorMail(string email = "")
    {
        var usuario = await _context.users.
       FirstOrDefaultAsync(u => u.Email == email);

        if (usuario == null) return null;

        return new UserDto
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Password = usuario.PasswordHash,
            Rol = usuario.Rol
        };
    }


}