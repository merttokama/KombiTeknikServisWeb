using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Branches")]
    public class Branches
    { 
        [Key]
        public string ID { get; set; }
        [Required]
        [StringLength(100)]
        public string BranchName { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(250)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        public string GMapLocation { get; set; }
    }
}
