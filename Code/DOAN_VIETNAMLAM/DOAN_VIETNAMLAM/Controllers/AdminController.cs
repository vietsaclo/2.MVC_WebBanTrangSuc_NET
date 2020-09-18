using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN_VIETNAMLAM.Models;
namespace DOAN_VIETNAMLAM.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        BanTrangSucClasses1DataContext db = new BanTrangSucClasses1DataContext();
        public ActionResult Index()
        {
            TAIKHOAN tk = Session["ss_user"] as TAIKHOAN;
            if (tk == null || tk.PHAN_QUYEN != 1)
                return RedirectToAction("Index","Home");
            return View();
        }

        public ActionResult SanPham()
        {
            List<TRANGSUC> lstsp = db.TRANGSUCs.ToList();
            return View(lstsp);
        }

        public ActionResult chitiet(int masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == masp);
            return View(sp);
        }

        public ActionResult suaSP(int masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == masp);
            return View(sp);
        }

        public ActionResult XlySuaSP(FormCollection fc, int masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == masp);
            sp.MALOAI = int.Parse(fc["maloai"]);
            sp.TENTRANGSUC = fc["tensp"];
            sp.GIA = int.Parse(fc["gia"]);
            sp.HINHANH = fc["hinh"];
            sp.NGAYDANG = DateTime.Parse(fc["ngaydang"]);
            sp.MOTA = fc["mota"];
            db.SubmitChanges();
            return RedirectToAction("chitiet", new { masp = sp.MATRANGSUC });

        }

        public ActionResult TaiKhoan()
        {
            TAIKHOAN tk = Session["ss_user"] as TAIKHOAN;
            if (tk == null || tk.PHAN_QUYEN != 1)
                return RedirectToAction("Index", "Home");
            List<TAIKHOAN> lstTK = db.TAIKHOANs.ToList();
            return View(lstTK);
        }

        public ActionResult xoaTaiKhoan(int matk)
        {
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.MATAIKHOAN == matk);
            if (tk != null)
            {
                db.TAIKHOANs.DeleteOnSubmit(tk);
                db.SubmitChanges();
                return RedirectToAction("TaiKhoan");
            }
            return RedirectToAction("TaiKhoan");
        }

        public ActionResult themTK()
        {
            return View();
        }

        public ActionResult XlyThemTK(FormCollection fc, TAIKHOAN tk)
        {
            tk.UNAME = fc["username"];
            tk.PASS = fc["pass"];
            tk.FULL_NAME = fc["fullname"];
            tk.EMAIL_ADDRESS = fc["email"];
            tk.PHAN_QUYEN = byte.Parse(fc["phanquyen"]);

            db.TAIKHOANs.InsertOnSubmit(tk);
            db.SubmitChanges();
            return RedirectToAction("TaiKhoan");
        }

        public ActionResult suaTK(int matk)
        {
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.MATAIKHOAN == matk);
            return View(tk);
        }

        public ActionResult xLySuaTK(FormCollection fc, int matk)
        {
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.MATAIKHOAN == matk);

            tk.UNAME = fc["username"];
            tk.PASS = fc["pass"];
            tk.FULL_NAME = fc["fullname"];
            tk.EMAIL_ADDRESS = fc["email"];
            tk.PHAN_QUYEN = byte.Parse(fc["phanquyen"]);

            db.SubmitChanges();
            return RedirectToAction("TaiKhoan");
        }

        public ActionResult Loai()
        {
            List<THELOAI> lstLoai = db.THELOAIs.ToList();
            return View(lstLoai);
        }

        public ActionResult themLoai()
        {
            return View();
        }

        public ActionResult xLyThemLoai(FormCollection fc, THELOAI tl)
        {
            tl.TENLOAI = fc["tenloai"];
            tl.ICON_THELOAI = fc["icon"];

            db.THELOAIs.InsertOnSubmit(tl);
            db.SubmitChanges();
            return RedirectToAction("Loai");
        }
    }
}
