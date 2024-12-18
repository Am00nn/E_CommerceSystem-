using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_CommerceSystem.Models
{

    //[PrimaryKey(nameof(OID), nameof(PID))]
    //public class OrderProduct
    //{

    //    [ForeignKey("Order")]
    //    public int OID { get; set; }


    //    [JsonIgnore]
    //    public Order Order { get; set; }



    //    [ForeignKey("Product")]
    //    public int PID { get; set; }

    //    [JsonIgnore]
    //    public Product Product { get; set; }

    //    [Required]
    //    [Range(1, int.MaxValue , ErrorMessage = "Quantity  must be more than 0")]
    //    public int Quantity { get; set; }   


    //}

    //[PrimaryKey(nameof(OID), nameof(PID))]
    //public class OrderProduct
    //{


    //    [ForeignKey("Order")]
    //    public int OID { get; set; }

    //    [JsonIgnore] 
    //    public Order Order { get; set; }

    //    [ForeignKey("Product")]
    //    public int PID { get; set; }

    //    [JsonIgnore] 
    //    public Product Product { get; set; }

    //    [Required]
    //    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be more than 0")]
    //    public int Quantity { get; set; }
    //}



    //[PrimaryKey(nameof(OID), nameof(PID))]
    //public class OrderProduct
    //{
    //    [ForeignKey("Order")]
    //    public int OID { get; set; }

    //    [NotMapped] // Prevent this property from being validated
    //    [JsonIgnore]
    //    public Order Order { get; set; }

    //    [ForeignKey("Product")]
    //    public int PID { get; set; }

    //    [NotMapped] // Prevent this property from being validated
    //    [JsonIgnore]
    //    public Product Product { get; set; }

    //    [Required]
    //    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be more than 0")]
    //    public int Quantity { get; set; }
    //}



    //[Table("OrderProduct")]
    //public class OrderProduct
    //{
    //    [Required]
    //    [ForeignKey("Order")]
    //    public int OID { get; set; }

    //    [JsonIgnore] // Prevent model binding and serialization
    //    public Order Order { get; set; }

    //    [Required]
    //    [ForeignKey("Product")]
    //    public int PID { get; set; }

    //    [JsonIgnore] // Prevent model binding and serialization
    //    public Product Product { get; set; }

    //    [Required]
    //    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be more than 0.")]
    //    public int Quantity { get; set; }
    //}


    //public class OrderProduct
    //{

    //    [JsonIgnore]
    //    [ForeignKey("Order")]
    //    public int OID { get; set; }

    //    [JsonIgnore]
    //    public Order Order { get; set; }

    //    [ForeignKey("Product")]
    //    public int PID { get; set; }

    //    [JsonIgnore]
    //    public Product Product { get; set; }

    //    [Required]
    //    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be more than 0.")]
    //    public int Quantity { get; set; }
    //}


    //public class OrderProduct
    //{
    //    [Required]
    //    [ForeignKey("Order")]
    //    public int OID { get; set; } // Foreign Key to Order

    //    [JsonIgnore]
    //    public Order Order { get; set; } // Navigation Property (ignored during binding)

    //    [Required]
    //    [ForeignKey("Product")]
    //    public int PID { get; set; } // Foreign Key to Product

    //    [JsonIgnore]
    //    public Product Product { get; set; } // Navigation Property (ignored during binding)

    //    [Required]
    //    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    //    public int Quantity { get; set; } // Product Quantity
    //}



    public class OrderProduct
    {
        //[Required(ErrorMessage = "Order ID is required.")]
        [ForeignKey("Order")]
        [JsonIgnore] // Include only if not needed in the payload
        public int OID { get; set; } // Order ID (Foreign Key)

        [JsonIgnore] // Prevent navigation property serialization
        public  Order? Order = null;

        [Required(ErrorMessage = "Product ID is required.")]
        [ForeignKey("Product")]
        public int PID { get; set; } // Product ID (Foreign Key)

        [JsonIgnore] // Prevent navigation property serialization
        public Product? Product = null;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; } // Product Quantity
    }



}
