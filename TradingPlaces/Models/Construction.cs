using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradingPlaces.Models
{
    public class Construction
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter a price")]
        [Range(typeof(decimal), "0,01", "999999999999,00", ErrorMessage = "Incorrect price")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public string Postcode { get; set; }

        [Required(ErrorMessage = "You must enter a address")]
        [StringLength(350)]
        public string Address { get; set; }
        public string Area { get; set; }
        public string Tenure { get; set; }
        public bool SSTC { get; set; }
        public int Bedrooms { get; set; }
        public int LivingRooms { get; set; }
        public int Bathrooms { get; set; }
        public bool Shower { get; set; }
        public bool Parking { get; set; }
        public bool Garden { get; set; }
        public byte[] Image { get; set; }
        public string ImageString { get; set; }
        public string Сomment { get; set; } 
        public string PropertyStatus { get; set; } 
    }
}