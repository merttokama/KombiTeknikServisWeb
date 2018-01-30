using Entities.Models;
using Entity.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
   public class FaultListViewModel
    {
        public List<FaultReports> Arizalar { get; set; } = new List<FaultReports>();

        public List<ApplicationUser> Teknikerler { get; set; } = new List<ApplicationUser>();
    }
}
