//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DONHANG
    {
        public string MADH { get; set; }
        public string TKNV { get; set; }
        public string TKKH { get; set; }
        public Nullable<System.DateTime> NGAYLAPDH { get; set; }
        public Nullable<int> TONGTIEN { get; set; }
    
        public virtual CHITIETDH CHITIETDH { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
