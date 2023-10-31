using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUserServicio _userServicio;

    public UsuarioController(IUserServicio userServicio)
    {
        _userServicio = userServicio;
    }

    [HttpPut]
    [Route("mod_usuario")]
    public async Task<IActionResult> ModificarPerfil([FromBody] UserDto userDto)
    {

        var usuario = await _userServicio.ObtenerPorId(userDto.Id);
        if (usuario == null) return BadRequest(new
        {
            message = "No se logro encontrar al usuario"
        });

        await _userServicio.Editar(userDto);

        return Ok(new
        {
            message = "los cambios se realizaron correctamente"
        });
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> ObtnerUsuarios(string cadenaBuscar = "")
    {
        var usuarios = await _userServicio.ObtenerUsuarios(cadenaBuscar);

        return Ok(new
        {
            usuarios
        });
    }
}