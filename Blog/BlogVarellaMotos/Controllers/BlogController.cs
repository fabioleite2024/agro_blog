using BlogVarellaMotos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BlogVarellaMotos.Models;

namespace BlogVarellaMotos.Controllers
{
    public class BlogController : Controller
    {
        public static List<BlogPost> BlogPosts;
                
        // GET: BlogPost
        public ActionResult Index(int? pagina)
        {

            BlogVarellaMotosEntities context = new BlogVarellaMotosEntities();
            
            List<BlogPostViewModel> blogPosts = new List<BlogPostViewModel>();

                
            int paginaTamanho = 15;
            int paginaNumero = (pagina ?? 1);

            foreach (var item in context.BlogPost.OrderByDescending(x => x.Id).ToList())
            {
                BlogPostViewModel blogPost = new BlogPostViewModel(item);
                blogPosts.Add(blogPost);
            }
          
            return PartialView(blogPosts.ToPagedList(paginaNumero, paginaTamanho));

          
        }
             
        public ActionResult Artigo(string slugTitle)
        {
            BlogVarellaMotosEntities context = new BlogVarellaMotosEntities();           
            BlogPost artigo = context.BlogPost.FirstOrDefault(x => x.SlugTitle == slugTitle);           

            if (artigo == null)
            {
               return RedirectToAction("Index");            
            }

            BlogPostViewModel blogPostViewModel = new BlogPostViewModel(artigo);
            return View(blogPostViewModel);
        }            

     
    }
}
