using BLL.Account;
using BLL.Repository;
using Entities.Models;
using Entities.ViewModels;
using Entity.IdentityModels;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KombiTeknikServisWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public static string isimSoyisim;
        public static string uyeSayisi;
        public static string aktifArizaSayisi;
        public static string cozulenArizaSayisi;
        public static string yuzdeAktifAriza;
        public static string yuzdeTamamlananAriza;

        [Authorize]
        public ActionResult Index()
        {
            var userManager = MembershipTools.NewUserManager();
            var user = userManager.FindById(HttpContext.User.Identity.GetUserId());
            isimSoyisim = user.Name + " " + user.Surname + " ";

            uyeSayisi = userManager.Users.Count().ToString();

            var onayArizaList = new FaultReportConfirmationRepo().GetAll().Select(x => x.FaultReportID);
            var arizaList = new FaultReportsRepo().GetAll().Select(x => x.ID);
            var olmayanlar = arizaList.Except(onayArizaList);
            foreach (var item in olmayanlar)
            {
                var onayAriza = new FaultReportConfirmation();
                onayAriza.FaultReportID = item;
                onayAriza.Approved = false;
                onayAriza.ID = 0;
                try
                {
                    new FaultReportConfirmationRepo().Insert(onayAriza);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            var yeniOnayArizaList = new FaultReportConfirmationRepo().GetAll();
            var calismalar = new WorksRepo().GetAll();
            aktifArizaSayisi = calismalar.Count().ToString();
            int sayacCozulenArizaSayisi = 0;
            foreach (var item2 in calismalar)
            {
                if (item2.FaultIsResolved == true)
                {
                    sayacCozulenArizaSayisi++;
                }
                foreach (var item in yeniOnayArizaList)
                {
                    var t = new Works();
                    if (item.Approved == true && item.FaultReportID != item2.FaultReportID)
                    {
                        t.TechnicionID = 1;
                        t.ID = 0;
                        t.FaultReportID = item.FaultReportID;
                        t.FaultIsResolved = false;
                        try
                        {
                            new WorksRepo().Insert(t);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            cozulenArizaSayisi = sayacCozulenArizaSayisi.ToString();
            int a = Convert.ToInt32(cozulenArizaSayisi);
            int b = Convert.ToInt32(aktifArizaSayisi);
            if (a != 0 && b != 0)
            {
                yuzdeAktifAriza = (100 / ((a + b) / a)).ToString();
                yuzdeTamamlananAriza = (100 / ((a + b) / b)).ToString();
            }
            else if (a == 0 && b != 0)
            {
                yuzdeTamamlananAriza = (100 / ((a + b) / b)).ToString();
                yuzdeAktifAriza = "0";
            }
            else if (a != 0 && b == 0)
            {
                yuzdeAktifAriza = (100 / ((a + b) / a)).ToString();
                yuzdeTamamlananAriza = "0";
            }

            return View();
        }

        #region PARTIALS
        public PartialViewResult APHeaderResult()
        {
            return PartialView("_PartialAPHeader");
        }
        public PartialViewResult APSidebarResult()
        {
            return PartialView("_PartialAPSidebar");
        }
        #endregion
    }
}