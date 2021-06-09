using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ShoppingCartController : Controller
    {
        public string ranDom(int chieudai)
        {
            chieudai = 25; const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < chieudai--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        CT25Team13Entities db = new CT25Team13Entities();
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //Phuong thuc add item vao gio hang
        public ActionResult AddtoCart(FormCollection form)
        {
            GetCart();
            ShowToCart();
            string id_pro = form["ID_Product"];
            int quantity = int.Parse(form["quantity"]);
            int gia = int.Parse(form["Gia"]);
            string size = form["Size"];
            GetCart().Add(id_pro, quantity, gia, size, User.Identity.GetUserId());
            GetCart().Total_Money(User.Identity.GetUserId());
            GetCart().Total_Quantity_in_Cart(User.Identity.GetUserId(), GetCart().Items.Count());
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //Trang gio hang
        public ActionResult ShowToCart()
        {
            GetCart();
            Cart cart = Session["Cart"] as Cart;
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Customer") || User.IsInRole("Admin") || User.IsInRole("Nhân viên"))
                {
                    var model = db.CHITIETGIOHANGs.ToList();
                    model = model.Where(c => c.MAGIOHG == User.Identity.GetUserId()).ToList();
                    if (cart == null || cart.Items.Count() == 0 || Session["Cart"] == null)
                    {
                        GetCart();
                        if (model.Count() != 0)
                        {
                            for (int i = 0; i < model.Count(); i++)
                            {
                                string masp = model[i].MASP;
                                int quantity = (int)model[i].SOLUONG;
                                string size = model[i].Size;
                                SANPHAM pro = db.SANPHAMs.SingleOrDefault(s => s.MASP == masp);
                                GetCart().Show(pro, size, quantity, User.Identity.GetUserId());
                            }
                        }
                    }
                    else
                    {
                        GetCart();
                        cart.ClearCart(null);
                        if (model.Count() != 0)
                        {
                            for (int i = 0; i < model.Count(); i++)
                            {
                                string masp = model[i].MASP.ToString();
                                int quantity = (int)model[i].SOLUONG;
                                string size = model[i].Size;
                                SANPHAM pro = db.SANPHAMs.SingleOrDefault(s => s.MASP == masp);
                                GetCart().Show(pro, size, quantity, User.Identity.GetUserId());
                            }
                        }
                    }
                }
            }

            GetCart();
            Cart cart1 = Session["Cart"] as Cart;
            return View(cart1);
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_pro = form["ID_Product"];
            int quantity = int.Parse(form["Quantity"]);
            string size = form["Size"];
            cart.Update_Quantity_Shopping(id_pro, quantity, size, User.Identity.GetUserId());
            cart.Total_Money(User.Identity.GetUserId());
            cart.Total_Quantity_in_Cart(User.Identity.GetUserId(), GetCart().Items.Count());
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_pro = form["ID_Product"];
            string size = form["Size"];
            GetCart().Remove_CartItem(id_pro, size, User.Identity.GetUserId());
            ShowToCart();
            GetCart().Total_Money(User.Identity.GetUserId());
            GetCart().Total_Quantity_in_Cart(User.Identity.GetUserId(), GetCart().Items.Count());
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            GetCart();
            ShowToCart();
            Cart cart = Session["Cart"] as Cart;

            if (cart != null)
            {
                ViewBag.QuantityCart = GetCart().Items.Count();
                return PartialView("BagCart");
            }
            else
            {
                ViewBag.QuantityCart = 0;
            }
            cart.Total_Quantity_in_Cart(User.Identity.GetUserId(), GetCart().Items.Count());
            return PartialView("BagCart");
        }
        public ActionResult Clear_Cart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                GetCart().ClearCart(User.Identity.GetUserId());
                ShowToCart();
                GetCart().Total_Money(User.Identity.GetUserId());
                GetCart().Total_Quantity_in_Cart(User.Identity.GetUserId(), GetCart().Items.Count());
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult Checkout()
        {
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public ActionResult Checkout_view(FormCollection form)
        {
            string madh = ranDom(25);
            add_donhang(madh,form["phone_number"], form["first_name"], form["email_address"], form["street_address"], form["Tong_gia"]);
            Cart cart = Session["cart"] as Cart;

            foreach (var item in cart.Items)
            {
                var orderDetail = new CHITIETDH();
                orderDetail.MADH = madh;
                orderDetail.MASP = item._shopping_product.MASP;
                orderDetail.SOLUONG = item._shopping_quantity;
                orderDetail.GIA = item._shopping_quantity * item._shopping_product.GIA.Value;
                orderDetail.SIZE = item._shopping_size;
                db.CHITIETDHs.Add(orderDetail);

                string SlSize = item._shopping_size;
                var chitietsp = db.CHITIETSPs.FirstOrDefault(c => c.MASANPHAM == item._shopping_product.MASP);

                chitietsp.SL_TONG = chitietsp.SL_TONG.Value - item._shopping_quantity;
                if (SlSize.Equals("36"))
                    chitietsp.SL_SIZE36 = chitietsp.SL_SIZE36.Value - item._shopping_quantity;
                else if (SlSize.Equals("37"))
                    chitietsp.SL_SIZE37 = chitietsp.SL_SIZE37.Value - item._shopping_quantity;
                else if (SlSize.Equals("38"))
                    chitietsp.SL_SIZE38 = chitietsp.SL_SIZE38.Value - item._shopping_quantity;
                else if (SlSize.Equals("39"))
                    chitietsp.SL_SIZE39 = chitietsp.SL_SIZE39.Value - item._shopping_quantity;
                else if (SlSize.Equals("40"))
                    chitietsp.SL_SIZE40 = chitietsp.SL_SIZE40.Value - item._shopping_quantity;
                else if (SlSize.Equals("41"))
                    chitietsp.SL_SIZE41 = chitietsp.SL_SIZE41.Value - item._shopping_quantity;
                else if (SlSize.Equals("42"))
                    chitietsp.SL_SIZE42 = chitietsp.SL_SIZE42.Value - item._shopping_quantity;
                else if (SlSize.Equals("43"))
                    chitietsp.SL_SIZE43 = chitietsp.SL_SIZE43.Value - item._shopping_quantity;

                db.SaveChanges();
            }
            cart.ClearCart(User.Identity.GetUserId());
            ShowToCart();
            GetCart().Total_Money(User.Identity.GetUserId());
            GetCart().Total_Quantity_in_Cart(User.Identity.GetUserId(), GetCart().Items.Count());
            return RedirectToAction("Index2","SANPHAMs");
        }

        public void add_donhang(string madh, string sdt, string hovaten, string email, string diachi, string tongtien)
        {
            var order = new DONHANG();
            if (User.Identity.IsAuthenticated)
            {
                order.TKKH = User.Identity.GetUserId();
                order.MADH = madh;
                order.NGAYLAPDH = DateTime.Now;
                order.SDT = sdt;
                order.HOVATEN = hovaten;
                order.EMAIL = email;
                order.DIACHI = diachi;
                order.TONGTIEN = int.Parse(tongtien);
                order.TRANGTHAI = "Chờ duyệt";
            }
            else
            {
                order.MADH = madh;
                order.NGAYLAPDH = DateTime.Now;
                order.SDT = sdt;
                order.HOVATEN = hovaten;
                order.EMAIL = email;
                order.DIACHI = diachi;
                order.TONGTIEN = int.Parse(tongtien);
                order.TRANGTHAI = "Chờ duyệt";
            }

            db.DONHANGs.Add(order);
            db.SaveChanges();
            GetCart();
        }
    }
}