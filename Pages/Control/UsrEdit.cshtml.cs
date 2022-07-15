using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages.Control
{
    [Authorize(Roles = "Administrator")]
    public class UsrEditModel : PageModel
    {
        private PLDbContext dbContext;

        public UsrEditModel(PLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [FromRoute]
        public decimal? id { get; set; }
        [BindProperty]
        public string lucTxt { get; set; }
        [BindProperty]
        public string nomeFantasia { get; set; }
        [BindProperty]
        public string userEmail { get; set; }
        [BindProperty]
        public string userPwd { get; set; }
        //[BindProperty]
        //public bool userIsAdmin { get; set; }
        //[BindProperty]
        //public bool userIsHR { get; set; }
        [BindProperty]
        public string userName { get; set; }
        [BindProperty]
        public decimal optType { get; set; }
        [BindProperty]
        public decimal optDoc { get; set; }
        [BindProperty]
        public decimal optActive { get; set; }
        [BindProperty]
        public string inputTest { get; set; }
        [BindProperty]
        public List<int?> lucForm { get; set; }
        [BindProperty]
        public List<int?> SelectedView { get; set; }
        [BindProperty]
        public List<int?> NonSelectedView { get; set; }
        public UserLuc lstUserLuc { get; set; }
        public List<UserLuc> lstUserLucGet { get; set; }
        public List<UserLuc> lstUserLucGetAllow { get; set; }
        public List<UserLuc> lstUserLucGetDeny { get; set; }
        public Usuario lstUser { get; set; }
        public Usuario lstUserEmail { get; set; }
        public List<Luc> lstLuc { get; set; }

        public List<string> lstData { get; set; }
        public bool IsNew
        {
            get { return lstUser == null; }
        }
        
        public void LoadData(){
            lstUserLucGet = dbContext.UserLuc
                .Include(x => x.IduserNavigation)
                .Include(x => x.IdlucNavigation)
                .Where(x => x.Iduser == id)
                .ToList();

            lstUserLucGetAllow = lstUserLucGet.Where(x => x.Isallow == true).ToList();

            lstUserLucGetDeny = lstUserLucGet.Where(x => x.Isallow == false).ToList();
        }

        public void OnGet()
        {

            LoadData();
            

            List<int?> lucFormId = new List<int?>();

            foreach(var item in lstUserLucGet){
                lucFormId.Add(item.Idluc);
            }

            lstUser = dbContext.Usuario
                .Where(x => x.Iduser == id)
                .FirstOrDefault();

            lstLuc = dbContext.Luc.AsNoTracking().ToList();

            if (lstUser == null)
            {
                nomeFantasia = "";
                userName = "";
                userEmail = "";
                optType = 0;
                optDoc = 0;
                optActive = 0;
            }
            else
            {
                List<int?> lstLucByte = new List<int?>();

                foreach(var item in lucFormId){
                    lstLucByte.Add(item);
                }

                lucForm = lstLucByte;
                nomeFantasia = lstUser.Txfantasia == null ? "" : lstUser.Txfantasia;
                userName = lstUser.Txnopessoa == null ? "" : lstUser.Txnopessoa;
                userEmail = lstUser.Txemail == null ? "" : lstUser.Txemail;
                optType = lstUser.Isadmin == true ? 1 : 0;
                // optDoc = lstUserLuc.Isallow == true ? 0 : 1;
                optActive = lstUser.Isactive == true ? 0 : 1;
            }
            userPwd = "";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                LoadData();
                if(userEmail == null){
                    ModelState.AddModelError("", "E-mail não preenchido. Verifique!");
                    return Page();
                }

                lstUserLuc = dbContext.UserLuc
                .Include(x => x.IduserNavigation)
                .Include(x => x.IdlucNavigation)
                .Where(x => x.Iduser == id || x.IduserNavigation.Txemail.ToLower() == userEmail.ToLower())
                .FirstOrDefault();

                lstUser = dbContext.Usuario
                .Where(x => x.Iduser == id)
                .FirstOrDefault();

                lstUserEmail = dbContext.Usuario
                .Where(x => x.Txemail.ToLower() == userEmail.ToLower())
                .FirstOrDefault();
                
                lstLuc = dbContext.Luc.AsNoTracking().ToList();

                ModelState.Clear();
                if (IsNew)
                {
                    if (userPwd == null)
                    {
                        ModelState.AddModelError("", "Senha não preenchida. Verifique!");
                        return Page();
                    }
                    if(lstUserEmail != null){
                        if(lstUserEmail.Txemail.ToLower() == userEmail.ToLower()){
                            ModelState.AddModelError("", "E-mail já cadastrado. Verifique.");
                            return Page();
                        }
                    }
                }

                if (userPwd != null)
                {
                    if (!Services.Patterns.ValidatePassword(userPwd))
                    {
                        ModelState.AddModelError("", "Senha deve ter entre 8 e 15 caracteres, letras minúsculas, maiúsculas e números. Verifique.");
                        return Page();
                    }
                }
                /* if (userIsAdmin == true && userIsHR == true)
                {
                    ModelState.AddModelError("", "Operador não pode ser Administrador e RH ao mesmo tempo. Selecione apenas um. Verifique!");
                    return Page();
                } */
            
                if(lstUserLucGetAllow == null){
                    lstUserLucGetAllow = new List<UserLuc>();
                }
                
                if(lstUserLucGetDeny == null){
                    lstUserLucGetDeny = new List<UserLuc>();
                }

                if (IsNew)
                {
                    string HashPwd = Services.Patterns.getMD5Hash(userPwd);

                    Usuario? newUser = new Usuario
                    {
                        Iduser = Convert.ToInt32(id),
                        Txfantasia = nomeFantasia,
                        Txnopessoa = userName,
                        Txpwd = HashPwd,
                        Txemail = userEmail == null ? "" : userEmail.Trim(),
                        Isadmin = optType == 1 ? true : false,
                        Isactive = optActive == 0 ? true : false,                        
                        Dtlastaccess = null
                    };

                    dbContext.Usuario.Add(newUser);

                    dbContext.SaveChanges();

                    var userIdDB = dbContext.Usuario.FirstOrDefault(x => x.Txemail == userEmail);

                    foreach(var item in SelectedView){
                        UserLuc lucDB = new UserLuc();

                        lucDB.Iduser = Convert.ToInt32(userIdDB.Iduser);
                        lucDB.Idluc = item;
                        lucDB.Isallow = true;

                        dbContext.UserLuc.AddRange(lucDB);
                    }

                    foreach(var item in NonSelectedView){
                        UserLuc lucDB = new UserLuc();

                        lucDB.Iduser = Convert.ToInt32(userIdDB.Iduser);
                        lucDB.Idluc = item;
                        lucDB.Isallow = false;

                        dbContext.UserLuc.AddRange(lucDB);
                    }

                    dbContext.SaveChanges();
                    
                    Global.idLucs = new List<Luc>();

                    List<Luc> itemLuc = dbContext.Luc.Where(x => lucForm.Contains(Convert.ToByte(x.Idluc))).ToList();

                    foreach(var luc in itemLuc){
                        Luc lstLuc = new Luc();
                        lstLuc.Idluc = Convert.ToInt32(luc.Idluc);
                        lstLuc.Txluc = luc.Txluc;
                        Global.idLucs.Add(lstLuc);
                    }
                }
                else
                {
                    Usuario table = dbContext.Usuario
                                             .Where(x => x.Iduser == id).FirstOrDefault();

                    string OldTxemail = table.Txemail;
                    string OldTxpassword = table.Txpwd;
                    bool? OldIsAdmin = table.Isadmin;
                    if (userPwd != null)
                    {
                        string HashPwd = Services.Patterns.getMD5Hash(userPwd);
                        table.Txpwd = HashPwd;
                    }
                        
                    table.Txfantasia = nomeFantasia;
                    table.Txnopessoa = userName;
                    table.Txemail = userEmail;
                    table.Isadmin = optType == 1 ? true : false;
                    table.Isactive = optActive == 0 ? true : false;

                    List<UserLuc> lstRemoveUserLuc = dbContext.UserLuc.Where(x => x.Iduser == id).ToList();
                    dbContext.UserLuc.RemoveRange(lstRemoveUserLuc);
                    dbContext.SaveChanges();

                    foreach(var item in SelectedView){
                        UserLuc lucDB = new UserLuc();

                        lucDB.Iduser = Convert.ToInt32(id);
                        lucDB.Idluc = item;
                        lucDB.Isallow = true;

                        dbContext.UserLuc.AddRange(lucDB);
                    }

                    foreach(var item in NonSelectedView){
                        UserLuc lucDB = new UserLuc();

                        lucDB.Iduser = Convert.ToInt32(id);
                        lucDB.Idluc = item;
                        lucDB.Isallow = false;

                        dbContext.UserLuc.AddRange(lucDB);
                    }

                    dbContext.SaveChanges();

                    dbContext.Entry(table);
                    dbContext.Attach(table).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    
                    Global.idLucs = new List<Luc>();

                    List<Luc> itemLuc = dbContext.Luc.Where(x => lucForm.Contains(Convert.ToByte(x.Idluc))).ToList();

                    foreach(var luc in itemLuc){
                        Luc lstLuc = new Luc();
                        lstLuc.Idluc = Convert.ToInt32(luc.Idluc);
                        lstLuc.Txluc = luc.Txluc;
                        Global.idLucs.Add(lstLuc);
                    }
                }

                // if (IsNew)
                // {
                //     HttpContext.Session.SetString("OutputMessage", "Acesso concedido com sucesso!");
                // }
                // else
                // {
                //     HttpContext.Session.SetString("OutputMessage", "Acesso atualizado com sucesso!");
                // }

                return RedirectToPage("/Control/CtrlPnl");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }
    }
}
