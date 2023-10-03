using BlogVarellaMotos.DAL;
using BlogVarellaMotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using BlogVarellaMotos.Framework;
using System.IO;

namespace BlogVarellaMotos.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        
        public int _take = 15;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BlogPosts(int? pagina)
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
            // context.BlogPost.OrderBy(x => x.DateCreated).Skip(page * _take).Take(_take).ToList()
            return PartialView(blogPosts.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult CreateBlogPost()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateBlogPost(BlogPostViewModel model)
        {
            string userId = string.Empty;
            string nomeUser = string.Empty;

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                using (BlogVarellaMotosEntities context = new BlogVarellaMotosEntities())
                {
                    //verifica se já existe algum artigo com esse titulo e só prossegue caso não exista
                    if(context.BlogPost.Any(x => x.Title.ToUpper().Equals(model.Title.ToUpper())))
                    {
                        ModelState.AddModelError("Title", "Já existe um artigo com esse título cadastrado.");
                        return View(model);
                    }
                    model.ImagePath = UploadImage(model.UploadImage);
                    userId = User.Identity.GetUserId();
                    var user = context.AspNetUsers.Find(userId);
                    nomeUser = string.IsNullOrEmpty(user.Name) ? user.UserName : user.Name;
                    BlogPost blogPost = new BlogPost();
                    blogPost.Create(model, user.Id);
                    context.BlogPost.Add(blogPost);
                    context.SaveChanges();                    
                };
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                DeleteUploadImage(model.ImagePath);
                return View();
            }
        }


        public ActionResult EditBlogPost(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            try
            {
                BlogPostViewModel blogPostViewModel = GetBlogPostViewModel(id);
                if (blogPostViewModel != null)
                {
                    return View(blogPostViewModel);
                }
            }
            catch (Exception ex)
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditBlogPost(BlogPostViewModel model)
        {
            string userId = string.Empty;
            string nomeUser = string.Empty;

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                using (BlogVarellaMotosEntities context = new BlogVarellaMotosEntities())
                {
                    bool deleteOldImage = false;
                    string oldImageName = "";
                    

                    
                    //localiza o artigo om o id informado
                    BlogPost blogPost = context.BlogPost.Find(model.Id);

                    if (blogPost != null)
                    {
                        // se o cliente mudou o titulo, valida se novo titulo não existe
                        if (blogPost.Title != model.Title && context.BlogPost.Any(x => x.Title.ToUpper().Equals(model.Title.ToUpper())))
                        {
                            ModelState.AddModelError("Title", "Já existe um artigo com esse título cadastrado.");
                            return View(model);
                        }

                        if (model.UploadImage != null)
                        {
                            model.ImagePath = UploadImage(model.UploadImage);
                            deleteOldImage = true;
                            oldImageName = blogPost.ImagePath;
                        }
                        userId = User.Identity.GetUserId();
                        var user = context.AspNetUsers.Find(userId);
                        nomeUser = string.IsNullOrEmpty(user.Name) ? user.UserName : user.Name;
                        blogPost.Update(model, user.Id);
                        context.SaveChanges();

                        if (deleteOldImage)
                            DeleteUploadImage(oldImageName);
                    }




                };

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ConfirmDeleteBlogPost(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            try
            {
                BlogPostViewModel blogPost = GetBlogPostViewModel(id);
                return View(blogPost);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

       
        public ActionResult DeleteBlogPost(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            try
            {
                using (BlogVarellaMotosEntities context = new BlogVarellaMotosEntities())
                {
                    BlogPost blogPost = context.BlogPost.Find(id);
                    if (blogPost != null)
                    {
                        string oldImageName = blogPost.ImagePath;
                        context.BlogPost.Remove(blogPost);
                        context.SaveChanges();

                        DeleteUploadImage(oldImageName);
                    }
                };
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private string UploadImage(HttpPostedFileBase image)
        {
            string relativePath = "";
            string imageName = "default.jpg";
            string imagesDir = "/imagens/Upload/";
            if (image != null && image.IsImage())
            {
                imageName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(image.FileName));
                string imagePath = Path.Combine(HttpContext.Server.MapPath("~/" + imagesDir), imageName);
                image.SaveAs(imagePath);
                relativePath = imagesDir + imageName;
            }
            else
            {
                relativePath = imagesDir+imageName;
            }
            return relativePath ;
        }

        private void DeleteUploadImage(string relativePath)
        {
            if(string.IsNullOrEmpty(relativePath))
                return;
            string fullPath = Path.Combine(HttpContext.Server.MapPath("~/"), relativePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        private BlogPostViewModel GetBlogPostViewModel(int id)
        {
            using (BlogVarellaMotosEntities context = new BlogVarellaMotosEntities())
            {
                BlogPost blogPost = context.BlogPost.Find(id);
                return new BlogPostViewModel(blogPost);
            };
        }

    }
}
