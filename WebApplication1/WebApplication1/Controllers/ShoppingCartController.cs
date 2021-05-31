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
        public ActionResult AddtoCart(string id)
        {
            var pro = db.SANPHAMs.SingleOrDefault(s => s.MASP == id);

            if (pro != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Customer"))
                    {
                        CHITIETGIOHANG cHITIETGIOHANG = new CHITIETGIOHANG();
                        cHITIETGIOHANG.MAGIOHG = User.Identity.GetUserId();
                        cHITIETGIOHANG.MASP = id;
                        cHITIETGIOHANG.SOLUONG = 1;
                        cHITIETGIOHANG.GIA = pro.GIA;
                        db.CHITIETGIOHANGs.Add(cHITIETGIOHANG);
                        db.SaveChanges();
                    }
                }
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //Trang gio hang
        public ActionResult ShowToCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Customer"))
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
                                string masp = model[i].MASP.ToString();
                                SANPHAM pro = db.SANPHAMs.SingleOrDefault(s => s.MASP == masp);
                                GetCart().Add(pro);
                            }
                        }
                    }
                    else
                    {
                        GetCart();
                        cart.ClearCart();
                        if (model.Count() != 0)
                        {
                            for (int i = 0; i < model.Count(); i++)
                            {
                                string masp = model[i].MASP.ToString();
                                SANPHAM pro = db.SANPHAMs.SingleOrDefault(s => s.MASP == masp);
                                GetCart().Add(pro);
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
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            Cart cart = Session["Cart"] as Cart;

            if (cart != null)
            {
                ViewBag.QuantityCart = cart.Items.Count();
                return PartialView("BagCart");
            }
            else
            {
                ViewBag.QuantityCart = 0;
            }

            return PartialView("BagCart");
        }
        public ActionResult Clear_Cart()
        {
            Cart cart = Session["Cart"] as Cart;
            GetCart();
            if (cart != null)
            {
                cart.ClearCart();
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
    }
}