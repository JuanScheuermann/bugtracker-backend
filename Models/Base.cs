using System.ComponentModel.DataAnnotations;

namespace backend.Models;

//Clase que Heredan la mayoria de los Modelos
//ya que estan son las propiedades que la mayoria tiene
public class Base
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Estado { get; set; } = "Activo"; //Valor por defecto
}