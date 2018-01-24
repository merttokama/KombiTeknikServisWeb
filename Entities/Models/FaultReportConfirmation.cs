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
        public string ID { get; set; }
        public string OperationID { get; set; }
        [Required]
        public string FaultReportID { get; set; }
        [Required]
        public bool Approved { get; set; } = false;
        [Required]
        [StringLength(250)]
        public string Message { get; set; }

        [ForeignKey("OperationID")]
        public virtual ApplicationUser OperationId { get; set; }

        [ForeignKey("FaultReportID")]
        public virtual ApplicationUser FaultReportId { get; set; }
    }
}
