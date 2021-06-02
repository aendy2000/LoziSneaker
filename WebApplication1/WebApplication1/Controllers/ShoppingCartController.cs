using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ShoppingCartController : Controller
    {
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
            cart.Remove_CartItem(id_pro, size, User.Identity.GetUserId());
            cart.Total_Money(User.Identity.GetUserId());
            cart.Total_Quantity_in_Cart(User.Identity.GetUserId(), GetCart().Items.Count());
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
            GetCart();
            if (cart != null)
            {
                cart.ClearCart(User.Identity.GetUserId());
                cart.Total_Money(User.Identity.GetUserId());
                cart.Total_Quantity_in_Cart(User.Identity.GetUserId(), GetCart().Items.Count());
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
    }
}