using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class ReiniciarContrasena
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Token { get; set; }
}