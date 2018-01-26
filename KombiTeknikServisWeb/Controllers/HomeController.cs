using BLL.Account;
using BLL.Repository;
using BLL.Settings;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
        public ActionResult BasariliIslem()
        {
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
        public ActionResult SayfaGirisYetkisi()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FaultReport(FaultReportsViewModel model)
        {
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
            catch (Exception ex)
            {
                throw ex;
            }
            //------------------ IMAGE

            if (model.Images.Any())
            {
                foreach (var dosya in model.Images)
                {
                    string fileName = Path.GetFileNameWithoutExtension(dosya.FileName);
                    string extName = Path.GetExtension(dosya.FileName);
                    fileName = SiteSettings.UrlFormatConverter(fileName);
                    fileName += Guid.NewGuid().ToString().Replace("-", "");
                    var directoryPath = Server.MapPath("~/Uploads/products");
                    var filePath = Server.MapPath("~/Uploads/products/") + fileName + extName;
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);
                    dosya.SaveAs(filePath);
                    ResimBoyutlandir(400, 300, filePath);
                    try
                    {
                        new ImagesRepo().Insert(new Images()
                        {
                            DosyaYolu = @"/Uploads/products/" + fileName + extName,
                            FaultReportsID = ariza.ID,
                            Uzanti = extName.Substring(1)
                        });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            //------------------- IMAGE END

            return RedirectToAction("BasariliIslem", "Home");
        }

        public void ResimBoyutlandir(int en, int boy, string yol)
        {
            WebImage img = new WebImage(yol);
            img.Resize(en, boy, false);
            img.AddTextWatermark("Kombi Master", fontColor: "Tomato", fontSize: 18, fontFamily: "Verdana");
            img.Save(yol);
        }
        public ActionResult FaultReport()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SayfaGirisYetkisi", "Home");
            }
            if (HttpContext.User.IsInRole("Passive"))
            {
                return RedirectToAction("SayfaGirisYetkisi", "Home");
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