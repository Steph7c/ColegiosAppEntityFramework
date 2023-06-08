using System.ComponentModel.DataAnnotations;

namespace ColegioAPI.Models
{
    public class UsuarioModel
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(10)]
        public string Celular { get; set; }

        [Required]
        [StringLength(50)]
        public string Rol { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Estado { get; set; }
    }
}
