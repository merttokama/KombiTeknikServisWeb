using BLL.Account;
using BLL.Repository;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KombiTeknikServisWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SecilenMenu(0);
            return View();
        }

        public ActionResult ComingSoon()
        {
            return View();
        }

        public ActionResult About()
        {
            SecilenMenu(1);
            return View();
        }
        public ActionResult Contact()
        {
            SecilenMenu(4);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FaultReport(FaultReportsViewModel model)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var userManager = MembershipTools.NewUserManager();
            var user = userManager.FindById(HttpContext.User.Identity.GetUserId());
            SecilenMenu(2);
            var ariza = new FaultReports()
            {
                Address = model.Address,
                Description = model.Description,
                LocationX = model.LocationX,
                LocationY = model.LocationY,
                UserID = user.Id
            };
            try
            {
                new FaultReportsRepo().Insert(ariza);
            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    Response.Write(string.Format("Entity türü \"{0}\" şu hatalara sahip \"{1}\" Geçerlilik hataları:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Response.Write(string.Format("- Özellik: \"{0}\", Hata: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                    Response.End();
                }

            }
            catch (DbUpdateException ex)
            {
                Response.Write(ex.Message);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View();
        }
        public ActionResult FaultReport()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            SecilenMenu(2);
            return View();
        }

        public ActionResult ServiceAreas()
        {
            SecilenMenu(3);
            return View();
        }

        public static string[] dizi = new string[6];
        public static void SecilenMenu(int secilen)
        {
            for (int i = 0; i < dizi.Length; i++)
            {
                if (i == secilen)
                {
                    dizi[secilen] = "current-menu-item";
                }
                else
                {
                    dizi[i] = string.Empty;
                }
            }
        }
        [Authorize(Roles = "Admin,User")]
        public ActionResult Mesajlar()
        {
            var model = new MessageRepo().Queryable().Where(x => x.SendBy == HttpContext.User.Identity.GetUserId()).ToList();
            return View(model);
        }
        #region Partials

        public PartialViewResult SliderResult()
        {

            return PartialView("_PartialSlider");
        }
        public PartialViewResult SectionServiceResult()
        {

            return PartialView("_PartialSectionService");
        }
        public PartialViewResult SectionResult()
        {

            return PartialView("_PartialSection");
        }
        public PartialViewResult SectionContactUsResult()
        {

            return PartialView("_PartialSectionContactUs");
        }
        public PartialViewResult SectionWhatsClientSayResult()
        {

            return PartialView("_PartialSectionWhatsClientSay");
        }
        public PartialViewResult SectionOurPartnerResult()
        {

            return PartialView("_PartialSectionOurPartner");
        }

        public PartialViewResult FooterResult()
        {
            return PartialView("_PartialFooter");
        }

        public PartialViewResult SectionComingSoonResult()
        {
            return PartialView("_PartialSectionComingSoon");
        }

        public PartialViewResult LiLoYesResult()
        {
            return PartialView("_PartialLiLoYes");
        }

        public PartialViewResult LiLoNoResult()
        {
            return PartialView("_PartialLiLoNo");
        }

        public PartialViewResult HeaderResult()
        {
            return PartialView("_PartialHeader");
        }

        #endregion

    }
}