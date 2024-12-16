using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{

    [PrimaryKey(nameof(UID), nameof(PID) , nameof(R_data))]
    public class Review
    {
        [Required]
        [ForeignKey("User")]
        public int UID { get; set; }
        public User User { get; set; }  


        [Required]
        [ForeignKey("Product")]
        public int PID { get; set; }    
        public Product Product { get; set; }    


        public DateTime R_data { get; set; }


        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public string Comment { get; set; }









    }
}
