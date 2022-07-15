using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages.Login
{
    
    public class NewPwdModel : PageModel
    {
        private readonly PLDbContext dbContext;
        public NewPwdModel(PLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public string? NewPassword { get; set; }
        [BindProperty]
        public string? NewPasswordConfirm { get; set; }
        public string? hashkey { get; set; }

        public void OnGet(string key)
        {
            // if(key == null)
            // {
            //     HttpContext.Session.SetString("OutputMessage", "Não possui chave de acesso. Favor verificar!");
            //     Response.Redirect("./ResetInfo");
            //     return;
            // }
            Usuario? user = dbContext.Usuario
            .Where(x => x.Hashkey == key)
            .FirstOrDefault();
            hashkey = key;
            // if (user == null)
            // {
            //     HttpContext.Session.SetString("OutputMessage", "Chave expirada. Use a recuperação de senha novamente!");
            //     Response.Redirect("./ResetInfo");
            //     return;
            // }
            if (user.Dtvalidhash < DateTime.Now)
            {
                user.Hashkey = null;
                user.Dtvalidhash = null;
                dbContext.Attach(user);
                dbContext.SaveChangesAsync();

                Global.outPutMessage = "Chave expirada. Use a recuperação de senha novamente!";
                // HttpContext.Session.SetString("OutputMessage", "Chave expirada. Use a recuperação de senha novamente!");
                Response.Redirect("./ResetInfo");
                return;
            }
        }

        public void OnPost(string key)
        {
            try
            {
                #region Validations
                if(key == null || key == ""){
                    ModelState.AddModelError("", "Não possui uma chave de acesso. Verifique.");   
                }
                ModelState.Clear();
                if (NewPassword == null)
                {
                    ModelState.AddModelError("", "Nova Senha não preenchida. Verifique.");
                }
                if (NewPasswordConfirm == null)
                {
                    ModelState.AddModelError("", "Confirmação da Nova Senha não preenchida. Verifique.");
                }
                else
                {
                    if (!Services.Patterns.ValidatePassword(NewPassword))
                    {
                        ModelState.AddModelError("", "Senha deve ter entre 8 e 15 caracteres, letras minúsculas, maiúsculas e números. Verifique.");
                    }
                    else
                    {
                        if (NewPasswordConfirm != NewPassword)
                        {
                            ModelState.AddModelError("", "Confirmação da Senha diferente. Verifique.");
                        }
                    }
                }
                if (!ModelState.IsValid)
                {
                    return;
                }

                #endregion

                // Validations OK - Proceed to Update Password

                //decimal id = Convert.ToDecimal(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                string HashPwd = Services.Patterns.getMD5Hash(NewPassword);

                Usuario? user = dbContext.Usuario
                .Where(x => x.Hashkey == key)
                .FirstOrDefault();

                if (user == null)
                {
                    ModelState.AddModelError("", "Chave expirada. Use a recuperação de senha novamente!");
                    return;
                }
                if (user.Dtvalidhash < DateTime.Now)
                {
                    ModelState.AddModelError("", "Chave expirada. Use a recuperação de senha novamente!");
                    return;
                }

                string oldPwd = user.Txpwd;
                user.Txpwd = HashPwd;
                user.Hashkey = null;
                user.Dtvalidhash = null;
                dbContext.Attach(user);
                dbContext.SaveChanges();

                Global.outPutMessage = "Senha alterada com sucesso!";

                Response.Redirect("./ResetInfo");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return;
            }
        }
    }
}
