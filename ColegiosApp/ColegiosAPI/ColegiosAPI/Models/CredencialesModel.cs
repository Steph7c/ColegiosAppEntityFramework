using System.ComponentModel.DataAnnotations;

namespace ColegioAPI.Models
{
    public class CredencialesModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
