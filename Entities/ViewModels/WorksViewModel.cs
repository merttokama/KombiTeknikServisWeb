using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class WorksViewModel
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
        public DateTime CompletionDate { get; set; } = new DateTime(1901, 01, 01);

    }
}
