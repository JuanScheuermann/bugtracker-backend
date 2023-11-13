using backend.DTOs;

namespace backend.Services.IServicio;

public interface IUserServicio
{
    Task Crear(UserDto userDto, string token);

    Task Editar(UserDto userDto);

    Task<UserDto> ObtenerPorMail(string email);

    Task<UserDto> ObtenerPorId(long id);

    Task<List<UserDto>> ObtenerUsuarios(string cadenaBuscar = "");

}