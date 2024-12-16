using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    public class Product
    {
        [Key] 
        public int P_Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string P_Name { get; set; }

        [Required(ErrorMessage = "The price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price should be a positive number.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The stock quantity is mandatory.")]

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be less than zero.")]
        public int Stock { get; set; }


        [Column(TypeName = "decimal(3,2)")]
        public decimal? OverallRating { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
