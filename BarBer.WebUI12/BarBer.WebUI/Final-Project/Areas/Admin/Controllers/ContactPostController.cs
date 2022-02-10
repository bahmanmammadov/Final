using Final_Project.AppCode.Extension;
using Final_Project.Models.DataContext;
using Final_Project.Models.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactPostController : Controller
    {
        readonly BarberDbContext db;
        readonly IWebHostEnvironment env;
        readonly IConfiguration configuration;


        public ContactPostController(BarberDbContext db, IWebHostEnvironment env, IConfiguration configuration)
        {
            this.db = db;
            this.env = env;
            this.configuration = configuration;
        }
        public async Task<IActionResult> Index(int typeId)
        {
            var query = db.Contacts.AsQueryable()
                .Where(cp => cp.DeleteByUserId == null);


            ViewBag.query = query.Count();
            ViewBag.Count = query.Where(cp => cp.AnswerdByUserId == null).Count();
            ViewBag.Count1 = query.Where(cp => cp.AnswerdByUserId != null).Count();

            switch (typeId)
            {
                case 1:
                    query = query.Where(cp => cp.AnswerdByUserId == null);
                    break;
                case 2:
                    query = query.Where(cp => cp.AnswerdByUserId != null);
                    break;
                default:
                    break;
            }
            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPost = await db.Contacts
                .FirstOrDefaultAsync(m => m.ID == id
                && m.DeleteByUserId == null
                && m.AnswerdByUserId == null);

            if (contactPost == null)
            {
                return NotFound();
            }

            return View(contactPost);
        }
        [HttpPost]

        public async Task<IActionResult> Answer([Bind("ID", "Answer")] Contact model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var contactPost = await db.Contacts
                .FirstOrDefaultAsync(m => m.ID == model.ID
                && m.DeleteByUserId == null
                && m.AnswerdByUserId == null);

            if (contactPost == null)
            {
                return NotFound();
            }

            contactPost.Answer = model.Answer;
            contactPost.AnswerDate = DateTime.Now;
            contactPost.AnswerdByUserId = 1;

            string token = $"subscribetoken-{model.ID}-{DateTime.Now:yyyyMMddHHmmss}"; // token yeni id goturuk

            token = token.Encrypt("");

            string path = $"{Request.Scheme}://{Request.Host}/subscribe-confirm?token={token}"; // path duzeldirik



            var mailSended = configuration.SendEmail(contactPost.Email, "BarberProject", $"Sorguvuza cvb olara  `{model.Answer}` ");


            await db.SaveChangesAsync();
            return Redirect(nameof(Index));
        }

    }
}

