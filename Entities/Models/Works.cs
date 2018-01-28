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
        public int ID { get; set; }
        [Required]
        public int FaultReportID { get; set; }
        [Required]
        public int TechnicionID { get; set; } = 1;
        [Required]
        public bool FaultIsResolved { get; set; } = false;
        [Required]
        public DateTime CompletionDate { get; set; } = new DateTime(1901,01,01);

        [ForeignKey("TechnicionID")]
        public virtual Technicions Technicions { get; set; }
        [ForeignKey("FaultReportID")]
        public virtual FaultReports FaultReports { get; set; }
        public virtual List<WorksReports> WorksReports { get; set; } = new List<WorksReports>();

    }
}
