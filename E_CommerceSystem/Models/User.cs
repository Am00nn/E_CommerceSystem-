﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_CommerceSystem.Models
{
    public class User
    {

        [Key]
        [JsonIgnore]
        public int UId { get; set; }

        [Required]

        [MaxLength(50)]
        public string U_Name { get; set; }

        [Required]
        [EmailAddress]
        public string U_Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, include one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }


        [Required]
        [Phone]
        public string Phone { get; set; }



        [Required]
        public string Role { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();


    }
}
