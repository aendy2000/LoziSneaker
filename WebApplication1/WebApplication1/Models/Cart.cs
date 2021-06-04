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
        public string _shopping_size { get; set; }

    }
    public class Cart
    {

        CT25Team13Entities db = new CT25Team13Entities();
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Show(SANPHAM _pro, string size, int _quantity, string IDUser)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.MASP == _pro.MASP);
            if (IDUser != null)
            {
                if (item == null)
                {
                    items.Add(new CartItem
                    {
                        _shopping_product = _pro,
                        _shopping_quantity = _quantity,
                        _shopping_size = size
                    });
                }
                else
                {
                    if (item._shopping_size == size)
                    {
                        item._shopping_quantity += _quantity;
                    }
                    else
                    {
                        items.Add(new CartItem
                        {
                            _shopping_product = _pro,
                            _shopping_quantity = _quantity,
                            _shopping_size = size
                        });
                    }
                }
            }
        }

        public void Add(string _IDpro, int _quantity, int gia, string size, string IDUser)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.MASP == _IDpro);
            if (IDUser != null)
            {
                if (item == null)
                {
                    var _pro = db.SANPHAMs.FirstOrDefault(c => c.MASP == _IDpro);
                    var newProductInBag = new CHITIETGIOHANG { MAGIOHG = IDUser, MASP = _IDpro, SOLUONG = _quantity, GIA = gia, Size = size };
                    db.CHITIETGIOHANGs.Add(newProductInBag);
                    db.SaveChanges();

                    items.Add(new CartItem
                    {
                        _shopping_product = _pro,
                        _shopping_quantity = _quantity,
                        _shopping_size = size
                    });
                }
                else
                {
                    if (size == item._shopping_size)
                    {
                        var checkPro = db.CHITIETGIOHANGs.Where(c => (c.MASP == _IDpro) && (c.MAGIOHG == IDUser)).First();
                        item._shopping_quantity += _quantity;
                        checkPro.SOLUONG = checkPro.SOLUONG.Value + _quantity;
                        db.SaveChanges();
                    }
                    else
                    {
                        var _pro = db.SANPHAMs.FirstOrDefault(c => c.MASP == _IDpro);
                        var newProductInBag = new CHITIETGIOHANG { MAGIOHG = IDUser, MASP = _IDpro, SOLUONG = _quantity, GIA = gia, Size = size };
                        db.CHITIETGIOHANGs.Add(newProductInBag);
                        db.SaveChanges();

                        items.Add(new CartItem
                        {
                            _shopping_product = _pro,
                            _shopping_quantity = _quantity,
                            _shopping_size = size
                        });
                    }
                }
            }
            else
            {
                if (item == null)
                {
                    var _pro = db.SANPHAMs.FirstOrDefault(c => c.MASP == _IDpro);
                    items.Add(new CartItem
                    {
                        _shopping_product = _pro,
                        _shopping_quantity = _quantity,
                        _shopping_size = size
                    });
                }
                else
                {
                    if (size == item._shopping_size)
                    {
                        item._shopping_quantity += _quantity;
                    }
                    else
                    {
                        var _pro = db.SANPHAMs.FirstOrDefault(c => c.MASP == _IDpro);
                        items.Add(new CartItem
                        {
                            _shopping_product = _pro,
                            _shopping_quantity = _quantity,
                            _shopping_size = size
                        });
                    }
                }
            }
        }
        public void Update_Quantity_Shopping(string id, int _quantity, string size, string IDUser)
        {
            var item = items.Find(s => (s._shopping_product.MASP == id) && (s._shopping_size == size));

            if (IDUser != null)
            {
                if (item != null)
                {
                    var updateQuantityCTGH = db.CHITIETGIOHANGs.FirstOrDefault(c => (c.Size == size) && (c.MASP == id) && (c.MAGIOHG == IDUser));
                    updateQuantityCTGH.SOLUONG = _quantity;
                    db.SaveChanges();
                    item._shopping_quantity = _quantity;
                }
            }
            else
            {
                if (item != null)
                {
                    item._shopping_quantity = _quantity;
                }
            }
        }
        public double Total_Money(string IDBag)
        {
            var total = items.Sum(s => s._shopping_product.GIA * s._shopping_quantity);
            if (IDBag != null)
            {
                var giohang = db.GIOHANGs.FirstOrDefault(c => c.MAGIOHANG == IDBag);
                giohang.TONGTIEN = total;
                db.SaveChanges();
            }
            return (double)total;
        }
        public void Remove_CartItem(string idPro, string size, string IDUser)
        {
            if (IDUser != null)
            {
                var RemoveProInBag = db.CHITIETGIOHANGs.FirstOrDefault(c => (c.MASP == idPro) && (c.MAGIOHG == IDUser) && (c.Size == size));
                if (RemoveProInBag.MASP != null)
                {
                    db.CHITIETGIOHANGs.Remove(RemoveProInBag);
                    db.SaveChanges();
                }
            }
            else
            {
                items.RemoveAll(s => (s._shopping_product.MASP == idPro) && (s._shopping_size == size));
            }
        }
        //Tong so luong shopping
        public void Total_Quantity_in_Cart(string IdBag, int Quantity)
        {
            if (IdBag != null)
            {
                var giohang = db.GIOHANGs.FirstOrDefault(c => c.MAGIOHANG == IdBag);
                giohang.SOLUONG = Quantity.ToString();
                db.SaveChanges();
            }
        }
        public void ClearCart(string IDDetailsCart)
        {
            if (IDDetailsCart != null)
            {
                var RemoveProInBag = db.CHITIETGIOHANGs.Where(c => c.MAGIOHG == IDDetailsCart).ToList();
                var updateQuantity = db.GIOHANGs.FirstOrDefault(c => c.MAGIOHANG == IDDetailsCart);
                updateQuantity.SOLUONG = "0";
                updateQuantity.TONGTIEN = 0;
                db.CHITIETGIOHANGs.RemoveRange(RemoveProInBag);
                db.SaveChanges();
            }
            items.Clear();
        }
    }
}