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
    public class AdminBrandController : Controller
    {
        DataContext db = new DataContext();
        public AdminBrandController(DataContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var list = db.Brands.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand data)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir hata oluştu");
            return View(data);
        }

        public IActionResult Update(int id)
        {
            var update = db.Brands.Where(x => x.BrandId == id).FirstOrDefault();

            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Brand data)
        {
            var update = db.Brands.Where(x => x.BrandId == data.BrandId).FirstOrDefault();
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
            var delete = db.Brands.Where(x => x.BrandId == id).FirstOrDefault();

            db.Brands.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
