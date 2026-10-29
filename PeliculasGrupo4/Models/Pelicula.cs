using System.ComponentModel.DataAnnotations;

namespace PeliculasGrupo4.Models
{
    public class Pelicula
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150, ErrorMessage = "El título no puede tener más de 150 caracteres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El director es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del director no puede tener más de 100 caracteres.")]
        [Display(Name = "Director")]
        public string Director { get; set; } = string.Empty;

        [Required(ErrorMessage = "La duración es obligatoria.")]
        [Range(1, 300, ErrorMessage = "La duración debe estar entre 1 y 300 minutos.")]
        [Display(Name = "Duración (minutos)")]
        public int DuracionMinutos { get; set; }

        [StringLength(500, ErrorMessage = "La sinopsis no puede exceder los 500 caracteres.")]
        [Display(Name = "Sinopsis")]
        public string? Sinopsis { get; set; }
    }
}