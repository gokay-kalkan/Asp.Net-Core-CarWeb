using DataAccessLayer.Data;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.UI.ViewComponents
{
    public class FiltrePartial : ViewComponent
    {
        DataContext db = new DataContext();
        public FiltrePartial(DataContext _db)
        {
            this.db = _db;
        }
        public IActionResult ModelGet(int BrandId)
        {
            List<Model> modelist = db.Models.Where(x => x.BrandId == BrandId).ToList();
            ViewBag.model = new SelectList(modelist, "Id", "Name");
            return (IActionResult)View();
        }
        public IViewComponentResult Invoke()
        {
            List<SelectListItem> value = (from i in db.Brands.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.BrandId.ToString()
                                          }).ToList();
            ViewBag.brand = value;

            List<SelectListItem> value1 = (from i in db.States.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.StateId.ToString()
                                           }).ToList();
            ViewBag.states = value1;

            List<SelectListItem> value2 = (from i in db.Cities.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.CityId.ToString()
                                           }).ToList();
            ViewBag.cities = value2;
            //ViewBag.brandlist = new SelectList(db.Brands, "Id", "Name");
            //ViewBag.states = new SelectList(db.States, "Id", "Name");

            //ViewBag.cities = new SelectList(db.Cities, "Id", "Name");
         

           
            return View("Default");
        }
    }
}
