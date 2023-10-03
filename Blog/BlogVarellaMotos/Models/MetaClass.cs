//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace BlogVarellaMotos.Models
//{
//    public partial class MetaBlogPostViewModel
//    {
//        public int Id { get; set; }
        
//        private string title;

//        public string SubTitle { get; set; }

//        public readonly string Author;
//        [DisplayName("Corpo")]
//        public string Body { get; set; }

//        public readonly DateTime DateCreated;

//        public string MetaDescription { get; set; }

//        public string ImagePath { get; set; }

//        public string SlugTitle { get; private set; }

//        public readonly DateTime? DateModified;

//        public readonly string ModifiedBy;
//    }

//    [MetadataType(typeof(MetaBlogPostViewModel))]
//    public partial class BlogPostViewModel { }
//}