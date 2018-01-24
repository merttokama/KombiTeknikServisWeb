using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class FaultReportsViewModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string LocationX { get; set; }
        [Required]
        public string LocationY { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Arıza Açıklaması")]
        public string Description { get; set; }


        [Required]
        public string FaultReportsID { get; set; }
        [Required]
        [Display(Name = "Arızayı Açıklayan Fotoğraf")]
        public byte Image { get; set; }
    }
}
