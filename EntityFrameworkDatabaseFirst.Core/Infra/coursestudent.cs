//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFramework.Core.Infra
{
    using System;
    using System.Collections.Generic;
    
    public partial class coursestudent
    {
        public int id { get; set; }
        public int courseid { get; set; }
        public int studentid { get; set; }
    
        public virtual course course { get; set; }
        public virtual student student { get; set; }
    }
}
