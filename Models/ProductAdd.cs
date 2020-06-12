using System.ComponentModel.DataAnnotations;

namespace WorksJwtClient.Models{
    public class ProductAdd{
        [Required(ErrorMessage="Product name area cannot be null !")]
        public string Name { get; set; }
    }
}