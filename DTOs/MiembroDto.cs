namespace backend.DTOs;

public class MiembroDto
{
    public long Id { get; set; }

    public long ProyectoId { get; set; }

    public long UsuarioId { get; set; }

    public string? ApiNom { get; set; }

    public string? Email { get; set; }
    public string? Estado { get; set; }
}