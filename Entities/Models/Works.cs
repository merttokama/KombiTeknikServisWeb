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
    [Table("Works")]
    public class Works
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string FaultReportID { get; set; }
        [Required]
        public string TechnicionID { get; set; }
        [Required]
        public bool FaultIsResolved { get; set; } = false;
        [Required]
        public DateTime CompletionDate { get; set; } = new DateTime(01,01,01);

        [ForeignKey("TechnicionID")]
        public virtual Technicions TechnicionsId { get; set; }
        [ForeignKey("FaultReportID")]
        public virtual FaultReports FaultReportsId { get; set; }
        public virtual List<WorksReports> WorksReports { get; set; } = new List<WorksReports>();

    }
}
