using Microsoft.EntityFrameworkCore;
using PeliculasGrupo4.Models;

public class PeliculasGrupo4Context(DbContextOptions<PeliculasGrupo4Context> options) : DbContext(options)
{
    public DbSet<PeliculasGrupo4.Models.Pelicula> Pelicula { get; set; } = default!;
    public DbSet<Genero> Generos { get; set; }
}
