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
    
    public partial class GIOHANG
    {
        public string MAGIOHANG { get; set; }
        public string SOLUONG { get; set; }
        public Nullable<int> TONGTIEN { get; set; }
        public string TKKH { get; set; }
    
        public virtual CHITIETGIOHANG CHITIETGIOHANG { get; set; }
    }
}
