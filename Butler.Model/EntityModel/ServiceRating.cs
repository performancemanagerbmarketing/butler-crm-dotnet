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
    
    public partial class ServiceRating
    {
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> JobId { get; set; }
        public Nullable<int> NoOfRating { get; set; }
        public string Remarks { get; set; }
        public string JobTitle { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Job Job { get; set; }
    }
}
