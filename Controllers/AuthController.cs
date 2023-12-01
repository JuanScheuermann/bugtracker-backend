using System.Security.Cryptography;
using backend.DTOs;
using backend.helpers;
using backend.Models;
using backend.Services.IServicio;
using backend.Services.Servicio;
using BCrypt.Net;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly IUserServicio _userServicio;
    private readonly IConfiguration _configuration;
    private readonly IContrasenaServicio _contrasenaServicio;

    public AuthController(
        IUserServicio userServicio,
        IConfiguration configuration,
        IContrasenaServicio contrasenaServicio
    )
    {
        _userServicio = userServicio;
        _configuration = configuration;
        _contrasenaServicio = contrasenaServicio;
    }

    [HttpPost]
    [Route("registrar")]
    public async Task<ActionResult> RegistrarUsuario([FromBody] UserDto request)
    {
        //verificar si el mail se encuentr en uso 
        var user = await _userServicio.ObtenerPorMail(request.Email);

        if (user != null)
            return BadRequest(new { Message = "Este mail ya se encuentra registrado" });

        string token = CrearRandomToken();
        await _userServicio.Crear(request, token);
        return Ok(new
        {
            Message = "usuario Creado Correctamente."
        });
    }

    [HttpPost]
    [Route("verificar")]
    public async Task<ActionResult> VerificarMail([FromBody] UserMailDto Dto)
    {
        var usuario = await _userServicio.ObtenerPorMail(Dto.Email);

        if (usuario == null) return NotFound();
        if (usuario.Estado == Estado.Bloqueado) return BadRequest(new { message = "Este Usuario se encuentra bloqueado" });

        var token = await _contrasenaServicio.Obtener(usuario.Email);

        await EnviarMailverificacion(token, usuario.Email);
        return Ok();
    }

    [HttpPut]
    [Route("cambio_contrasena")]
    public async Task<ActionResult> CambioContrasena(string token, [FromBody] UsuarioContrasenaDto dto)
    {
        var usuario = await _contrasenaServicio.ObtenerporToken(token);

        string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        usuario.Password = contrasenaHash;

        await _userServicio.Editar(usuario);

        return Ok(new { message = "Cambios realizados correctamente" });
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

        if (usuario.Estado == Estado.Bloqueado) return BadRequest(new { message = "Este Usuario se encuentra bloqueado" });

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
            Uid = usuario.Id,
            Rol = usuario.Rol
        });
    }

    private string CrearRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }

    private async Task EnviarMailverificacion(string token, string mailUsuario)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("t41136041@gmail.com"));
        email.To.Add(MailboxAddress.Parse(mailUsuario));

        email.Subject = "Cambio de contraseña";

        email.Body = new TextPart(TextFormat.Html)
        {
            Text = "Para finalizar haga click en el enlace: \n"
            + $"http://localhost:5173/auth/verificar/cambiar/{token}"
        };

        using var smtp = new SmtpClient();
        // smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

        smtp.Authenticate("t41136041@gmail.com"
        , "frumypqsmgqbssul");

        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }

}