using System.ComponentModel.DataAnnotations;

namespace APIExample
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
