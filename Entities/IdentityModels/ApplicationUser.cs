using Entities.Models;
using Entity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string Surname { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string ActivationCode { get; set; }

        public virtual List<Message> Messages { get; set; } = new List<Message>();
        public virtual List<Surveys> Surveys { get; set; } = new List<Surveys>();
        public virtual List<FaultReports> FaultReports { get; set; } = new List<FaultReports>();

        public virtual List<Technicions> Technicions { get; set; } = new List<Technicions>();
    }
}
