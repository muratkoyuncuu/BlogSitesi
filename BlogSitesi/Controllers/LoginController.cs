﻿using BlogSitesi.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Security.Claims;

namespace BlogSitesi.Controllers
{
    
    public class LoginController : Controller
       
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email,string password)
        {
            try
            {
                var kullanici = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password && x.IsActive);
                if (kullanici == null) TempData["Mesaj"] = "Giriş Başarısız";
                else
                    {
                    var haklar = new List<Claim>() { new Claim(ClaimTypes.Email,kullanici.Email)};
                    var kullaniciKimligi = new ClaimsIdentity(haklar, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(kullaniciKimligi);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    }
                if (kullanici.IsAdmin)
                {
                    return Redirect("/Admin");
                }
                else return Redirect("/Home");
                
            }
            catch (Exception hata)
            {
                //Todo : hata loglanacak
                TempData["Mesaj"] = "Hata oluştu";

            }
            return View();
        }
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Login");
        }
    }
}
