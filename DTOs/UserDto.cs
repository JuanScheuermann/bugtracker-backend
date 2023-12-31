namespace backend.DTOs;

public class UserDto
{
    public long Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string ApiNom => Nombre + " " + Apellido;

    public string Email { get; set; }

    public string Password { get; set; }

    public string Rol { get; set; }

    public string? Estado { get; set; }
}