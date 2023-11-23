namespace backend.DTOs;

public class ComentarioDto
{
    public long Id { get; set; }

    public string Cuerpo { get; set; }

    public string? Fecha { get; set; }

    public long MiembroId { get; set; }

    public string? MiembroStr { get; set; }

    public long EtiquetaId { get; set; }

    public string? Estado { get; set; }
}