using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using pWPortalLogista.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System;
using System.Collections.Generic;

namespace pWPortalLogista.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly PLDbContext dbContext;
        public LoginModel(PLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public string Login { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void OnPost(string? returnUrl = null)
        {
            try
            {

                if (Login == null || Password == null)
                {
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    ModelState.AddModelError("", "Acesso inválido. Verifique.");
                    return;
                }

                string HashPwd = Services.Patterns.getMD5Hash(Password);

                Usuario? user;
                user = dbContext.Usuario.FirstOrDefault(x => x.Txemail.ToLower().Trim() == Login.ToLower().Trim() && x.Txpwd == HashPwd);                

                if (user == null || user.Isactive == false)
                {
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    ModelState.AddModelError("", "Acesso inválido! Verifique.");
                    return;
                }

                var claims = new List<Claim>
                {
                    new Claim("Id", user.Iduser.ToString()),
                    new Claim("Name", user.Txnopessoa),
                    new Claim("DtLastAccess", user.Dtlastaccess.ToString())
                    //new Claim("DtUpdatePwd", user.Dtupdatepwd.ToString()),
                    //new Claim("AccessIP", user.Accessip == null ? "" : user.Accessip.ToString())
                };

                if (user.Isadmin == true)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Normal"));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                DateTime? logDtLastAccess = user.Dtlastaccess;
                user.Dtlastaccess = DateTime.Now;

                // string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
                // var externalIp = IPAddress.Parse(externalIpString);

                // string oldIp = user.Accessip;
                // user.Accessip = externalIp.ToString();

                dbContext.Entry(user);
                dbContext.Attach(user).State = EntityState.Modified;
                dbContext.SaveChanges();

                // // Log Pattern Service
                // Services.Patterns pat = new Services.Patterns(this.dbContext);
                // pat.Log("U", "USER", "DTLASTACCESS", user.Id.ToString(), Convert.ToDecimal(user.Id), logDtLastAccess == null ? "" : DateTime.Parse(logDtLastAccess.ToString()).ToString("MM/dd/yyyy HH:mm:ss"), DateTime.Parse(user.Dtlastaccess.ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                // pat.Log("U", "USER", "ACCESSIP", user.Id.ToString(), Convert.ToDecimal(user.Id), oldIp, user.Accessip);

                if (Url.IsLocalUrl(returnUrl))
                {
                    Response.Redirect(returnUrl);
                }
                else
                {   
                    if(user.Isadmin == true){
                        Response.Redirect("../Files/AllFiles");
                    }else{
                        Response.Redirect("../Files/CheckFiles");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return;
            }

        }
    }
}
