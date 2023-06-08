namespace ColegioAPI.Models
{
    public class AutenticacionModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Rol { get; set; }
    }
}
