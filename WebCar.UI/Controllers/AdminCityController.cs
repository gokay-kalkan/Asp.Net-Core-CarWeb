using DataAccessLayer.Data;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.UI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminCityController : Controller
    {
        DataContext db = new DataContext();
        public AdminCityController(DataContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var liste = db.Cities.ToList();
            return View(liste);
        }
        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City data)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir hata oluştu");
            return View(data);
        }
        public IActionResult Update(int id)
        {
            var update = db.Cities.Where(x => x.CityId == id).FirstOrDefault();

            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(City data)
        {
            var update = db.Cities.Where(x => x.CityId == data.CityId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                update.Name = data.Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Bir hata oluştu");
            return View(data);

        }

        public IActionResult Delete(int id)
        {
            var delete = db.Cities.Where(x => x.CityId == id).FirstOrDefault();
         
            db.Cities.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
