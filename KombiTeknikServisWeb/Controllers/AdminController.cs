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
        #region SLIDER YÜZDE VE SAYILAR

        public static string isimSoyisim;
        public static string uyeSayisi;
        public static string aktifArizaSayisi;
        public static string cozulenArizaSayisi;
        public static string yuzdeAktifAriza;
        public static string yuzdeTamamlananAriza;
        public static string yuzdeUyeSayisi;

        #endregion

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

            int sayacCozulenArizaSayisi = 0; // SLIDER YÜZDE BÖLÜMÜ
            foreach (var item2 in calismalar)
            {
                if (item2.FaultIsResolved == true) // SLIDER YÜZDE BÖLÜMÜ
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

            #region SLIDER YÜZDE BÖLÜMÜ


            aktifArizaSayisi = calismalar.Count().ToString();
            cozulenArizaSayisi = sayacCozulenArizaSayisi.ToString();
            double a = Convert.ToDouble(cozulenArizaSayisi);
            double b = Convert.ToDouble(aktifArizaSayisi);
            double c = arizaList.Count();
            yuzdeUyeSayisi = Math.Round((100 / (c / Convert.ToDouble(uyeSayisi))), 0).ToString();
            if (a != 0 && b != 0)
            {
                yuzdeAktifAriza = Math.Round((100 / (c / b)), 0).ToString();
                yuzdeTamamlananAriza = Math.Round((100 / (c / a)), 0).ToString();
            }
            else if (a == 0 && b != 0)
            {
                yuzdeAktifAriza = Math.Round((100 / (c / b)), 0).ToString();
                yuzdeTamamlananAriza = "0";
            }
            else if (a != 0 && b == 0)
            {
                yuzdeTamamlananAriza = Math.Round((100 / (c / a)), 0).ToString();
                yuzdeAktifAriza = "0";
            }

            #endregion

            return View();
        }
        public ActionResult NewFaultReports()
        {
            var onayArizaList = new FaultReportConfirmationRepo().GetAll().ToList();
            var arizaList = new FaultReportsRepo().GetAll().ToList();
            List<FaultReportsViewModel> ArizaBildirimleri = new List<FaultReportsViewModel>();
            foreach (var item in arizaList)
            {
                foreach (var item2 in onayArizaList)
                {
                    if (item2.FaultReportID == item.ID && item2.Approved == false)
                    {
                        ArizaBildirimleri.Add(new FaultReportsViewModel()
                        {
                            ID = item.ID,
                            UserID = item.UserID,
                            Address = item.Address,
                            Description = item.Description,
                            LocationX = item.LocationX,
                            LocationY = item.LocationY,
                            FaultReportDate = item.FaultReportDate
                        });

                    }
                }
            }
            return View(ArizaBildirimleri);
        }

        public ActionResult ActiveWorks()
        {
            var onayArizaList = new FaultReportConfirmationRepo().GetAll().ToList();
            var arizaList = new FaultReportsRepo().GetAll().ToList();
            var islerList = new WorksRepo().GetAll().ToList();
            List<WorksViewModel> ArizaBildirimleri = new List<WorksViewModel>();
            foreach (var item in arizaList)
            {
                foreach (var item2 in onayArizaList)
                {
                    if (item2.FaultReportID == item.ID && item2.Approved == true)
                    {
                        foreach (var item3 in islerList)
                        {
                            if (item3.FaultIsResolved == false)
                            {
                                ArizaBildirimleri.Add(new WorksViewModel()
                                {
                                    ID = item3.ID,
                                    FaultReportID = item3.FaultReportID,
                                    TechnicionID = item3.TechnicionID,
                                    FaultIsResolved = false,
                                });
                            }
                        }
                    }
                }
            }
            return View(ArizaBildirimleri);
        }
        public ActionResult ComplateWorks()
        {
            var onayArizaList = new FaultReportConfirmationRepo().GetAll().ToList();
            var arizaList = new FaultReportsRepo().GetAll().ToList();
            var islerList = new WorksRepo().GetAll().ToList();
            List<WorksViewModel> ArizaBildirimleri = new List<WorksViewModel>();
            foreach (var item in arizaList)
            {
                foreach (var item2 in onayArizaList)
                {
                    if (item2.FaultReportID == item.ID && item2.Approved == true)
                    {
                        foreach (var item3 in islerList)
                        {
                            if (item3.FaultIsResolved == true)
                            {
                                ArizaBildirimleri.Add(new WorksViewModel()
                                {
                                    ID = item3.ID,
                                    FaultReportID = item3.FaultReportID,
                                    TechnicionID = item3.TechnicionID,
                                    CompletionDate = item3.CompletionDate
                                });
                            }
                        }
                    }
                }
            }
            return View(ArizaBildirimleri);
        }

        public ActionResult UserList()
        {
            var userManager = MembershipTools.NewUserStore();
            var roleManager = MembershipTools.NewRoleManager();
            var TumKullanicilar = userManager.Context.Set<ApplicationUser>().ToList();
            List<ProfileViewModel> KullanicilarProfilList = new List<ProfileViewModel>();
            TumKullanicilar.ForEach(x =>
            KullanicilarProfilList.Add(new ProfileViewModel()
            {
                Name = x.Name,
                Surname = x.Surname,
                Username = x.UserName,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                Role = roleManager.FindById(x.Roles.FirstOrDefault().RoleId).Name
            })
            );
            return View(KullanicilarProfilList);
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