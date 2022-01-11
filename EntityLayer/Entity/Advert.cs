using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class Advert
    {
        public Advert()
        {
            Images = new List<Image>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public string AdvertNo { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public int Kilometer { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public int ModelYear { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public string FuelType { get; set; }
        //yakıt türü
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public string VitesType { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public int StateId { get; set; }
        public virtual State State { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }
        [Required(ErrorMessage = "Boş Geçmeyiniz")]

        public int CityId { get; set; }
        public virtual City City { get; set; }
        [NotMapped]
        public IFormFile[] Image { get; set; }

        public virtual  List<Image> Images { get; set; }
        public string UserAdminId { get; set; }

        public virtual UserAdmin UserAdmin { get; set; }

        public bool Status { get; set; }
    }
}
