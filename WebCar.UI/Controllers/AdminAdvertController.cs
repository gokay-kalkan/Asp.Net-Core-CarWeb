using DataAccessLayer.Data;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminAdvertController : Controller
    {
        DataContext db = new DataContext();
        IWebHostEnvironment env;
        public AdminAdvertController(DataContext _db, IWebHostEnvironment _env)
        {
            this.db = _db;
            this.env = _env;
        }
        public IActionResult Index(string id)
        {
            id = HttpContext.Session.GetString("Id");
            var list = db.Adverts.Where(x => x.UserAdminId == id && x.Status==true).ToList();
            return View(list);
        }

       
        public IActionResult ModelGet(int BrandId)
        {
            List<Model> modelist = db.Models.Where(x => x.BrandId == BrandId).ToList();
            ViewBag.model = new SelectList(modelist, "ModelId", "Name");
            return PartialView("ModelPartial");
        }

        public IActionResult Create()
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
            ViewBag.userid = HttpContext.Session.GetString("Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Advert data)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (data.Image != null)
                    {
                        var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                        foreach (var item in data.Image)
                        {

                            var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);
                            using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                            {
                                

                                item.CopyTo(dosyaAkisi);
                            }

                            data.Images.Add(new Image { ImageName = item.FileName });
                        }
                        data.Date = DateTime.Now;
                        data.Status = true;
                        db.Adverts.Add(data);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            ModelState.AddModelError("", "Bir hata oluştu");

            return View();
        }
        public IActionResult Update(int id)
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
            var update = db.Adverts.Where(x => x.Id == id).FirstOrDefault();
            return View(update);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Update(Advert data)
        {
            if (User.Identity.IsAuthenticated)
            {

                var update = db.Adverts.Where(x => x.Id == data.Id).FirstOrDefault();
                update.Date = DateTime.Now;
                update.AdvertNo = data.AdvertNo;
                update.BrandId = data.BrandId;
                update.CityId = data.CityId;
                update.FuelType = data.FuelType;
                update.Description = data.Description;
                update.Kilometer = data.Kilometer;
                update.ModelId = data.ModelId;
                update.ModelYear = data.ModelYear;
                update.Phone = data.Phone;
                update.Price = data.Price;
                update.StateId = data.StateId;
                update.UserAdminId = HttpContext.Session.GetString("Id");
                update.VitesType = data.VitesType;
                db.Adverts.Update(update);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Hata oluştu");

            return View();



        }
        public IActionResult ImageUpdate(int id)
        {
            var update = db.Images.Where(x => x.Id == id).FirstOrDefault();
            var image = db.Images.Where(x => x.AdvertId == id).ToList();
            ViewBag.image = image;
            return View(update);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ImageUpdate(Image data)
        {
            if (User.Identity.IsAuthenticated)
            {
                var image = db.Images.Where(x => x.Id == data.Id).FirstOrDefault();

                if (ModelState.IsValid)
                {

                   

                    if (data.File != null)
                    {

                        var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                       
                        
                           
                            var tamDosyaAdi = Path.Combine(dosyaYolu, data.File.FileName);
                            using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                            {

                            data.File.CopyTo(dosyaAkisi);
                            }
                            image.ImageName = data.File.FileName;
                            db.Images.Update(image);
                        



                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                }
                ModelState.AddModelError("", "Hata Oluştu");

            }
            return View();
        }

        public IActionResult ImageDelete(int id)
        {
            var image = db.Images.Find(id);
            if (User.Identity.IsAuthenticated)
            {


                if (ModelState.IsValid)
                {




                    db.Images.Remove(image);


                        db.SaveChanges();
                        return RedirectToAction("Index");

                    
                }
                ModelState.AddModelError("", "Hata Oluştu");
               
            }
            return RedirectToAction("ImageUpdate");
        }
        //if (User.Identity.IsAuthenticated)
        //{

        //    var image = db.Images.Find(data.Id);
        //    image.ImageName = data.File.ToString();
        //    var dosyaYolu = Path.Combine(env.WebRootPath, "img");


        //    var tamDosyaAdi = Path.Combine(dosyaYolu, image.ImageName);
        //    using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
        //    {


        //    }

        //    db.Images.Update(image);
        //    db.SaveChanges();



        //        if (data.File != null)
        //        {




        //        db.Images.Update(data);


        //        db.SaveChanges();
        //            return RedirectToAction("Index");

        //        }

        //    ModelState.AddModelError("", "Hata Oluştu");


        
        public IActionResult Delete(int id)
        {
            var delete = db.Adverts.Where(x => x.Id == id).FirstOrDefault();
            delete.Status = false;
            db.Adverts.Update(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AdvertImages(int id)
        {
            var brandimages = db.Images.Where(x => x.AdvertId == id).ToList();
            return View(brandimages);
        }
        public IActionResult ImageCreate(int id)
        {
            var image = db.Adverts.Where(x => x.Id == id).FirstOrDefault();
            var images = db.Images.Where(x => x.AdvertId == id).ToList();
            ViewBag.images = images;
            return View(image);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ImageCreate(Advert data)
        {
           
            if (User.Identity.IsAuthenticated)
            {
                var image = db.Adverts.Where(x => x.Id == data.Id).FirstOrDefault();

                if (!ModelState.IsValid)
                {

                

                    if (data.Image != null)
                    {
                    
                        var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                        foreach (var item in data.Image)
                        {

                            var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);
                            using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                            {

                                item.CopyTo(dosyaAkisi);
                            }

                            db.Images.Add(new Image { ImageName = item.FileName,AdvertId=image.Id });
                        }
                  

                       
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    
                }
                }
                ModelState.AddModelError("", "Hata Oluştu");
                
            }
            return View();
        }



    }
}
