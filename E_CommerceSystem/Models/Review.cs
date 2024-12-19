using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_CommerceSystem.Models
{

    public class Review
    {
        [Key]
        [JsonIgnore]
        public int R_ID { get; set; }


        [Required]
        [JsonIgnore]
        public DateTime R_date { get; set; } = DateTime.UtcNow;



        [Required]
        [ForeignKey("Product")]
        public int PID { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }


        [Required]
        [Range(1, 5, ErrorMessage = "please enter rating [1-5]")]
        public int Rating { get; set; }


        public string Comment { get; set; }


        [Required]
        [JsonIgnore]
        [ForeignKey("User")]
        public int UID { get; set; }
        [JsonIgnore]
        public User? User { get; set; }



    }

}
