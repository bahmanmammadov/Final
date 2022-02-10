using Final_Project.AppCode.Extension;
using Final_Project.Models.DataContext;
using Final_Project.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        readonly BarberDbContext db;
        readonly IConfiguration configuraton;
        public BlogController(BarberDbContext db, IConfiguration configuraton)
        {
            this.db = db;
            this.configuraton = configuraton;
        }
        public IActionResult Index()
        {

            var data = db.BlogPosts
                .Where(b => b.PublishDate == null && b.DeleteByUserId == null)
                .ToList();
            return View(data);
        }
        public IActionResult Details(int? id)
        {

             var data = db.BlogPosts
                    .Include(m => m.CreatedByUser)
                    .Include(m => m.Comments)
                    .ThenInclude(m => m.CreatedByUser)
                     .Include(m => m.Comments)
                    .ThenInclude(m => m.Children)
                    .FirstOrDefault(m => m.ID == id);

            //.ToList();
            return View(data);


        }

        [HttpPost]
        public async Task<IActionResult> PostComment(int? commentId, int postId, string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                return Json(new
                {
                    error = true,
                    message = "Serh bos buraxila bilmez!"
                });
            }

            if (postId < 1)
            {
                return Json(new
                {
                    error = true,
                    message = "Post movcud deyil!"
                });
            }


            var post = await db.BlogPosts.FirstOrDefaultAsync(c => c.ID == postId);


            if (post == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Post movcud deyil!"
                });
            }

            var commentModel = new BlogPostComment
            {
                ParentId = commentId,
                BlogPostId = postId,
                Comment = comment
                //,CreatedByUserId= User.GetCurrentUserId()
            };
            if (commentId.HasValue && await db.BlogPostComments.AnyAsync(c => c.ID == commentId))
                commentModel.ParentId = commentId;

            await db.BlogPostComments.AddAsync(commentModel);
            await db.SaveChangesAsync();



            //return Json(new
            //{
            //    error = false,
            //    message = "Elave edildi!"
            //});
            commentModel = await db.BlogPostComments
                .Include(c => c.CreatedByUserId)
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(c => c.ID == commentModel.ID);

            return PartialView("_Comment", commentModel);
        }




        









    }
}
