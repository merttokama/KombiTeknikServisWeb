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
        public string ID { get; set; }
        [Required]
        public string FaultReportsID { get; set; }
        [Required]
        public byte Image { get; set; }

        [ForeignKey("FaultReportsID")]
        public virtual FaultReports FaultReportsId { get; set; }
    }
}
