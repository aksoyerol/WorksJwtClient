using System.ComponentModel.DataAnnotations;

namespace WorksJwtClient.Models
{
    public class AppUserLogin
    {
        [Required(ErrorMessage="Username area cannot be null !")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage="Password area cannot be null !")]
        public string Password { get; set; }
    }
}