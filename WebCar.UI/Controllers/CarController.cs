using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.UI.Controllers
{
    public class CarController : Controller
    {
        DataContext db = new DataContext();
        public CarController(DataContext _db)
        {
            this.db = _db;
        }
        public IActionResult CarDetails(int id)
        {
            var cardetail = db.Adverts.Where(x => x.Id == id).FirstOrDefault();
            var carimageone = db.Images.Where(x => x.Advert.Id == x.AdvertId).Select(x => x.ImageName).Take(1).FirstOrDefault();
            ViewBag.imageone = carimageone;
            return View(cardetail);
        }
        public IActionResult OnSale()
        {
            var onsale = db.Adverts.Where(x=>x.StateId==2).ToList();
            var advertimage = db.Images.ToList();
            ViewBag.imgs = advertimage;
            return View(onsale);
        }
        public IActionResult OnRent()
        {
            var onrent = db.Adverts.Where(x => x.StateId==1).ToList();
            var advertimage = db.Images.ToList();
            ViewBag.imgs = advertimage;
            return View(onrent);
        }
        public IActionResult Filtre(int min, int max, int sehirid, int durumid, int markaid, int modelid)
        {
            var imgs = db.Images.ToList();
            ViewBag.imgs = imgs;
            var filtre = db.Adverts.Where(i => i.Price >= min && i.Price <= max
              && i.StateId == durumid
              && i.CityId == sehirid
              && i.BrandId == markaid
              && i.ModelId == modelid).ToList();
            

            return View(filtre);
        }
    }
}
