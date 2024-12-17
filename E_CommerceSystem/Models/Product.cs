using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_CommerceSystem.Models
{
    public class Product
    {
        [Key]
        [JsonIgnore]
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


        [Required]
        public string Description { get; set; }

       
        [Range(0, 5, ErrorMessage = "Overall rating must be between 0 and 5.")]
        [Column(TypeName = "decimal(3,2)")]
        [JsonIgnore]
        public decimal? OverallRating { get; set; } = 5; // Default value =5



        [JsonIgnore]
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();

        [JsonIgnore]
        public ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();

    }
}
