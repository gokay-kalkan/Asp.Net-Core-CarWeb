using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public int AdvertId { get; set; }
        public virtual Advert Advert { get; set; }
    }
}
