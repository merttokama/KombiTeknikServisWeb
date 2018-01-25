using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Images")]
    public class Images
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int FaultReportsID { get; set; }
        [Required]
        public byte Image { get; set; }

        [ForeignKey("FaultReportsID")]
        public virtual FaultReports FaultReports { get; set; }
    }
}
