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
    [Table("FaultReports")]
    public class FaultReports
    {
        [Key]
        public string ID { get; set; }
        public string UserID { get; set; }
        [Required]
        public string LocationX { get; set; }
        [Required]
        public string LocationY { get; set; }
        [Required]
        [StringLength(250)]
        public string Address { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        public DateTime FaultReportDate { get; set; } = DateTime.Now;

        [ForeignKey("UserID")]
        public virtual ApplicationUser FaultReportsUserId { get; set; }

        public virtual List<Works> Works { get; set; } = new List<Works>();
        public virtual List<Images> Images { get; set; } = new List<Images>();
        public virtual List<FaultReportConfirmation> FaultReportConfirmation { get; set; } = new List<FaultReportConfirmation>();
    }
}
