//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlogVarellaMotos.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string AuthorId { get; set; }
        public string Body { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string MetaDescription { get; set; }
        public string ImagePath { get; set; }
        public string SlugTitle { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string ModifiedById { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
    }
}
