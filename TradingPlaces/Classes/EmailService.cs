using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradingPlaces.Models;

namespace TradingPlaces.Classes
{
    public class EmailService
    {
        RealEstateContext db;
        public EmailService(RealEstateContext database)
        {
            db = database;
        }

        /// <summary>
        /// add new email model in db
        /// </summary>
        /// <param name="emailModel"></param>
        public void AddEmail(Email emailModel)
        {
            if (String.IsNullOrWhiteSpace(emailModel.ContactTime))
            {
                emailModel.ContactTime = "Anytime";
            }
            emailModel.Time = DateTime.Now;
            db.Emails.Add(emailModel);
            try
            {
                db.SaveChanges();
            }
            catch
            { } 
        }
    }
}