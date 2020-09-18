using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN_VIETNAMLAM.Models;
namespace DOAN_VIETNAMLAM.Controllers
{
    public class GioHangController : Controller
    {
        BanTrangSucClasses1DataContext db = new BanTrangSucClasses1DataContext();
        public List<Item> layGiohang()
        {
            List<Item> lstGiohang = Session["gh"] as List<Item>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Item>();
                Session["gh"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult pick(int masp)
        {
            List<Item> lstGiohang = layGiohang();
            Item sp = lstGiohang.FirstOrDefault(t => t.masp == masp);
            if (sp == null)
            {
                sp = new Item(masp);
                lstGiohang.Add(sp);
                return RedirectToAction("SanPham", "Home");
            }
            else
            {
                sp.soluong++;
                return RedirectToAction("SanPham", "Home");
            }
            
        }

        public ActionResult xemGioHang()
        {
            List<Item> lstGioHang = layGiohang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("SanPham", "Home");
            }
            ViewBag.soluong = soluong();
            ViewBag.tongtien = tongtien();
            return View(lstGioHang);
        }

        public int soluong()
        {
            int tongsoluong = 0;
            List<Item> lstgioHang = layGiohang();
            if (lstgioHang != null)
            {
                tongsoluong = lstgioHang.Sum(t => t.soluong);
            }
            return tongsoluong;
        }

        public int tongtien()
        {
            int tongtien = 0;
            List<Item> lstgiohang = layGiohang();
            if (lstgiohang != null)
            {
                tongtien = lstgiohang.Sum(t => t.thanhtien);
            }
            return tongtien;
        }

        public ActionResult xoaSp(int masp)
        {
            List<Item> lstGiohang = Session["gh"] as List<Item>;
            if (lstGiohang != null)
            {
                lstGiohang.RemoveAll(t => t.masp == masp);
            }
            return RedirectToAction("xemGioHang");
               
        }

        public ActionResult chuanBiDatHang()
        {
            if (Session["ss_user"] == null || Session["gh"] == null)
                return RedirectToAction("dangNhap","login");
            return View();
        }

        [HttpPost]
        public ActionResult xacNhanDatHang(FormCollection fc)
        {
            TAIKHOAN tk = Session["ss_user"] as TAIKHOAN;
            List<Item> gioHang = Session["gh"] as List<Item>;
            if (tk == null || gioHang == null)
                return RedirectToAction("Index","Home");
            HOADON hd = new HOADON();
            hd.MATAIKHOAN = tk.MATAIKHOAN;
            hd.NGAYLAP = DateTime.Now;
            try
            {
                hd.NGAYGIAO = DateTime.Parse(fc["txtNgayGiao"].ToString());
            }
            catch
            {
                ViewBag.thongBao2 = "Bạn Nhập Ngày không hợp lệ";
                return View("chuanBiDatHang");
            }
            hd.TONGTIEN = gioHang.Sum(n=>n.thanhtien);
            db.HOADONs.InsertOnSubmit(hd);
            db.SubmitChanges();
            CHITIETHD ct;
            foreach(Item it in gioHang){
                ct = new CHITIETHD();
                ct.MAHD = hd.MAHD;
                ct.MATRANGSUC = it.masp;
                ct.SOLUONG = it.soluong;
                db.CHITIETHDs.InsertOnSubmit(ct);
            }
            db.SubmitChanges();
            ViewBag.thongBao = "Hóa Đơn Của Bạn Được đặt thành công";
            Session["gh"] = null;

            return View("chuanBiDatHang");
        }
    }
}
