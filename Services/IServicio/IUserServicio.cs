using backend.DTOs;

namespace backend.Services.IServicio;

public interface IUserServicio
{
    Task Crear(UserDto userDto);

    Task<UserDto> ObtenerPorMail(string email);


}