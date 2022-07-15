using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages.Control
{
    [Authorize(Roles = "Administrator")]
    public class CtrlPnlModel : PageModel
    {
        private readonly PLDbContext dbContext;
        public CtrlPnlModel(PLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [BindProperty]
        public string? LucFilter { get; set; }
        [BindProperty]
        public string? EmailFilter { get; set; }
        [BindProperty]
        public string? NameFilter { get; set; }
        [BindProperty]
        public bool IsAdminFilter { get; set; }
        [BindProperty]
        public bool IsActiveFilter { get; set; }
        public string OutputMessage { get; set; }
        public List<Usuario> lstUser { get; set; }
        public void OnGet()
        {
            LoadData();
            if (HttpContext.Session.GetString("OutputMessage") != null && HttpContext.Session.GetString("OutputMessage") != "")
            {
                OutputMessage = HttpContext.Session.GetString("OutputMessage");
                HttpContext.Session.Remove("OutputMessage");
            }
            if (HttpContext.Session.GetString("LucFilter") != null && HttpContext.Session.GetString("LucFilter") != "")
            {
                LucFilter = HttpContext.Session.GetString("LucFilter");
            }
            if (HttpContext.Session.GetString("EmailFilter") != null && HttpContext.Session.GetString("EmailFilter") != "")
            {
                EmailFilter = HttpContext.Session.GetString("EmailFilter");
            }
            if (HttpContext.Session.GetString("NameFilter") != null && HttpContext.Session.GetString("NameFilter") != "")
            {
                EmailFilter = HttpContext.Session.GetString("NameFilter");
            }
            if (HttpContext.Session.GetString("IsAdminFilter") != null && HttpContext.Session.GetString("IsAdminFilter") != "")
            {
                IsAdminFilter = Convert.ToBoolean(HttpContext.Session.GetString("IsAdminFilter"));
            }
            if (HttpContext.Session.GetString("IsActiveFilter") != null && HttpContext.Session.GetString("IsActiveFilter") != "")
            {
                IsActiveFilter = Convert.ToBoolean(HttpContext.Session.GetString("IsActiveFilter"));
            }
            
            string IsSearched = "";
            if (HttpContext.Session.GetString("IsSearched") != null && HttpContext.Session.GetString("IsSearched") != "")
            {
                IsSearched = HttpContext.Session.GetString("IsSearched");
            }
            if (IsSearched != "")
            {
                SearchDB();
            }
        }
        public void LoadData()
        {
            OutputMessage = "";
            // lstCCusto = dbContext.RhVwCcusto.AsNoTracking()
            // .OrderBy(x => x.Nome)
            // .ToList();
        }

        public void OnPost()
        {
            try
            {
                LoadData();

                // if (LucFilter == null && EmailFilter == null)
                // {
                //     OutputMessage = "Nenhum filtro preenchido. Verifique!";
                //     return;
                // }

                HttpContext.Session.SetString("LucFilter", LucFilter == null ? "" : LucFilter.ToString());
                HttpContext.Session.SetString("EmailFilter", EmailFilter == null ? "" : EmailFilter);
                HttpContext.Session.SetString("NameFilter", NameFilter == null ? "" : NameFilter);
                HttpContext.Session.SetString("IsAdminFilter", IsAdminFilter.ToString());
                HttpContext.Session.SetString("IsActiveFilter", IsActiveFilter.ToString());
                HttpContext.Session.SetString("IsSearched", "S");

                SearchDB();

                if (lstUser.Count == 0)
                {
                    OutputMessage = "Nenhum registro encontrado. Verifique filtro aplicado!";
                }
            }
            catch (Exception ex)
            {
                OutputMessage = ex.Message;
                return;
            }
        }

        public void SearchDB()
        {
            IQueryable<Usuario> query = dbContext.Usuario.AsNoTracking();

            if (LucFilter != null)
            {
                List<int?> idUser = dbContext.UserLuc.AsNoTracking().Include(x => x.IdlucNavigation).Where(x => x.IdlucNavigation.Txluc.Contains(LucFilter)).Select(x => x.Iduser).ToList();
                query = query.Where(x => idUser.Contains(x.Iduser));
            }
            if (EmailFilter != null)
            {
                query = query.Where(x => x.Txemail.ToLower().Contains(EmailFilter.ToLower()));
            }
            if (NameFilter != null)
            {
                query = query.Where(x => x.Txnopessoa.ToLower().Contains(NameFilter.ToLower()));
            }
            if (IsAdminFilter == true)
            {
                query = query.Where(x => x.Isadmin == true);
            }
            if (IsActiveFilter == true)
            {
                query = query.Where(x => x.Isactive == true);
            }
            query = query.OrderBy(x => x.Txnopessoa);
            lstUser = query.ToList();

        }

    }
}
