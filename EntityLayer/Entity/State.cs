using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [StringLength(20, ErrorMessage = "Max 20 karakter olabilir")]
        public string Name { get; set; }
       
    }
}
