using Entity.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("FaultReportConfirmation")]
    public class FaultReportConfirmation
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int FaultReportID { get; set; }
        [Required]
        public bool Approved { get; set; } = false;
        [Required]
        [StringLength(250)]
        public string Message { get; set; } = "Arıza kaydınız incelendi. 3 saat içerisinde teknisyenimiz telefon numaranız aracılığıyla sizinle iletişime geçecek ve kombi arızası çözümü için bildirdiğiniz konuma gelecektir.";

        [ForeignKey("FaultReportID")]
        public virtual FaultReports FaultReports { get; set; }
    }
}
