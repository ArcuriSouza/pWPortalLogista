using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pWPortalLogista.Models;

namespace pWPortalLogista.Pages.Excel
{
    [Authorize(Roles = "Administrator")]
    public class ExcelModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private PLDbContext dbContext;

        public ExcelModel(IWebHostEnvironment environment, PLDbContext dbContext)
        {
            this._environment = environment;
            this.dbContext = dbContext;
        }
        [BindProperty]
        public string? strFolder { get; set; }
        [BindProperty]
        public IFormFile? Upload { get; set; }
        [BindProperty]
        public int typePlan { get; set; }
        public string OutputMessage { get; set; }
        public List<string> lstEmail { get; set; }
        public void OnGet()
        {
            lstEmail = new List<string>();
            OutputMessage = "";
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                lstEmail = dbContext.Usuario.AsNoTracking().Select(x => x.Txemail).ToList();

                ModelState.Clear();
                /* if (!ModelState.IsValid)
                {
                    return Page();
                } */

                /* if (Ativo == true)
                {
                    Table.Flgativo = "S";
                }
                else
                {
                    Table.Flgativo = "N";
                } */

                OutputMessage = "";

                if (Upload == null)
                {
                    ModelState.AddModelError("", "Arquivo não informado. Verifique!");
                    return Page();
                }
                
                string webRootPath = _environment.WebRootPath;

                var file = Path.Combine(webRootPath + "\\import\\" + Upload.FileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    Upload.CopyTo(fileStream);
                }

                var xls = new XLWorkbook(file);
                var planilha = xls.Worksheets.FirstOrDefault();
                var totalLines = planilha.Rows().Count();

                if (totalLines < 8)
                {
                    ModelState.AddModelError("", "Arquivo sem registros. Verifique!");
                    return Page();
                }
                
                ModelState.Clear();
                if(typePlan == 1){  
                    for(int i = 8; i <= totalLines; i++){
                        Usuario lstUser = new Usuario();
                        Luc lstLuc = new Luc();
                        string planEmail = planilha.Cell($"L{i}").Value.ToString().Trim();
                        string planLuc = planilha.Cell($"A{i}").Value.ToString().Trim();

                        Luc lucExists = dbContext.Luc.AsNoTracking().FirstOrDefault(x => x.Txluc == planLuc);

                        if(lucExists != null){
                            continue;
                        }
                        // REAL
                        if(!lstEmail.Contains(planEmail) && (planEmail != "" && planEmail != null) && (planLuc != "" && planLuc != null)){
                            lstEmail.Add(planEmail);
                            lstUser.Txcnpjcpf = planilha.Cell($"C{i}").Value.ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "");
                            
                            lstUser.Txcontrato = planilha.Cell($"E{i}").Value.ToString().Trim();
                            lstUser.Txqualif = planilha.Cell($"F{i}").Value.ToString().Trim();
                            lstUser.Txfantasia = planilha.Cell($"D{i}").Value.ToString().Trim();
                            lstUser.Txnopessoa = planilha.Cell($"G{i}").Value.ToString().Trim();
                            lstUser.Txendereco = planilha.Cell($"H{i}").Value.ToString().Trim();
                            lstUser.Txbairro = planilha.Cell($"I{i}").Value.ToString().Trim();
                            lstUser.Txemail = planilha.Cell($"L{i}").Value.ToString().Trim();
                            lstUser.Txcep = planilha.Cell($"M{i}").Value.ToString().Trim().Replace(" ", "").Replace("/", "").Replace("-", "");
                            lstUser.Isadmin = false;
                            lstUser.Isactive = true;

                            lstLuc.Txluc = planLuc;
                            lstLuc.Txfantasia = planilha.Cell($"D{i}").Value.ToString().Trim();

                            dbContext.Usuario.Add(lstUser);
                            dbContext.Luc.Add(lstLuc);
                            dbContext.SaveChanges();

                            Luc idLuc = dbContext.Luc.AsNoTracking().FirstOrDefault(x => x.Txluc == planLuc);
                            Usuario idUser = dbContext.Usuario.AsNoTracking().FirstOrDefault(x => x.Txemail == planEmail);
                            
                            UserLuc lstUserLuc = new UserLuc();
                            lstUserLuc.Iduser = idUser.Iduser;
                            lstUserLuc.Isallow = true;
                            lstUserLuc.Idluc = idLuc.Idluc;

                            dbContext.UserLuc.Add(lstUserLuc);
                            dbContext.SaveChanges();
                            
                        }
                    }
                //MONET
                }else if(typePlan == 2){   
                    for(int i = 8; i <= totalLines; i++){
                        Usuario lstUser = new Usuario();
                        Luc lstLuc = new Luc();
                        string planEmail = planilha.Cell($"N{i}").Value.ToString().Trim();
                        string planLuc = planilha.Cell($"A{i}").Value.ToString().Trim();

                        Luc lucExists = dbContext.Luc.AsNoTracking().FirstOrDefault(x => x.Txluc == planLuc);

                        if(lucExists != null){
                            continue;
                        }

                        if(!lstEmail.Contains(planEmail) && (planEmail != "" && planEmail != null) && (planLuc != "" && planLuc != null)){
                            lstEmail.Add(planEmail);
                            lstUser.Txluc = planilha.Cell($"A{i}").Value.ToString().Trim();
                            lstUser.Txcnpjcpf = planilha.Cell($"C{i}").Value.ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "");
                            lstUser.Txfantasia = planilha.Cell($"D{i}").Value.ToString().Trim();
                            lstUser.Txcontrato = planilha.Cell($"E{i}").Value.ToString().Trim();
                            lstUser.Txqualif = planilha.Cell($"F{i}").Value.ToString().Trim();
                            lstUser.Txfantasia = planilha.Cell($"D{i}").Value.ToString().Trim();
                            lstUser.Txnopessoa = planilha.Cell($"G{i}").Value.ToString().Trim();
                            lstUser.Txendereco = planilha.Cell($"H{i}").Value.ToString().Trim();
                            lstUser.Txbairro = planilha.Cell($"I{i}").Value.ToString().Trim() + " - " + planilha.Cell($"J{i}").Value.ToString().Trim() + " - " + planilha.Cell($"K{i}").Value.ToString().Trim();
                            lstUser.Txinsest = planilha.Cell($"L{i}").Value.ToString().Trim();
                            lstUser.Txinsmun = planilha.Cell($"M{i}").Value.ToString().Trim();
                            lstUser.Txemail = planilha.Cell($"N{i}").Value.ToString().Trim();
                            lstUser.Txcep = planilha.Cell($"O{i}").Value.ToString().Trim().Replace(" ", "").Replace("/", "").Replace("-", "");
                            lstUser.Isadmin = false;
                            lstUser.Isallow = true;
                            lstUser.Isactive = true;
                            
                            lstLuc.Txluc = planLuc;
                            lstLuc.Txfantasia = planilha.Cell($"D{i}").Value.ToString().Trim();

                            dbContext.Usuario.Add(lstUser);
                            dbContext.Luc.Add(lstLuc);
                            dbContext.SaveChanges();

                            Luc idLuc = dbContext.Luc.AsNoTracking().FirstOrDefault(x => x.Txluc == planLuc);
                            Usuario idUser = dbContext.Usuario.AsNoTracking().FirstOrDefault(x => x.Txemail == planEmail);
                            
                            UserLuc lstUserLuc = new UserLuc();
                            lstUserLuc.Iduser = idUser.Iduser;
                            lstUserLuc.Isallow = true;
                            lstUserLuc.Idluc = idLuc.Idluc;

                            dbContext.UserLuc.Add(lstUserLuc);
                            dbContext.SaveChanges();
                        }
                    }
                }

                OutputMessage = "Arquivo importado com sucesso.";
                return Page();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }
        // public void OnPost()
        // {   
        //     try
        //     {
                

        //         // string strMonthTest = planilha.Cell($"A2").Value.ToString().Trim();
        //         // DateTime dtMonthTest = DateTime.Parse("01/" + strMonthTest.Substring(4, 2) + "/" + strMonthTest.Substring(0, 4));
        //         // dtMonthTest = dtMonthTest.AddMonths(1);

        //         // decimal countTest = dbContext.ColabHealthreport.AsNoTracking()
        //         // .Where(x => x.Txmonth == Convert.ToDecimal(dtMonthTest.ToString("yyyyMM"))).Count();
        //         // if (countTest > 0)
        //         // {
        //         //     ModelState.AddModelError("", "Já existsm registros de saúde processados para esta competência. Risco de duplicidade. Processamento recusado. Verifique!");
        //         //     return;
        //         // }

        //         // dbContext.Database.BeginTransaction();

        //         // //dbContext.Database.AutoSavepointsEnabled = false;
        //         // for (int line = 2; line <= totalLines; line++)
        //         // {
        //         //     decimal idEmployee = decimal.Parse(planilha.Cell($"B{line}").Value.ToString().Trim());

        //         //     string strMonth = planilha.Cell($"A{line}").Value.ToString().Trim();
        //         //     DateTime dtMonth = DateTime.Parse("01/" + strMonth.Substring(4, 2) + "/" + strMonth.Substring(0, 4));
        //         //     dtMonth = dtMonth.AddMonths(1);
        //         //     decimal txMonth = decimal.Parse(dtMonth.ToString("yyyyMM"));

        //         //     string strDep = planilha.Cell($"C{line}").Value.ToString().Trim();
        //         //     decimal? idDep = null;
        //         //     if (strDep.Length > 0) idDep = decimal.Parse(strDep);

        //         //     string txName = planilha.Cell($"D{line}").Value.ToString().Trim();
        //         //     if (txName.Length > 50) txName.Substring(0, 50);

        //         //     string txHeathUnit = planilha.Cell($"E{line}").Value.ToString().Trim();
        //         //     if (txHeathUnit.Length > 80) txHeathUnit.Substring(0, 80);

        //         //     string txDegree = planilha.Cell($"F{line}").Value.ToString().Trim();
        //         //     if (txDegree.Length > 20) txDegree.Substring(0, 20);

        //         //     string idEventTable = planilha.Cell($"G{line}").Value.ToString().Trim();
        //         //     if (idEventTable.Length > 15) idEventTable.Substring(0, 15);

        //         //     string txEventTable = planilha.Cell($"H{line}").Value.ToString().Trim();
        //         //     if (txEventTable.Length > 100) txEventTable.Substring(0, 100);

        //         //     DateTime dtEvent = DateTime.Parse(planilha.Cell($"I{line}").Value.ToString().Trim());
        //         //     decimal qtEvent = decimal.Parse(planilha.Cell($"J{line}").Value.ToString().Trim());
        //         //     decimal vlEvent = decimal.Parse(planilha.Cell($"K{line}").Value.ToString().Trim());
        //         //     decimal vlEmployee = decimal.Parse(planilha.Cell($"L{line}").Value.ToString().Trim());

        //         //     // ColabHealthreport Table = new ColabHealthreport
        //         //     // {
        //         //     //     Idevent = 0, // Table trigger applies sequence number
        //         //     //     Idemployee = idEmployee,
        //         //     //     Txmonth = txMonth,
        //         //     //     Iddep = idDep,
        //         //     //     Txname = txName,
        //         //     //     Txhealthunit = txHeathUnit,
        //         //     //     Txdegree = txDegree,
        //         //     //     Ideventtable = idEventTable,
        //         //     //     Txeventtable = txEventTable,
        //         //     //     Dtevent = dtEvent,
        //         //     //     Qtevent = qtEvent,
        //         //     //     Vlevent = vlEvent,
        //         //     //     Vlemployee = vlEmployee
        //         //     // };
        //         //     // dbContext.ColabHealthreport.Add(Table);
        //         //     // dbContext.SaveChanges();
        //         // }

        //         // dbContext.Database.CommitTransaction();

                
        //         return;

        //     }
        //     catch (Exception ex)
        //     {
        //         // dbContext.Database.RollbackTransaction();
        //         ModelState.AddModelError("", ex.Message);
        //         return;
        //     }
        // }
    }
}
