using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradingPlaces.Models;

namespace TradingPlaces.Classes
{
    public class RealEstateService
    {
        RealEstateContext db;
        public RealEstateService(RealEstateContext database)
        {
            db = database;
        }

        /// <summary>
        /// get house model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Construction GetById(int? id)
        {
            Construction house = db.Houses.Find(id);
            return house;
        }

        /// <summary>
        /// set string image in house model
        /// </summary>
        /// <param name="house"></param>
        public void SetStringImage(Construction house)
        {
            house.ImageString = "";
            if (house.Image != null)
            {
                house.ImageString = "<img id=\"prev_image\" style='width:230px; height:230px;' src=\"data:image/jpeg;base64," + Convert.ToBase64String(house.Image) + "\" />";
            }
        }

        /// <summary>
        /// get houses list for sale page
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public IEnumerable<Construction> GetHousesListInPage(int pageSize, int pageNum)
        {
            return (from houses in db.Houses orderby houses.Id select houses).Skip(pageSize * pageNum).Take(pageSize).ToList();
        }

        /// <summary>
        /// get count of all houses model 
        /// </summary>
        /// <returns></returns>
        public int GetHousesCount()
        {
            return db.Houses.Count();
        }

        /// <summary>
        /// get houses list with images
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Construction> GetHousesWithPhotos()
        {
            return from houses in db.Houses where houses.Image != null select houses;
        }

        /// <summary>
        /// delete house model from db
        /// </summary>
        /// <param name="house"></param>
        public void DeleteHouse(Construction house)
        {
            db.Houses.Remove(house);
            try
            {
                db.SaveChanges(); ;
            }
            catch { }
            
        }

        /// <summary>
        /// add new house model, or save edited model
        /// </summary>
        /// <param name="house"></param>
        public void AddSaveHouse(Construction house)
        {
            if (house.Id == 0)
            {
                db.Houses.Add(house);
            }
            else
            {
                db.Entry(house).State = System.Data.Entity.EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
            }
            catch { }        
        }
    }
}