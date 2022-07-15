using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages.LucCtrl
{
    [Authorize(Roles = "Administrator")]
    public class LucPnlModel : PageModel
    {
        private readonly PLDbContext dbContext;
        public LucPnlModel(PLDbContext dbContext)
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
        public List<Luc> lstUser { get; set; }
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
            IQueryable<Luc> query = dbContext.Luc.AsNoTracking();

            if (LucFilter != null)
            {
                query = query.Where(x => x.Txluc.Contains(LucFilter));
            }
            
            query = query.OrderBy(x => x.Txluc);
            lstUser = query.ToList();

        }

    }
}
