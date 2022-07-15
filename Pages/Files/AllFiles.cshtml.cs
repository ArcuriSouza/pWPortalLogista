using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages
{
    [Authorize(Roles = "Administrator")]
    public class AllFilesModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly PLDbContext dbContext;
        public AllFilesModel(IWebHostEnvironment environment, PLDbContext dbContext)
        {
            this._environment = environment;
            this.dbContext = dbContext;
        }

        public string OutputMessage { get; set; }
        public string ValidaOutPut { get; set; }
        public List<Documentos> lstFiles { get; set; }
        public List<Documentos> lstFilesExpired { get; set; }
        public List<Documentos> lstFilesToExpire { get; set; }
        public void OnGet()
        {
            OutputMessage = "";
            ValidaOutPut = "";
            HttpContext.Session.Remove("OutputMessage");
            string webRootPath = _environment.WebRootPath;
            string[] fileEntries;

            fileEntries = Directory.GetFiles(webRootPath + "\\import\\");
            
            List<string> docName = new List<string>();
            foreach (string fileName in fileEntries){
                docName.Add(fileName.Split("\\")[fileName.Split("\\").Length - 1]);
            }
            
            lstFiles = dbContext.Documentos.AsNoTracking()
                    .Include(x => x.DocuserOpen!)
                    .ToList();

            lstFilesExpired = lstFiles.Where(x => x.Dtvenc < DateTime.Now.Date).ToList();
            lstFilesToExpire = lstFiles.Where(x => x.Dtvenc >= DateTime.Now.Date).ToList();
                        
            // foreach (var fileName in lstDoc){
            //     Documentos docFile = new Documentos();
            //     docFile.Iddoc = fileName.Iddoc;
            //     docFile.Nodoc = Path.GetFileName(fileName.Nodoc);
            //     docFile.Dtvenc = fileName.Dtvenc;
            //     lstFiles.Add(docFile);
            // }

            if (HttpContext.Session.GetString("OutputMessage") != null)
            {
                OutputMessage = HttpContext.Session.GetString("OutputMessage");
                ValidaOutPut = "SUCCESS";

                HttpContext.Session.Remove("OutputMessage");
            }

            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            var userLucs = dbContext.UserLuc.Where(x => x.Iduser == userId).Select(x => new { lucId = x.Idluc, lucName = x.IdlucNavigation.Txluc }).ToList();
            Global.idLucs = new List<Luc>();
            foreach(var luc in userLucs){
                Luc lstLuc = new Luc();
                lstLuc.Idluc = Convert.ToInt32(luc.lucId);
                lstLuc.Txluc = luc.lucName;
                Global.idLucs.Add(lstLuc);
            }

        }
    }
}
