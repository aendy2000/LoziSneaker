using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Models
{
    public class CartItem
    {
        public SANPHAM _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }
    }
    public class Cart
    {
        CT25Team13Entities db = new CT25Team13Entities();
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(SANPHAM _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.MASP == _pro.MASP);
            if(item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });
            }
            else
            {
                item._shopping_quantity += _quantity;
            }
        }
        public void Update_Quantity_Shopping(string id, int _quantity)
        {
            var item = items.Find(s => s._shopping_product.MASP == id);
            CHITIETGIOHANG sanphamtronggiohang = db.CHITIETGIOHANGs.FirstOrDefault(c => c.MASP == id);
            
            if(item != null)
            {
                if(sanphamtronggiohang.MASP != null)
                {
                    sanphamtronggiohang.SOLUONG = _quantity;
                    item._shopping_quantity = _quantity;
                    db.Entry(sanphamtronggiohang).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        public double Total_Money()
        {
            var total = items.Sum(s => s._shopping_product.GIA * s._shopping_quantity);
            return (double)total;
        }
        public void Remove_CartItem(string id)
        {
            items.RemoveAll(s => s._shopping_product.MASP == id);
        }
        //Tong so luong shopping
        public int Total_Quantity_in_Cart()
        {
            return items.Sum(s => s._shopping_quantity);
        }
        public void ClearCart()
        {
            items.Clear(); //Xoa gio hang de thuoc hien order
        }
    }
}