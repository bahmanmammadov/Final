using Final_Project.Models.DataContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class GalleryController : Controller
    {
        readonly BarberDbContext db;
        public GalleryController(BarberDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var data = db.Gallerys
                .Where(b => b.DeleteByUserId == null)
                .ToList();
            return View(data);
        }
    }
}
