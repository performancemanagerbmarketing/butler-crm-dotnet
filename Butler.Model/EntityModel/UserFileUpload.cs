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
    
    public partial class UserFileUpload
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<int> UserType { get; set; }
        public string FileUploadUrl { get; set; }
        public Nullable<int> FileType { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
    }
}
