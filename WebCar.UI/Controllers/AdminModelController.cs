using DataAccessLayer.Data;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.UI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminModelController : Controller
    {
        DataContext db = new DataContext();
        public AdminModelController(DataContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var list = db.Models.ToList();
            return View(list);
        }
        public IActionResult Create()
        {
            DropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Model data)
        {
            if (ModelState.IsValid)
            {
                db.Models.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir hata oluştu");
            return View(data);
        }
        public IActionResult Update(int id)
        {
            DropDowns();
            var update = db.Models.Where(x => x.ModelId == id).FirstOrDefault();

            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Model data)
        {
            var update = db.Models.Where(x => x.ModelId == data.ModelId).FirstOrDefault();
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
            var delete = db.Models.Where(x => x.ModelId == id).FirstOrDefault();

            db.Models.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void DropDowns()
        {
            List<SelectListItem> value = (from i in db.Brands.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.BrandId.ToString()
                                           }).ToList();
            ViewBag.brand = value;
        }

    }
}
