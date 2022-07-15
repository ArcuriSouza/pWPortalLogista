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

namespace pWPortalLogista.Pages.LucCtrl
{
    [Authorize(Roles = "Administrator")]
    public class LucEditModel : PageModel
    {
        private PLDbContext dbContext;

        public LucEditModel(PLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [FromRoute]
        public decimal? id { get; set; }
        [BindProperty]
        public string lucTxt { get; set; }
        [BindProperty]
        public string noFantasia { get; set; }
        public Luc lstLuc { get; set; }

        public Luc lstLucGet { get; set; }
        public List<string> lstData { get; set; }
        public bool IsNew
        {
            get { return lstLuc == null; }
        }

        public void OnGet()
        {
            lstLuc = dbContext.Luc.AsNoTracking().FirstOrDefault(x => x.Idluc == id);

            if (lstLuc == null)
            {
                lucTxt = "";
                noFantasia = "";
            }
            else
            {
                Luc lstLucGet = new Luc();
                
                lucTxt = lstLuc.Txluc == null ? "" : lstLuc.Txluc;
                noFantasia = lstLuc.Txfantasia == null ? "" : lstLuc.Txfantasia;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {                
                lstLuc = dbContext.Luc.AsNoTracking().FirstOrDefault(x => x.Idluc == id);
                ModelState.Clear();
                if (IsNew)
                {
                    if (lucTxt == null)
                    {
                        ModelState.AddModelError("", "Luc não preenchido. Verifique!");
                        return Page();
                    }
                    if(lstLuc != null){
                        if(lstLuc.Txluc.ToLower() == lucTxt.ToLower()){
                            ModelState.AddModelError("", "LUC já cadastrado. Verifique.");
                            return Page();
                        }
                    }
                }

                if (IsNew)
                {
                    Luc newUser = new Luc
                    {
                        Txluc = lucTxt,
                        Txfantasia = noFantasia
                    };

                    dbContext.Luc.Add(newUser);

                    dbContext.SaveChanges();
                }
                else
                {
                    Luc table = dbContext.Luc.Where(x => x.Idluc == id).FirstOrDefault();
                    table.Txluc = lucTxt;
                    table.Txfantasia = noFantasia;

                    dbContext.Entry(table);
                    dbContext.Attach(table).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                return RedirectToPage("/LucCtrl/LucPnl");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }
    }
}
