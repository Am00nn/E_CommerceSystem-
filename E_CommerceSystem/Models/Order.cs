using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    public class Order
    {
        [Key]   
        public int Order_Id { get; set; }

        [Required]

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime Orderdate { get; set; }


        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Totale moust be more than 0  " )]
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalAmount  { get; set; }


        public ICollection<OrderProduct> OrderProduct { get; set; } 



    }
}
