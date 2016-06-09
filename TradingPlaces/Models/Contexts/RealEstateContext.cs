using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TradingPlaces.Models
{
    public class RealEstateContext : DbContext
    {
        public RealEstateContext()
            : base("Name=RealEstateContext") 
        {

        }
        public DbSet<Construction> Houses { get; set; }
        public DbSet<Email> Emails { get; set; } 
    }
}