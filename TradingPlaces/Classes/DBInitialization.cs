using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradingPlaces.Models;

namespace TradingPlaces.Classes
{
    public static class DBInitialization
    {
        public static RealEstateContext InitializeDB()
        {
            return new RealEstateContext();
        }

    }
}