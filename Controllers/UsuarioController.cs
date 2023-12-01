using backend.DTOs;
using backend.Models;
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

        if (usuario.Email != userDto.Email && _userServicio.ObtenerPorMail(userDto.Email) != null)
        {
            return BadRequest(new
            {
                message = "Este email se encuentra usado por otro usuario"
            });
        }

        if (!string.IsNullOrEmpty(userDto.Password))
        {

            string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            userDto.Password = contrasenaHash;
        }
        else
        {
            userDto.Password = usuario.Password;
        }

        await _userServicio.Editar(userDto);

        return Ok("los cambios se realizaron correctamente");
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
    [Route("{pid}/nuevos_m")]
    public async Task<ActionResult> ObtenerNoMiembros(long pid, string cadenaBuscar = "")
    {
        var miembros = await _miembroServicio.ObtenerMiembros(pid);
        var usuarios = await _userServicio.ObtenerUsuarios(cadenaBuscar);

        var usuariosList = new List<UserDto>();
        foreach (var usuario in usuarios)
        {
            if (miembros.Any(x => x.UsuarioId == usuario.Id || usuario.Estado == Estado.Bloqueado) == false)
            {
                usuariosList.Add(usuario);
            }
        }
        return Ok(usuariosList);
    }

    [HttpPut]
    [Route("{uid}/permisos")]
    public async Task<ActionResult> PermisosUsuario(long uid, [FromBody] UsuarioRolDto rolDto)
    {
        var usuario = await _userServicio.ObtenerPorId(uid);
        if (usuario == null) return BadRequest(new
        {
            message = "No se logro encontrar al usuario"
        });

        usuario.Rol = rolDto.rol;
        await _userServicio.Editar(usuario);

        return Ok();
    }

    [HttpPut]
    [Route("{uid}/bloquear")]
    public async Task<ActionResult> BloquearUsuario(long uid)
    {
        var usuario = await _userServicio.ObtenerPorId(uid);
        if (usuario == null) return BadRequest();

        usuario.Estado = usuario.Estado == Estado.Activo
        ? Estado.Bloqueado
        : Estado.Activo;

        await _userServicio.Editar(usuario);

        return Ok();
    }
}