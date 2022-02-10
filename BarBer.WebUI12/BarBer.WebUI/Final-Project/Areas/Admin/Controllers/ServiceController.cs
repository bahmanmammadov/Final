using Final_Project.Models.DataContext;
using Final_Project.Models.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        readonly BarberDbContext db;
        readonly IWebHostEnvironment env;

        public ServiceController(BarberDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Services
               .Where(b =>  b.DeleteByUserId == null)
               .ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await db.Services
                .FirstOrDefaultAsync(m => m.ID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            if (service != null)
            {
                Service serv = new Service();
                serv.Xidmetler = service.Xidmetler;
                serv.Qiymet = service.Qiymet;
                db.Services.Add(serv);
                await db.SaveChangesAsync();
                return View(serv);
            }
            return null;
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await db.Services.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,  Service category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    db.Update(category);
                    await db.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serv = await db.Services
                .FirstOrDefaultAsync(m => m.ID == id);
            if (serv == null)
            {
                return NotFound();
            }

            return View(serv);
        }

        // POST: Admin/BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serv = await db.Services.FindAsync(id);
            db.Services.Remove(serv);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
