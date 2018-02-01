using BLL.Account;
using BLL.Repository;
using Entities.Models;
using Entities.ViewModels;
using Entity.Enums;
using Entity.IdentityModels;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var userManager = MembershipTools.NewUserStore();
            var TumKullanicilar = userManager.Context.Set<ApplicationUser>().ToList();
            var teknikerler = new TechnicionsRepo().GetAll().ToList();
            var onayArizaList = new FaultReportConfirmationRepo().GetAll().ToList();
            var arizaList = new FaultReportsRepo().GetAll().ToList();
            FaultListViewModel model = new FaultListViewModel();
            foreach (var item1 in teknikerler)
            {
                if (item1.Appropriate == true)
                {
                    foreach (var item in TumKullanicilar)
                    {
                        if (item1.UserID == item.Id)
                        {
                            model.Teknikerler.Add(item);
                        }
                    }
                }
            }
            foreach (var item in arizaList)
            {
                foreach (var item2 in onayArizaList)
                {
                    if (item2.FaultReportID == item.ID && item2.Approved == false)
                    {
                        model.Arizalar.Add(item);
                    }
                }
            }
            return View(model);
        }
        public ActionResult NewFaultReportsTeknisyen(int arizaId, string teknisyenId)
        {
            var teknikerler = new TechnicionsRepo().GetAll().ToList();
            foreach (var item in teknikerler)
            {
                if (item.UserID == teknisyenId)
                {
                    var aktifIsler = new Works()
                    {
                        ID = 0,
                        FaultIsResolved = false,
                        FaultReportID = arizaId,
                        TechnicionID = item.ID,
                        CompletionDate = new DateTime(1901, 01, 01)
                    };
                    try
                    {
                        new WorksRepo().Insert(aktifIsler);
                        var teknikerUygunDegil = new TechnicionsRepo().GetAll().FirstOrDefault(x => x.ID == item.ID);
                        teknikerUygunDegil.Appropriate = false;
                        var onaylananArızalar = new FaultReportConfirmationRepo().GetAll().FirstOrDefault(x => x.FaultReportID == aktifIsler.FaultReportID);
                        onaylananArızalar.Approved = true;
                        new FaultReportConfirmationRepo().Update();
                        new TechnicionsRepo().Update();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return RedirectToAction("NewFaultReports");
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
                            if (item3.FaultIsResolved == false && item3.FaultReportID == item2.FaultReportID)
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
        public ActionResult ActiveWorksComplated(int isId)
        {
            var teknikerId = 0;
            var tumIsler = new WorksRepo().GetAll().ToList();
            var tumTeknikerler = new TechnicionsRepo().GetAll().ToList();
            foreach (var item in tumIsler)
            {
                if (item.ID == isId)
                {
                    item.FaultIsResolved = true;
                    item.CompletionDate = DateTime.Now;
                    teknikerId = item.TechnicionID;
                }
            }
            if (teknikerId != 0)
            {
                foreach (var item in tumTeknikerler)
                {
                    if (item.ID == teknikerId)
                    {
                        item.Appropriate = true;
                    }
                }
            }
            new WorksRepo().Update();
            new TechnicionsRepo().Update();
            return RedirectToAction("ActiveWorks");
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
                            if (item3.FaultIsResolved == true && item2.FaultReportID == item3.FaultReportID)
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
                Role = roleManager.FindById(x.Roles.FirstOrDefault().RoleId).Name,
            }));
            return View(KullanicilarProfilList);
        }
        public ActionResult UserListChange(string ProfilRol, string profilUsername)
        {
            var username = MembershipTools.NewUserManager().FindByName(profilUsername);
            var userid = username.Id;
            var user = MembershipTools.NewUserManager().FindById(userid);
            var oldroleId = user.Roles.FirstOrDefault().RoleId;
            var oldrolename = MembershipTools.NewRoleManager().FindById(oldroleId).Name;
            var userManager = MembershipTools.NewUserManager();
            if (ProfilRol == "Technicians")
            {
                var technician = new Technicions()
                {
                    ID = 0,
                    UserID = userid,
                    Appropriate = true
                };
                new TechnicionsRepo().Insert(technician);
            }
            userManager.RemoveFromRole(userid, oldrolename);
            userManager.AddToRole(userid, ProfilRol);
            return RedirectToAction("UserList");
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