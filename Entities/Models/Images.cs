using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entities.Models
{
    [Table("Images")]
    public class Images
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int FaultReportsID { get; set; }

        public string DosyaYolu { get; set; }
        public string Uzanti { get; set; }

        [ForeignKey("FaultReportsID")]
        public virtual FaultReports FaultReports { get; set; }
    }
}
