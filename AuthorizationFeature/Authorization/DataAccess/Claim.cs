//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Authorization.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Claim
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public bool ClaimValue { get; set; }
        public int UserTypeId { get; set; }
    
        public virtual UserType UserType { get; set; }
    }
}
