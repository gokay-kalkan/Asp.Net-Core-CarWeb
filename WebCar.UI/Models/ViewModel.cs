using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace WebCar.UI.Models
{
    public class ViewModel
    {
        public List <Advert> Adverts { get; set; }
        public Advert Advert { get; set; }
    }
}
