using DataAccessLayer.Data;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCar.UI.Models;
using X.PagedList;

namespace WebCar.UI.Controllers
{

    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public HomeController(DataContext _db)
        {
            this.db = _db;
        }
        public IActionResult Index(int page = 1)
        {

            List<SelectListItem> value = (from i in db.Brands
                                          select new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.BrandId.ToString()
                                          }).ToList();
            ViewBag.brand = value;

            List<SelectListItem> value1 = (from i in db.States
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.StateId.ToString()
                                           }).ToList();
            ViewBag.states = value1;

            List<SelectListItem> value2 = (from i in db.Cities
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.CityId.ToString()
                                           }).ToList();
            ViewBag.cities = value2;

            //ViewBag.brand = new SelectList(BrandGet(), "BrandId", "Name");
            //ViewBag.states = new SelectList(db.States, "StateId", "Name");

            //ViewBag.cities = new SelectList(db.Cities, "CityId", "Name");

            var carlist = db.Adverts.ToList().ToPagedList(page,3);
           
           
            var advertimage = db.Images.ToList();
            ViewBag.imgs = advertimage;

            return View(carlist);
        }
        public IActionResult ModelGet(int BrandId)
        {
            List<Model> modelist = db.Models.Where(x => x.BrandId == BrandId).ToList();
            ViewBag.model = new SelectList(modelist, "ModelId", "Name");
            return PartialView("ModelPartial");
        }
    
        public IActionResult Filtre(int min, int max, int sehirid, int durumid, int markaid, int modelid)
        {
            List<SelectListItem> value = (from i in db.Brands
                                          select new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.BrandId.ToString()
                                          }).ToList();
            ViewBag.brand = value;

            List<SelectListItem> value1 = (from i in db.States
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.StateId.ToString()
                                           }).ToList();
            ViewBag.states = value1;

            List<SelectListItem> value2 = (from i in db.Cities
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.CityId.ToString()
                                           }).ToList();
            ViewBag.cities = value2;

            //ViewBag.brand = new SelectList(BrandGet(), "BrandId", "Name");
            //ViewBag.states = new SelectList(db.States, "StateId", "Name");

            //ViewBag.cities = new SelectList(db.Cities, "CityId", "Name");
            var filtre = db.Adverts.Where(i => i.Price >= min && i.Price <= max
            && i.StateId == durumid
            && i.CityId == sehirid
            && i.BrandId == markaid
            && i.ModelId == modelid).Include(x => x.Model).Include(x => x.State).Include(x => x.City).ToList();
            var imgs = db.Images.ToList();

            ViewBag.imgs = imgs;
           
            

            return View("Filtre" , filtre);
        }
        public PartialViewResult PartialFiltre()
        {
            List<SelectListItem> value = (from i in db.Brands
                                          select new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.BrandId.ToString()
                                          }).ToList();
            ViewBag.brand = value;

            List<SelectListItem> value1 = (from i in db.States
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.StateId.ToString()
                                           }).ToList();
            ViewBag.states = value1;

            List<SelectListItem> value2 = (from i in db.Cities
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.CityId.ToString()
                                           }).ToList();
            ViewBag.cities = value2;

            return PartialView();
        }
       
      
       



    }
}
