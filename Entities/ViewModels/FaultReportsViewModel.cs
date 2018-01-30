using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entities.ViewModels
{
    public class FaultReportsViewModel
    {
        [Key]
        public int ID { get; set; }
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
        public DateTime FaultReportDate { get; set; } = DateTime.Now;
        [Required]
        public List<HttpPostedFileBase> Images { get; set; } = new List<HttpPostedFileBase>();
        public string DosyaYolu { get; set; }
        public string Uzanti { get; set; }
    }
}
