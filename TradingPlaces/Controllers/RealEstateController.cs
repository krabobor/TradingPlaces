using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using TradingPlaces.Classes;
using TradingPlaces.Models;
using TradingPlaces.Helpers;

namespace TradingPlaces.Controllers
{
    public class RealEstateController : Controller
    {   
        readonly RealEstateService _realEstateService;
        readonly EmailService _emailService;
        private int defaulPageSize = 10;

        public RealEstateController()
        {
            RealEstateContext db = DBInitialization.InitializeDB();
            _realEstateService = new RealEstateService(db);
            _emailService = new EmailService(db);
        }

        /// <summary>
        /// GET: /RealEstate/Index - main page with photo gallery
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var houseList = _realEstateService.GetHousesWithPhotos();
            var items = LinqRandomsExtensions.Randoms(houseList, 12);
            return View(items);
        }

        /// <summary>
        /// GET: /RealEstate/Sale - Sale page with models list
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult Sale(int pageSize = 0, int pageNum = 0)
        {
            if (HttpContext.Request.Cookies["pageSize"] != null && pageSize == 0)
            {
                try
                {
                    pageSize = Int32.Parse(HttpContext.Request.Cookies["pageSize"].Value);
                }
                catch (FormatException e)
                {
                    pageSize = 0;
                }
            }
            if (pageSize == 0)
            {
                pageSize = defaulPageSize;
            }
            HttpContext.Response.Cookies["pageSize"].Value = pageSize.ToString();
            ViewData["PageNum"] = pageNum;
            ViewData["ItemsCount"] = _realEstateService.GetHousesCount();
            ViewData["PageSize"] = pageSize;
            var houseList = _realEstateService.GetHousesListInPage(pageSize, pageNum);
            return View(houseList);
        }

        /// <summary>
        /// GET: /RealEstate/Delete/5 - delete house model page 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Construction b = _realEstateService.GetById(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        /// <summary>
        /// POST: /RealEstate/Delete/5 - delete house model from db 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Construction house = _realEstateService.GetById(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            _realEstateService.DeleteHouse(house);
            return RedirectToAction("Sale");
        }

        /// <summary>
        /// GET: /RealEstate/info/5 - get info page to create/edit house model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Info(int? id, int pageNum = 0)
        {
            if (id == null)
            {
                Construction newHouse = new Construction();
                return View(newHouse);
            }
            Construction house = _realEstateService.GetById(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            _realEstateService.SetStringImage(house);
            
            HttpContext.Response.Cookies["pageNum"].Value = pageNum.ToString();
            return View(house);
        }

        /// <summary>
        /// POST: /RealEstate/info/5 - save house model in db
        /// </summary>
        /// <param name="house"></param>
        /// <param name="uploadImage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Info(Construction house, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (uploadImage != null)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        house.Image = imageData;
                    }
                }
                _realEstateService.AddSaveHouse(house);
                int pageNum = 0;
                if (HttpContext.Request.Cookies["pageNum"] != null)
                {
                    try
                    {
                        pageNum = Int32.Parse(HttpContext.Request.Cookies["pageNum"].Value);
                    }
                    catch { }
                }
                return RedirectToAction("Sale", new { pageNum = pageNum });
            }
            return View(house);
        }

        /// <summary>
        /// GET: /RealEstate/Contact - contact page with ours contacts, google map and send-mail form
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            Email newEmail = new Email();
            return View(newEmail);
        }

        /// <summary>
        /// POST: /RealEstate/Contact/ -  save Email model in db and send email
        /// </summary>
        /// <param name="emailModel"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Contact")]
        public ActionResult SendEmail(Email emailModel)
        {
            if (ModelState.IsValid)
            {
                _emailService.AddEmail(emailModel);
                bool result = SendingEmail.SendEmail(emailModel);
                if (result)
                {
                    ViewBag.Message = "Your message has been sent successfully!";
                }
                else
                {
                    ViewBag.Message = "Error sending message :(";
                }
                return View("SendEmail");
            }
            return View("Contact", emailModel);
        }

    }
}
