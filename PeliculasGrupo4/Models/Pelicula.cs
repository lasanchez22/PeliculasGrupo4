namespace PeliculasGrupo4.Models
{
    public class Pelicula
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Director { get; set; } = string.Empty;

        public int DuracionMinutos { get; set; }

        public string? Sinopsis { get; set; }
    }
}