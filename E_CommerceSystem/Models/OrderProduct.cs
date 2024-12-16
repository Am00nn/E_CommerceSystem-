using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{

    [PrimaryKey(nameof(OID), nameof(PID))]
    public class OrderProduct
    {

        [ForeignKey("Order")]
        public int OID { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int PID { get; set; }    

        public Product Product { get; set; }

        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "Quantity  must be more than 0")]
        public int Quantity { get; set; }   


    }
}
