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
    [Authorize(Roles = "Admin")]
    public class AdminStateController : Controller
    {
        DataContext db = new DataContext();
        public AdminStateController(DataContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var list = db.States.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(State data)
        {
            if (ModelState.IsValid)
            {
                db.States.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir hata oluştu");
            return View(data);
        }

        public IActionResult Update(int id)
        {
            var update = db.States.Where(x => x.StateId == id).FirstOrDefault();
            
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(State data)
        {
           
            if (ModelState.IsValid)
            {
                var update = db.States.Where(x => x.StateId == data.StateId).FirstOrDefault();
                update.Name = data.Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir hata oluştu");
            return View(data);

        }

        public IActionResult Delete(int id)
        {
            var delete = db.States.Where(x => x.StateId == id).FirstOrDefault();
            db.States.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
