using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
   public class Model
    {
        [Key]
        public int ModelId { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [StringLength(50, ErrorMessage = "Max 50 karakter olabilir")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
