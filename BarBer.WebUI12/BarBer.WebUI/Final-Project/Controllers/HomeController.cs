using Final_Project.AppCode.Extension;
using Final_Project.Models.DataContext;
using Final_Project.Models.Entity;
using Final_Project.Models.Entity.Membership;
using Final_Project.Models.FormModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        readonly BarberDbContext db;
        readonly Microsoft.AspNetCore.Identity.UserManager<BarberUser> userManager;
        readonly SignInManager<BarberUser> signinManeger;
        readonly IConfiguration configuraton;
        public HomeController(BarberDbContext db, Microsoft.AspNetCore.Identity.UserManager<BarberUser> userManager,
            SignInManager<BarberUser> signinManeger,
            IConfiguration configuraton)
        {
            this.db = db;
            this.userManager = userManager;
            this.signinManeger = signinManeger;
            this.configuraton = configuraton;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Contact(Contact model)
        {

            if (ModelState.IsValid)
            {
                db.Contacts.Add(model);
                db.SaveChanges();
                //ModelState.Clear();
                //ViewBag.message = "Yerine yetirildi";
                return Json(new
                {
                    error = false,
                    message = "Yerine yetirildi"
                });
            }
            return Json(new
            {
                error = true,
                message = "Yeniden sinayin"
            });


        }
        [AllowAnonymous]

        public IActionResult signin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Home");

            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> signin(LoginFormModel user)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Home");

            }

            if (ModelState.IsValid)
            {

                BarberUser founderUser = null;

                if (user.UserName.IsEmail())
                {
                    founderUser = await userManager.FindByEmailAsync(user.UserName); //Eger Isdifadeci email gore giris edibse onu yoxlayir 
                }
                else
                {
                    founderUser = await userManager.FindByNameAsync(user.UserName); //YOX EGER USERNAME GORE GIRIBSE ONU AXTARIS EDIR.

                }

                if (founderUser == null) //Eger login ola bilmirse gonderir view gonderir yeni isdifadeci tapilmiyanda
                {
                    ViewBag.Ms = "Isdifadeci sifresi ve parol sefdir ";
                    return View(user);

                }

                if (founderUser.EmailConfirmed == false)
                {
                    ViewBag.Ms = "Zehmet olmasa Emailinizi testiq edin....";
                    return View(user);
                }

                //Eger database isdifadeci user deyilse onda gire bilmez.

                if (!await userManager.IsInRoleAsync(founderUser, "User"))
                {
                    ViewBag.Ms = "Isdifadeci sifresi ve parol sefdir ";
                    return View(user);
                }




              
                    var sRuselt = await signinManeger.PasswordSignInAsync(founderUser, user.Password, true, true); //Burda giwi edirik.

                    if (sRuselt.Succeeded != true) // Eger giriw zamani ugurlu deyilse yeni gire bilmirse 
                    {
                        ViewBag.Ms = "Isdifadeci sifresi ve parol sefdir ";
                        return View(user);

                    }
                    var redirectUrl = Request.Query["ReturnUrl"];

                    if (!string.IsNullOrWhiteSpace(redirectUrl))
                    {
                        return Redirect(redirectUrl);
                    }

                    return RedirectToAction("Index", "Home");

                
            }

            ViewBag.Ms = "Melumatlari doldur gagas";
            return View(user);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel register)
        {
            //Eger giris edibse routda myaccount/sing yazanda o seyfe acilmasin homa tulaasin
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Home");

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            //Yeni user yaradiriq.
            BarberUser user = new BarberUser
            {

                UserName = register.UserName,
                Email = register.Email,
                //EmailConfirmed=true
            };



            string token = $"subscribetoken-{register.UserName}-{DateTime.Now:yyyyMMddHHmmss}"; // token yeni id goturuk

            token = token.Encrypt("");

            string path = $"{Request.Scheme}://{Request.Host}/subscribe-confirmm?token={token}"; // path duzeldirik



            var mailSended = configuraton.SendEmail(user.Email, "BeluqaTahir", $"Zehmet olmasa <a href={path}>Link</a> vasitesile abuneliyi tamamlayin");


            var person = await userManager.FindByNameAsync(user.UserName);


            if (person == null)
            {
                //Burda biz userManager vasitesile user ve RegistirVM passwword yoxluyuruq.(yaradiriq)
                var identityRuselt = await userManager.CreateAsync(user, register.Password);


                //Startupda yazdigimiz qanunlara uymursa Configure<IdentityOptions> onda error qaytariq summary ile.;
                if (!identityRuselt.Succeeded)
                {
                    foreach (var error in identityRuselt.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                //Yratdigimiz user ilk yarananda user rolu verik.

                await userManager.AddToRoleAsync(user, "User");

                return RedirectToAction("Signin", "Home");

            }


            if (person.UserName != null)
            {
                ViewBag.ms = "Bu username evvelceden qeydiyyatdan kecib";

                return View(register);
            }
            return null;
        }


        public async Task<IActionResult> logout()
        {
            await signinManeger.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult Accessdenied()
        {
            return View();
        }

        
        [AllowAnonymous]
        [HttpGet]
        [Route("subscribe-confirmm")]
        public IActionResult SubscibeConfirm(string token)
        {


            token = token.Decrypte("");

            Match match = Regex.Match(token, @"subscribetoken-(?<id>[a-zA-Z0-9]*)(.*)-(?<timeStampt>\d{14})");

            if (match.Success)
            {
                string id = match.Groups["id"].Value;
                string executeTimeStamp = match.Groups["executeTimeStamp"].Value;

                var subsc = db.Users.FirstOrDefault(s => s.UserName == id);

                if (subsc == null)
                {
                    ViewBag.ms = Tuple.Create(true, "Token xetasi");
                    goto end;
                }
                if (subsc.EmailConfirmed == true)
                {
                    ViewBag.ms = Tuple.Create(true, "Artiq tesdiq edildi");
                    goto end;
                }
                subsc.EmailConfirmed = true;
                db.SaveChanges();

                ViewBag.ms = Tuple.Create(false, "Abuneliyiniz tesdiq edildi");

            }
            end:
            return View();
        }




        [HttpPost]
        //[Route("/Subscrice.html")]

        public IActionResult Subscrices([Bind("EMail")] Subscribe model)
        {

            if (ModelState.IsValid)
            {
                var current = db.subscribes.FirstOrDefault(s => s.EMail.Equals(model.EMail));
                if (current != null && current.EMailConfirm == true)
                {
                    return Json(new
                    {

                        error = true,
                        message = "Bu E-Mail evvelceden qeydiyyati edilib"

                    });
                }
                else if (current != null && (current.EMailConfirm ?? false == false))
                {
                    return Json(new
                    {

                        error = true,
                        message = "Bu E-Mail evvelceden qeydiyyati edilib "

                    });
                }


                db.subscribes.Add(model);
                db.SaveChanges();


                string token = $"subscribetoken-{model.ID}-{DateTime.Now:yyyyMMddHHmmss}"; // token yeni id goturuk

                token = token.Encrypt("");

                string path = $"{Request.Scheme}://{Request.Host}/subscribe-confirm?token={token}"; // path duzeldirik



                var mailSended = configuraton.SendEmail(model.EMail, "Barber", $"Siz yeni melumatlardan xederdar olacaqsiniz");
                if (mailSended == false)
                {
                    db.Database.RollbackTransaction();

                    return Json(new
                    {
                        error = false,
                        message = "Email-gonderilmasi zamini xeta bas verdi!"

                    });
                }

                return Json(new
                {

                    error = false,
                    message = "Sorgunuz ugurla qeyde alindi!!"

                });
            }



            return Json(new
            {

                error = true,
                message = "Sorgunuzun Icrasi zamani xeta yarandi,Zehmet olmasa yeniden yoxlayin"

            });
        }
    }
}
