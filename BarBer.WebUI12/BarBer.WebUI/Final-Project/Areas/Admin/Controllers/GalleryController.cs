using Final_Project.Models.DataContext;
using Final_Project.Models.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        readonly BarberDbContext db;
        readonly IWebHostEnvironment env;

        public GalleryController(BarberDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Gallerys
                .Where(b => b.DeleteByUserId == null)
                .OrderByDescending(b => b.ID)
                .ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.Gallerys
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gallery galery, IFormFile file)
        {
            if (file == null)
            {
                ModelState.AddModelError("file", "File secilmeyib");
            }
            if (ModelState.IsValid)
            {

                string extension = Path.GetExtension(file.FileName);
                galery.ImagePath = $"{Guid.NewGuid()}{extension}";
                string pyschialFileName = Path.Combine(env.ContentRootPath,
                    "wwwroot",
                    "assets",
                    "img",
                    "gallery",
                     galery.ImagePath);
                using (var stream = new FileStream(pyschialFileName, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }

                db.Add(galery);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(galery);
        }
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var galery = await db.Gallerys.FindAsync(id);
            if (galery == null)
            {
                return NotFound();
            }
            return View(galery);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gallery galery, IFormFile file, string fileTemp)
        {
            if (string.IsNullOrEmpty(fileTemp) && file == null)
            {
                ModelState.AddModelError("file", "Shekil secilmeyib");
            }

            if (id != galery.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = await db.Gallerys.FirstOrDefaultAsync(b => b.ID == id && b.DeleteByUserId == null);
                    
                    if (file != null)
                    {
                        string extension = Path.GetExtension(file.FileName);
                        galery.ImagePath = $"{Guid.NewGuid()}{extension}";
                        string pyschialFileName = Path.Combine(env.ContentRootPath,
                            "wwwroot",
                            "assets",
                            "img",
                            "gallery",
                             galery.ImagePath);
                        using (var stream = new FileStream(pyschialFileName, FileMode.Create, FileAccess.Write))
                        {
                            await file.CopyToAsync(stream);
                        }
                        if (!string.IsNullOrWhiteSpace(entity.ImagePath))
                        {
                            System.IO.File.Delete(Path.Combine(env.ContentRootPath,
                                "wwwroot",
                                "assets",
                                "img",
                                "gallery",
                                entity.ImagePath));
                        }
                        entity.ImagePath = galery.ImagePath;
                    }
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(galery);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galery = await db.Gallerys
                .FirstOrDefaultAsync(m => m.ID == id);
            if (galery == null)
            {
                return NotFound();
            }

            return View(galery);
        }

        // POST: Admin/BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galery = await db.Gallerys.FindAsync(id);
            db.Gallerys.Remove(galery);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BlogPostExists(int id)
        {
            return db.BlogPosts.Any(e => e.ID == id);
        }
    }
}
