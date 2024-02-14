using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWeb.API.Model.Domain
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Column("Name")]
        [MinLength(5, ErrorMessage = "Name has to be a minimum of 5 characters")]
        [MaxLength(15, ErrorMessage = "Name has to be a maximum of 15 characters")]
        [Required]
        public string Name { get; set; } = String.Empty;

        [Column("Category")]
        [MinLength(5, ErrorMessage = "Name has to be a minimum of 5 characters")]
        [MaxLength(10, ErrorMessage = "Name has to be a maximum of 10 characters")]
        [Required]
        public string Category { get; set; } = String.Empty;

        [Column("Price")]
        [Required]
        public decimal Price { get; set; }

    }
}
