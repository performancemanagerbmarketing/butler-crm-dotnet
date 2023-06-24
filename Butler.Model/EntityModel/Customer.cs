//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Butler.Model.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public string ProfileImageUrl { get; set; }
        public string VerificationImageUrl { get; set; }
        public string CNICFromtImageUrl { get; set; }
        public string CNICBackImageUrl { get; set; }
        public bool VerificationStatus { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public bool IsAdded { get; set; }
    }
}
