using backend.DTOs;
using backend.Services.IServicio;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUserServicio _userServicio;
    private readonly IMiembroServicio _miembroServicio;

    public UsuarioController(IUserServicio userServicio, IMiembroServicio miembroServicio)
    {
        _userServicio = userServicio;
        _miembroServicio = miembroServicio;
    }

    [HttpPut]
    [Route("mod_usuario")]
    public async Task<ActionResult> ModificarPerfil([FromBody] UserDto userDto)
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
    public async Task<ActionResult> ObtnerUsuarios(string cadenaBuscar = "")
    {
        var usuarios = await _userServicio.ObtenerUsuarios(cadenaBuscar);

        return Ok(usuarios);
    }

    [HttpGet]
    [Route("{uid}")]
    public async Task<ActionResult> ObtenerUsuario(long uid)
    {
        var usuario = await _userServicio.ObtenerPorId(uid);
        if (usuario == null) return NotFound();

        return Ok(usuario);
    }

    [HttpGet]
    [Route("{pid}")]
    public async Task<ActionResult> ObtenerNoMiembros(long pid)
    {
        var miembros = await _miembroServicio.ObtenerMiembros(pid);
        var usuarios = await _userServicio.ObtenerUsuarios("");

        var usuariosList = new List<UserDto>();
        foreach (var usuario in usuarios)
        {
            if (miembros.Any(x => x.UsuarioId == usuario.Id) == false)
            {
                usuariosList.Add(usuario);
            }
        }
        return Ok(usuariosList);
    }
}