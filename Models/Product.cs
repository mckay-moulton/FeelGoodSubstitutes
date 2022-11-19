using System.ComponentModel.DataAnnotations;

namespace FeelGoodSubstitutes.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? Product_Name {get;set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public string? Vendor_Name { get; set; }
        public string? Product_Image_URL { get; set; }
        public float? Product_Price { get; set; }
        public string? Product_URL { get; set; }
        public float? Eco_Rating { get; set; }

        public float Customer_Rating { get; set; }
        public string? About { get; set; }
        public string? Category { get; set; }



    }
}
