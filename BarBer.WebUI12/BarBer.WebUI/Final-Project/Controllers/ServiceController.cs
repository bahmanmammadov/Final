using Final_Project.Models.DataContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class ServiceController : Controller
    {
        readonly BarberDbContext db;
        public ServiceController(BarberDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var data = db.Services
               .Where(b => b.DeleteByUserId == null)
               .ToList();
            return View(data);
        }
    }
}
