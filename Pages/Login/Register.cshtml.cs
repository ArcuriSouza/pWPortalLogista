using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages.Register
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly PLDbContext dbContext;
        public RegisterModel(PLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [BindProperty]
        public string txLuc { get; set; }
        [BindProperty]
        public string txLogin { get; set; }
        [BindProperty]
        public string txEmail { get; set; }
        [BindProperty]
        public string txContrato { get; set; }
        [BindProperty]
        public string txNopessoa { get; set; }
        [BindProperty]
        public string txCNPJCPF { get; set; }
        [BindProperty]
        public string txFantasia { get; set; }
        [BindProperty]
        public string txQualif { get; set; }
        [BindProperty]
        public string txPwd { get; set; }
        [BindProperty]
        public string txConfPwd { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            try
            {
                #region Validations

                ModelState.Clear();
                if (txLogin == null)
                {
                    ModelState.AddModelError("", "Usuário não preenchido. Verifique.");
                }
                if (txEmail == null)
                {
                    ModelState.AddModelError("", "E-mail não preenchida. Verifique.");
                }
                if (txCNPJCPF == null)
                {
                    ModelState.AddModelError("", "CNPJ/CPF não preenchido. Verifique.");
                }
                if (txFantasia == null)
                {
                    ModelState.AddModelError("", "Nome Fantasia não preenchido. Verifique.");
                }
                if (txQualif == null)
                {
                    ModelState.AddModelError("", "Qualificação não preenchida. Verifique.");
                }
                if (txPwd == null)
                {
                    ModelState.AddModelError("", "Senha não preenchida. Verifique.");
                }
                else
                {
                    if (!Services.Patterns.ValidatePassword(txPwd))
                    {
                        ModelState.AddModelError("", "Senha deve ter entre 8 e 15 caracteres, letras minúsculas, maiúsculas e números. Verifique.");
                    }
                    else
                    {
                        if (txConfPwd == null)
                        {
                            ModelState.AddModelError("", "Confirmação da Senha não preenchida. Verifique.");
                        }
                        else
                        {
                            if (txConfPwd != txPwd)
                            {
                                ModelState.AddModelError("", "Confirmação da Senha diferente. Verifique.");
                            }
                        }
                    }
                }
                if (!ModelState.IsValid)
                {
                    return;
                }

                // Usuario? test = dbContext.Usuario.AsNoTracking().FirstOrDefault(x => x.Txluc == txLuc);

                // if (test != null)
                // {
                //     ModelState.AddModelError("", "Você já possui cadastro nesse portal. Tente recuperar a senha ou entre em contato com o RH.");
                //     return;
                // }

                
                #endregion

                // Validations OK - Proceed to Grant the Access

                string HashPwd = Services.Patterns.getMD5Hash(txPwd);

                Usuario? newUser = new Usuario
                {
                    // Txluc = txLuc,
                    // Txpassword = HashPwd,
                    // Txemail = txEmail == null ? "" : txEmail.Trim(),
                    // Isadmin = "0",
                    // Isactive = "1",
                    // Dtupdatepwd = DateTime.Now,
                    // Dtlastaccess = DateTime.Now,
                    // Isacceptedterms = "0"
                };

                // dbContext.ColabUser.Add(newColab);

                dbContext.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    //new Claim("Txluc", newUser.Txluc),
                    // new Claim("Name", txFullName),
                    // new Claim("DtLastAccess", newColab.Dtlastaccess.ToString()),
                    // new Claim("DtUpdatePwd", newColab.Dtupdatepwd.ToString())
                };

                claims.Add(new Claim(ClaimTypes.Role, "Normal"));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                Response.Redirect("../Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return;
            }
        }
    }
}
