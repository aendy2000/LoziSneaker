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
    
    public partial class TTCANHAN
    {
        public string ID { get; set; }
        public string TENTK { get; set; }
        public string TEN { get; set; }
        public Nullable<System.DateTime> NGAYSINH { get; set; }
        public string DIACHI { get; set; }
        public string GIOITINH { get; set; }
        public string SDT { get; set; }
        public string MAIL { get; set; }
    
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
