using backend.DTOs;

namespace backend.Services.IServicio;

public interface IContrasenaServicio
{
    Task<string> Obtener(string Email);

    Task<UserDto> ObtenerporToken(string token);
}