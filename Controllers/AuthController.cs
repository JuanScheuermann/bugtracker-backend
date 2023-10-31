using backend.DTOs;
using backend.helpers;
using backend.Services.IServicio;
using backend.Services.Servicio;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly IUserServicio _userServicio;
    private readonly IConfiguration _configuration;

    public AuthController(IUserServicio userServicio, IConfiguration configuration)
    {
        _userServicio = userServicio;
        _configuration = configuration;
    }
    [HttpPost]
    [Route("registrar")]
    public async Task<ActionResult> RegistrarUsuario([FromBody] UserDto request)
    {
        //verificar si el mail se encuentr en uso 
        var user = await _userServicio.ObtenerPorMail(request.Email);

        if (user != null)
            return BadRequest(new { Message = "Este mail ya se encuentra registrado" });


        await _userServicio.Crear(request);
        return Ok(new
        {
            Message = "usuario Creado Correctamente."

        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> LoginUsuario(UserLoginDto request)
    {
        var usuario = await _userServicio.ObtenerPorMail(request.Email);

        //verificar si el mail esta registrado
        if (usuario == null) return BadRequest(new
        {
            Message = "Usuario o contraseña incorrecta."
        });

        //verificar contraseña
        if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
        {
            return BadRequest(new
            {
                Message = "Usuario o contraseña incorrecta."
            });
        }

        var token = JwtHelper.GenerarJwt(usuario, _configuration);

        return Ok(new
        {
            Token = token,
            Nombre = usuario.ApiNom,
            Uid = usuario.Id
        });
    }

}