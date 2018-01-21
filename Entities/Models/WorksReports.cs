﻿using Entity.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("WorksReports")]
    public class WorksReports
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string WorkID { get; set; } 
        [Required]
        [StringLength(250)]
        public string Report { get; set; }

        [ForeignKey("WorkID")]
        public virtual Works WorkId { get; set; }
    }
}
