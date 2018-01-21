using Entity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.IdentityModels;
using Entities.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext()
            : base("name=MyCon")
        { }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Works> Works { get; set; }
        public virtual DbSet<WorksReports> WorksReports { get; set; }
        public virtual DbSet<Technicions> Technicions { get; set; }
        public virtual DbSet<Surveys> Surveys { get; set; }
        public virtual DbSet<FaultReports> FaultReports { get; set; }
        public virtual DbSet<FaultReportConfirmation> FaultReportConfirmation { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
    }
}