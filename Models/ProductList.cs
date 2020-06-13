using System.ComponentModel.DataAnnotations;

namespace WorksJwtClient.Models{
    public class ProductList{
        public int Id { get; set; }
        
        [Required(ErrorMessage="Name area cannot be null !")]
        public string Name { get; set; }
    }
}