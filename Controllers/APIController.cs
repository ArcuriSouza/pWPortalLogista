using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using pWPortalLogista.Models;
using pWPortalLogista;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Globalization;

namespace pWPortalLogista.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Normal, Administrator")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        private readonly PLDbContext dbContext;
        public APIController(IWebHostEnvironment IHostingEnvironment, PLDbContext dbContext)
        {
            _environment = IHostingEnvironment;
            this.dbContext = dbContext;
        }

        // GET: ~/api/OpenDoc/xxx
        [HttpGet("OpenDoc/{idDoc}")]
        public string OpenDoc(int idDoc)
        {   
            int idUser = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            // if(User.IsInRole("Normal")){
                DocuserOpen docVerify = dbContext.DocuserOpen.FirstOrDefault(x => x.Iduser ==  idUser && x.Iddoc == idDoc);
                if(docVerify == null){
                    DocuserOpen docOpen = new DocuserOpen();
                    docOpen.Iddoc = idDoc;
                    docOpen.Iduser = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                    docOpen.Dtopen = DateTime.Now;

                    dbContext.DocuserOpen.Add(docOpen);
                    dbContext.SaveChanges();

                    return "Aberto com sucesso";
                }else{
                    return "";
                }
            // }

            return "";
        }

        
        [HttpPost]
        [Route("DeleteDoc/{idDoc}")]
        public string DeleteMsg(string idDoc)
        {
            try{   
                string cReturn = "";
                string[] lstIds = idDoc.Split(";");

                List<Documentos> lstDocs = dbContext.Documentos.AsNoTracking().Where(x => lstIds.Contains(x.Iddoc.ToString())).ToList();

                foreach(var item in lstDocs)
                {
                    System.IO.File.Delete("./wwwroot/import/" + item.Nodoc);
                }

                dbContext.RemoveRange(dbContext.DocuserOpen.Where(x => lstIds.Contains(x.Iddoc.ToString())).ToList());
                dbContext.RemoveRange(dbContext.Documentos.Where(x => lstIds.Contains(x.Iddoc.ToString())).ToList());
                dbContext.SaveChanges();

                HttpContext.Session.SetString("OutputMessage","Mensagens excluídas com sucesso!");

                cReturn = "Mensagens excluídas com sucesso!";
                return cReturn;
            }catch (Exception ex){
                dbContext.Database.RollbackTransaction();
                ModelState.AddModelError("", ex.Message);
                return "";
            }
        }

        [HttpPost]
        [Route("SetGlobalLuc/{idLuc}")]
        public string SetGlobalLuc(int idLuc)
        {
            try{   
                Global.idLucHome = idLuc;

                Response.StatusCode = 200;
                return "Success"; // Or the json object you need to return
            }catch (Exception ex){
                Response.StatusCode = 520;
                return "Server Error: " + ex.Message;
            }
        }

        [HttpPost]
        [Route("PostEmail/{lstLuc}")]
        public string PostEmail(string lstLuc)
        {
            string[] lstIds = lstLuc.Split(";");
            foreach(var item in lstIds){
               SendMail(Convert.ToInt32(item));
            }
            
            Response.StatusCode = 200;
            return "Success";
        }

        public bool SendMail(int lstLuc){
            List<UserLuc> lstLucLoj = dbContext.UserLuc.AsNoTracking().Include(x => x.IduserNavigation).Include(x => x.IdlucNavigation).Where(x => x.IdlucNavigation.Idluc == lstLuc && x.Isallow == true).ToList();

            foreach(var item in lstLucLoj){
                var subject = "Novo Boleto Cadastrado - Portal do Lojista";    

                var textContent = "";
                textContent += @"<table width='100%'>";
                textContent += @"	<tr>";
                textContent += @"	<td>";
                textContent += @"	</td>";
                textContent += @"	<td width='350px' align='center'>";
                textContent += @"	<tr>";
                textContent += @"		<td>";
                textContent += @"			<a href='#'><img src='https://i.imgur.com/mAKqONu.png' alt='' width='250px'/></a>";
                textContent += @"		</td>";
                textContent += @"	</tr>";
                textContent += @"	<tr style='display: flex; flex-direction: column; justify-content: center; align-items: center;'>";
                textContent += @"		<td>";
                textContent += @"			<h1>Olá, " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.IduserNavigation.Txfantasia.Split(" ")[0].ToLower()) + "</h1>    ";
                textContent += @"		</td>";
                textContent += @"	</tr> ";
                textContent += @"	<tr style='width: 90%; text-align: justify'>";
                textContent += @"		<td style='font-size: 12pt; width: '>";
                textContent += @"			Foi inserido um novo boleto no Portal do Lojista, favor conferir!";
                textContent += @"		</td>";
                textContent += @"	</tr>";
                textContent += @"	<tr style='width: 90%; text-align: justify'>";
                textContent += @"		<td style='font-size: 12pt; width: '>";
                textContent += @"			Clique no botão abaixo para acessar o portal. Caso não esteja visualizando, clique no link.";
                textContent += @"		</td>";
                textContent += @"	</tr>";
                
                textContent += @"
                <table role='presentation' align='center' class='x_row x_float-center' style='border-spacing:0; border-collapse:collapse; padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:center; padding:0; width:100%; margin:0 auto; float:none; display:table'>
                    <tbody>
                        <tr style='padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left'>
                            <th class='x_small-12 x_large-12 x_columns x_first x_last' style='word-wrap:break-word; border-collapse:collapse!important; padding-top:12px; padding-right:24px; padding-bottom:12px; padding-left:0; vertical-align:top; text-align:left; color:#11100f; font-family:Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:400; margin:0 auto; line-height:20px; font-size:14px; width:616px'>
                                <table role='presentation' style='border-spacing:0; border-collapse:collapse; padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left; width:100%'>
                                    <tbody>
                                        <tr style='padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left'>
                                            <th style='word-wrap:break-word; border-collapse:collapse!important; padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left; color:#11100f; font-family:Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:400; margin:0; line-height:20px; font-size:14px'>
                                                <table role='presentation' class='x_button' style='border-spacing:0; border-collapse:collapse; padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left; width:auto; margin:0'>
                                                    <tbody>
                                                        <tr style='padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left'>
                                                            <td style='word-wrap:break-word; border-collapse:collapse!important; padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left; color:#11100f; font-family:Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:400; margin:0; line-height:20px; font-size:14px'>
                                                                <table role='presentation' style='border-spacing:0; border-collapse:collapse; padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left; width:100%'>
                                                                    <tbody>
                                                                        <tr style='padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left'>
                                                                            <td style='word-wrap:break-word; border-collapse:collapse!important; padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:center; color:#ffffff; font-family:Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:400; margin:0; line-height:20px; font-size:14px; background:#0078d4; border:0; border-radius:8px'>";

                                                                            if (Config.IsDevelopment)
                                                                            {
                                                                                textContent += @"<a href='http://localhost:5001' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable' style='color:#ffffff; text-decoration:none; font-family:Segoe UI Semibold,SegoeUISemibold,Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:600; padding:12px 16px 12px 16px; text-align:left; line-height:1; font-size:16px; display:inline-block; border:0 solid #0078d4; border-radius:2px' data-linkindex='2'>Acessar Portal</a>";
                                                                            }
                                                                            else
                                                                            {
                                                                                textContent += @"<a href='http://realemp.com.br' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable' style='color:#ffffff; text-decoration:none; font-family:Segoe UI Semibold,SegoeUISemibold,Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:600; padding:12px 16px 12px 16px; text-align:left; line-height:1; font-size:16px; display:inline-block; border:0 solid #0078d4; border-radius:2px' data-linkindex='2'>Acessar Portal</a>";
                                                                            }
            textContent += @" 
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </th>
                                            <th class='x_expander' style='word-wrap:break-word; border-collapse:collapse!important; padding-top:0; padding-right:0; padding-bottom:0; padding-left:0; vertical-align:top; text-align:left; color:#11100f; font-family:Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:400; margin:0; line-height:20px; font-size:14px; visibility:hidden; width:0; padding:0!important'>
                                            </th>
                                        </tr>
                                    </tbody>
                                </table>
                            </th>
                        </tr>
                    </tbody>
                </table>
            ";

                textContent += @"	<tr style='width: 90%; text-align: justify'>";
                textContent += @"		<td style='font-size: 12pt; width: '>";
                textContent += @"			<a href='http://realemp.com.br/Login/Login'>http://realemp.com.br/Login/Login</a>";
                textContent += @"		</td>";
                textContent += @"	</tr>";
                textContent += @"	<tr>";
                textContent += @"		<td>";
                textContent += @"			<p>";
                textContent += @"				<i>Por favor, não responda esse e-mail. Respostas não serão lidas e nem respondidas.</i>";
                textContent += @"			</p>";
                textContent += @"		</td>";
                textContent += @"	</tr>";
                textContent += @"	</td>";
                textContent += @"	<td>";
                textContent += @"	</td>";
                textContent += @"	</tr>";
                textContent += @"</table>";

                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse("Real Empreendimentos <nao-responda@realemp.com.br>"));
                message.To.Add(MailboxAddress.Parse(item.IduserNavigation.Txemail));
                // message.To.Add(MailboxAddress.Parse("arcuri.souza@hotmail.com"));
                message.Subject = subject;
                message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = textContent };
                // message.IsBodyHtml = true;
                // message.BodyEncoding = Encoding.UTF8;
                

                // var client  = new SmtpClient("smtp.kinghost.net", 587);
                var client  = new SmtpClient();
                client.Connect("smtp.kinghost.net", 587, SecureSocketOptions.Auto);
                
                client.Authenticate("nao-responda@realemp.com.br", "J0s4Par@r3al");
                var autenticado = client.IsAuthenticated;
                var conectado = client.IsConnected;
                var encriptado = client.IsEncrypted;
                var sasl = client.SslHashAlgorithm;
                var aut = client.AuthenticationMechanisms;
                client.Send(message);
                client.Disconnect(true);
            }

            return true;
        }

    }

}
