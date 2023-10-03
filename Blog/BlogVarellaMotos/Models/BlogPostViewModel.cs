using BlogVarellaMotos.DAL;
using BlogVarellaMotos.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace BlogVarellaMotos.Models
{
    public class BlogPostViewModel
    {
        [Key]
        public int Id { get;  set; }
        
        private string title;

        [Required]
        [MaxLength(150)]
        [Display(Name = "Título")]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                this.SlugTitle = Util.GenerateSlug(this.title);
            }

        }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Subtítulo")]
        public string SubTitle { get; set; }

        public readonly string NomeAutor;

        [Required]
        [AllowHtml]
        [Display(Name="Texto")]
        public string Body { get; set; }

        public readonly DateTime DateCreated;

         [Display(Name = "Descrição SEO")]
         [MaxLength(400)]
        public string MetaDescription { get; set; }

         private string imagePath;

         [MaxLength(255)]
         [DataType(DataType.ImageUrl)]
         public string ImagePath { get; set; }

         //[MaxLength(255)]
         //[DataType(DataType.ImageUrl)]
         //public string ImagePath
         //{
         //    get
         //    {
         //        return string.Format("/Jonathan{0}", imagePath);
         //    }
         //    set
         //    {
         //        imagePath = value;
         //    }
         //}

        [NotMapped]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UploadImage { get; set; }
        
        [MaxLength(150)]
        public string SlugTitle { get; private set; }

        public readonly DateTime? DateModified;


        [MaxLength(50)]
        public readonly string ModifiedById;

      
        public BlogPostViewModel()
        {
          
        }
        public BlogPostViewModel(BlogPost blogPost)
        {
            this.Id = blogPost.Id;
            this.ImagePath = blogPost.ImagePath;
            this.MetaDescription = blogPost.MetaDescription;
            this.SubTitle = blogPost.SubTitle;
            this.Title = blogPost.Title;
            this.SlugTitle = blogPost.SlugTitle;
            this.NomeAutor = string.IsNullOrEmpty(blogPost.AspNetUsers.Name) ? blogPost.AspNetUsers.UserName : blogPost.AspNetUsers.Name;
            this.Body = blogPost.Body;
            this.DateCreated = blogPost.DateCreated;
            this.DateModified = blogPost.DateModified;
            this.ModifiedById = blogPost.ModifiedById;           
        }
                
       

    }
}