using BLL.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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

        public ActionResult FaultReport()
        {
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
            for (int i=0;i<dizi.Length;i++)
            {
                if (i==secilen)
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