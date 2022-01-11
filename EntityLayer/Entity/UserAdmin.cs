using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class UserAdmin:IdentityUser
    {
        public string FullName { get; set; }
        public virtual List<Advert> Adverts { get; set; }
    }
}
