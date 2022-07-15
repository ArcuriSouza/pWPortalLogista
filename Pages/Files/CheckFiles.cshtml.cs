using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages
{
    [Authorize(Roles = "Administrator, Normal")]
    public class CheckFilesModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly PLDbContext dbContext;
        public CheckFilesModel(IWebHostEnvironment environment, PLDbContext dbContext)
        {
            this._environment = environment;
            this.dbContext = dbContext;
        }

        public string OutputMessage { get; set; }
        public string ValidaOutPut { get; set; }
        public List<Documentos> lstFiles { get; set; }
        public List<Documentos> lstFilesExpired { get; set; }
        public List<Documentos> lstFilesToExpire { get; set; }
        public bool userAllow { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            userAllow = true;
            string userId = User.Claims.FirstOrDefault(m => m.Type == "Id").Value;
            var userLucs = dbContext.UserLuc.Where(x => x.Iduser == Convert.ToInt32(userId)).Select(x => new { lucId = x.Idluc, lucName = x.IdlucNavigation.Txluc }).ToList();
            if(userLucs.Count() > 0){
                Global.idLucs = new List<Luc>();
                foreach(var luc in userLucs){
                    Luc lstLuc = new Luc();
                    lstLuc.Idluc = Convert.ToInt32(luc.lucId);
                    lstLuc.Txluc = luc.lucName;
                    Global.idLucs.Add(lstLuc);
                }

                if(Global.idLucHome == null){
                    Luc luc = Global.idLucs.FirstOrDefault();
                    Global.idLucHome = luc.Idluc;
                }
            }

            OutputMessage = "";
            ValidaOutPut = "";
            HttpContext.Session.Remove("OutputMessage");
            string webRootPath = _environment.WebRootPath;
            string[] fileEntries;

            //lstFiles = new List<Documentos>();
            fileEntries = Directory.GetFiles(webRootPath + "\\import\\");

            UserLuc userLuc = dbContext.UserLuc.AsNoTracking().Include(x => x.IdlucNavigation).FirstOrDefault(x => x.Iduser == Convert.ToInt32(userId) && x.Idluc == Global.idLucHome);
            Usuario lstUser = dbContext.Usuario.FirstOrDefault(x => x.Iduser == Convert.ToInt32(userId));
            lstFiles = new List<Documentos>();
            if(userLucs.Count() > 0){
                if(userLuc.Isallow == true){
                    List<string> docName = new List<string>();
                    foreach (string fileName in fileEntries){
                        docName.Add(fileName.Split("\\")[fileName.Split("\\").Length - 1]);
                    }

                    //var lstDocOpen = dbContext.DocuserOpen.Include(x => x.IddocNavigation).Where(x => x.Iduser == Convert.ToInt32(userId));

                    
                    lstFiles = dbContext.Documentos.AsNoTracking()
                    .Where(y => y.Txluc == userLuc.IdlucNavigation.Txluc)
                    .Include(x => x.DocuserOpen!)
                    .ToList();

                    lstFilesExpired = lstFiles.Where(x => x.Dtvenc < DateTime.Now.Date).ToList();
                    lstFilesToExpire = lstFiles.Where(x => x.Dtvenc >= DateTime.Now.Date).ToList();

                }else{
                    userAllow = false;
                }
            }else{
                userAllow = false;
            }            

            return Page();
        }
    }
}
