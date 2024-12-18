using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_CommerceSystem.Models
{

    [PrimaryKey(nameof(OID), nameof(PID))]
    public class OrderProduct
    {
        [JsonIgnore]
        [ForeignKey("Order")]
        public int OID { get; set; }


        [JsonIgnore]
        public Order Order { get; set; }


        
        [ForeignKey("Product")]
        public int PID { get; set; }
       
        [JsonIgnore]
        public Product Product { get; set; }

        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "Quantity  must be more than 0")]
        public int Quantity { get; set; }   


    }
}
