using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioDomain.Entidades
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

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
        public string Rol { get; set;}
        public DateTime FechaIngreso { get; set; }
        public bool Estado { get; set; }
    }
}
