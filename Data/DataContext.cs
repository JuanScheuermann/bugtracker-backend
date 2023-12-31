using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

//La clase DataContext representa el contexto de mi base de datos
/*Esta clase hereda de DbContext que es una clase que es propia 
de un ORM llamado EntityFramework.

Un ORM me permite poder interactuar con mi BD para
realizar operaciones de consulta y creacion de datos 
con la syntaxis de C# sin necesidad de escribir consultas SQL*/

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
    : base(options)
    {

    }
    public DbSet<User> users { get; set; }

    public DbSet<Proyecto> Proyectos { get; set; }

    public DbSet<Miembro> Miembros { get; set; }

    public DbSet<Etiqueta> Etiquetas { get; set; }

    public DbSet<Comentario> Comentarios { get; set; }

    public DbSet<ReiniciarContrasena> reiniciarContrasenas { get; set; }

}