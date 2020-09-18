using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN_VIETNAMLAM.Models;
namespace DOAN_VIETNAMLAM.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        BanTrangSucClasses1DataContext db = new BanTrangSucClasses1DataContext();
        public ActionResult Index()
        {
            TAIKHOAN tk = Session["ss_user"] as TAIKHOAN;
            if (tk != null && tk.PHAN_QUYEN == 1)
                return RedirectToAction("Index","Admin");
            return View();
        }

        public ActionResult SanPham()
        {
            List<TRANGSUC> lstTS = db.TRANGSUCs.ToList();
            return View(lstTS);
        }

        public ActionResult LoaiSP()
        {
            List<THELOAI> lstLoai = db.THELOAIs.ToList();
            return PartialView(lstLoai);
        }

        public ActionResult timSPtheoLoai(string maLoai)
        {
            List<TRANGSUC> lstSP = db.TRANGSUCs.Where(t => t.MALOAI == int.Parse(maLoai)).ToList();
            return View(lstSP);
        }

        public ActionResult Chitiet(string masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == int.Parse(masp));
            return View(sp);
        }

    }
}
