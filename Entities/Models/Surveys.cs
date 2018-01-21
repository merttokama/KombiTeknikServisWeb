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
    [Table("Surveys")]
    public class Surveys
    {
        [Key]
        public string ID { get; set; }
        public string UserID { get; set; }
        [Required]
        public int Question1 { get; set; }
        [Required]
        public int Question2 { get; set; }
        [Required]
        public int Question3 { get; set; }
        [Required]
        public int Question4 { get; set; }
        [Required]
        public int Question5 { get; set; }
        [StringLength(250)]
        public string Explanation { get; set; }
        [Required]
        public int Score { get; set; } = 0;

        [ForeignKey("UserID")]
        public virtual ApplicationUser SurveysUserId { get; set; }

    }
}
