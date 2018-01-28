using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class FaultReportConfirmationViewModel
    {
        public int FaultReportID { get; set; }
        [Required]
        public bool Approved { get; set; } = false;
        [Required]
        [StringLength(250)]
        public string Message { get; set; }
    }
}
