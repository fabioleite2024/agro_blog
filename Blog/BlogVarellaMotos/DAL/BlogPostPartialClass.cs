using BlogVarellaMotos.Framework;
using BlogVarellaMotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogVarellaMotos.DAL
{
    public partial class BlogPost
    {       
        public void Create(BlogPostViewModel blogPostViewModel, string authorId)
        {
            this.ImagePath = blogPostViewModel.ImagePath;
            this.MetaDescription = blogPostViewModel.MetaDescription;
            this.SubTitle = blogPostViewModel.SubTitle;
            this.Title = blogPostViewModel.Title;
            this.SlugTitle = blogPostViewModel.SlugTitle;
            this.Body = blogPostViewModel.Body;
            this.DateCreated = DateTime.Now;
            this.AuthorId = authorId;
        }
        public void Update(BlogPostViewModel blogPostViewModel,string modifiedById)
        {
            this.ImagePath = blogPostViewModel.ImagePath;
            this.MetaDescription = blogPostViewModel.MetaDescription;
            this.SubTitle = blogPostViewModel.SubTitle;
            this.Title = blogPostViewModel.Title;
            this.SlugTitle = blogPostViewModel.SlugTitle;
            this.Body = blogPostViewModel.Body;
            this.DateModified = DateTime.Now;
            this.ModifiedById = modifiedById;
        }

    }
}