using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages.Files
{
    [Authorize(Roles = "Administrator")]
    public class SendMailModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private PLDbContext dbContext;
        [BindProperty]
        public IFormFileCollection Upload { get; set; }
        [BindProperty]
        public DateTime dtVenc { get; set; }
        [BindProperty]
        public List<int?> lstStore { get; set; }
        public List<UserLuc> lstLuc { get; set; }
        public string OutputMessage { get; set; }
        public string ValidaOutPut { get; set; }
        public SendMailModel(IWebHostEnvironment environment, PLDbContext dbContext)
        {
            this._environment = environment;
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            LoadData();
            if(HttpContext.Session.GetString("OutputMessage") != null){
                OutputMessage = HttpContext.Session.GetString("OutputMessage");
            }
            if(HttpContext.Session.GetString("ValidaOutPut") != null){
                ValidaOutPut = HttpContext.Session.GetString("ValidaOutPut");
            }
            dtVenc = DateTime.Now;
            HttpContext.Session.Remove("OutputMessage");
            HttpContext.Session.Remove("ValidaOutPut");
        }
        public void LoadData(){
            lstLuc = dbContext.UserLuc.Include(x => x.IdlucNavigation).AsNoTracking().ToList();
        }
        public async void OnPostAsync()
        {
            try
            {
                LoadData();
                ModelState.Clear();
                OutputMessage = "E-mail enviado com sucesso.";
                ValidaOutPut = "SUCCESS";

                return;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return;
            }
        }
        
    }
}
