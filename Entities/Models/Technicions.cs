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
    [Table("Technicions")]
    public class Technicions
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        [Required]
        public bool Appropriate { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Works> Works { get; set; } = new List<Works>();
    }
}
