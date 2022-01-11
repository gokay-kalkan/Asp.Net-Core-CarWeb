using EntityLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using WebCar.UI.Models;

namespace WebCar.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<UserAdmin>userManager,SignInManager<UserAdmin>signInManager,RoleManager<IdentityRole>roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", " Hatalı lütfen bilgilerinizi kontrol ediniz");
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Id", user.Id);
                return RedirectToAction("Index", "Home");
            }
            return View();

         
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserAdmin user = new UserAdmin
            {
                Email = model.Email,
                UserName = model.UserName,
                FullName = model.FullName

            };

            IdentityRole role = new IdentityRole()
            {
                Name = "User"
            };
            await _roleManager.CreateAsync(role);

            var result = await _userManager.CreateAsync(user, model.Password);
            var resultt = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded || resultt.Succeeded)
            {
                TempData["kayitmesaj"] = "Sitemize Başarıyla Kayıt Oldunuz";
                return RedirectToAction("Login");
            }
            else
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
                return View(model);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("Fullname");
            return RedirectToAction("Login");
        }
        public IActionResult PasswordReset()
        {
            //UserAdmin userr = new UserAdmin();
            //ResetPasswordModel resetpassword = new ResetPasswordModel() { Email = userr.Email };
            //var check = _userManager.FindByEmailAsync(userr.Email);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordReset(ResetPasswordModel model)
        {
            UserAdmin user = await _userManager.FindByEmailAsync(model.Email);

                
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string passwordResetLink = Url.Action("UpdatePassword", "Account", new { userId = user.Id, token = resetToken }, HttpContext.Request.Scheme);
                //MailMessage mail = new MailMessage();
                //mail.IsBodyHtml = true;
                //mail.To.Add(user.Email);
                //mail.From = new MailAddress("******@gmail.com", "Şifre Güncelleme", System.Text.Encoding.UTF8);
                //mail.Subject = "Şifre Güncelleme Talebi";
                //mail.Body = $"<a target=\"_blank\" href=\"https://localhost:44389{Url.Action("UpdatePassword", "Account", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Yeni şifre talebi için tıklayınız</a>";
                //mail.IsBodyHtml = true;
                //SmtpClient smp = new SmtpClient();
                //smp.Credentials = new NetworkCredential("gky.klkn@gmail.com", "gkytr1907");
                //smp.Port = 587;
                //smp.Host = "smtp.gmail.com";
                //smp.EnableSsl = true;
                //smp.Send(mail);
                MailHelper.ResetPassword.PasswordResetSendMail(passwordResetLink);
                ViewBag.State = true;
            }
            else
                ViewBag.State = false;

            return View(model);
        }




       [HttpGet]
        public IActionResult UpdatePassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            //UserAdmin userr = new UserAdmin();
            //UpdatePasswordModel resetpassword = new UpdatePasswordModel() { Email = userr.Email };
            //var check = _userManager.FindByEmailAsync(userr.Email);
            //TempData["check"] = check;
            return View();
        }
       [HttpPost]
        public async Task<IActionResult> UpdatePassword([Bind("NewPassword")]ResetPasswordModel model)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();
            UserAdmin user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    TempData["Success"] = "Şifreniz Başarıyla Güncellenmiştir";

                }
            }
            else
            {
                
                ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı");
            }
            //UserAdmin userid = new UserAdmin() { Id = userId };
            
            //UserAdmin user = await _userManager.FindByIdAsync(userId);
            //IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.NewPassword);
            //if (result.Succeeded)
            //{
            //    ViewBag.State = true;
            //    await _userManager.UpdateSecurityStampAsync(user);
            //}
            //else
            //    ViewBag.State = false;
            return View(model);
        }


    }
}
