using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradingPlaces.Models
{
    public class Email
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "You must enter a title")]
        [StringLength(150)]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must enter a name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter a phone number")]
        [StringLength(25)]
        public string PhoneNumber  { get; set; }
        public string ContactTime { get; set; }

        [Required(ErrorMessage = "You must enter a message")]
        [StringLength(750)]
        public string Message { get; set; }
    }
}