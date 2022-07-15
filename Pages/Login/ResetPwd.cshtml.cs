using System;
using System.Globalization;
using System.Linq;
// using System.Net.Mail;
// using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pWPortalLogista.Models;
using MimeKit;
using MimeKit.IO;
using MimeKit.Cryptography;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.IO;

namespace pWPortalLogista.Pages.Login
{
    [AllowAnonymous]
    public class ResetPwdModel : PageModel
    {
        private readonly PLDbContext dbContext;
        private IWebHostEnvironment _hostEnvironment;
        public ResetPwdModel(PLDbContext dbContext, IWebHostEnvironment environment)
        {
            this.dbContext = dbContext;
            this._hostEnvironment = environment;
        }
        
        [BindProperty]
        public string Login { get; set; }
        public void OnGet()
        {
        }  

        public void OnPost()
        {
            if (Login == null)
            {
                ModelState.AddModelError("", "Usuário inválido. Verifique.");
                return;
            }

            Usuario? user;

            // Login by E-mail
            user = dbContext.Usuario.FirstOrDefault(x => x.Txemail.ToLower().Trim() == Login.ToLower().Trim());

            if (user == null)
            {
                ModelState.AddModelError("", "Usuário inválido! Verifique.");
                return;
            }
            if (user.Txemail == null)
            {
                ModelState.AddModelError("", "Usuário não possui e-mail cadastrado. Não é possível recuperar sua senha. Verifique.");
                return;
            }

            string oldHash = user.Hashkey;
            user.Hashkey = Guid.NewGuid().ToString().Replace("-", "");
            user.Dtvalidhash = DateTime.Now.AddMinutes(20);

            dbContext.Attach(user);
            dbContext.SaveChanges();            
            
            var subject = "Recuperação de Senha - Real Empreendimentos";    

            var textContent = "";
            textContent += @"<table id='tableMail' width='100%' style='font-family: 'Roboto'; font-style: normal; padding: 20px;'>";
            textContent += @"	<tr>";
            textContent += @"	<td>";
            textContent += @"	</td>";
            textContent += @"	<td width='350px' align='center'>";
            textContent += @"	<tr>";
            textContent += @"		<td>";
            textContent += @"		    <table style='background-color: #fff!important'>";
            textContent += @"			    <a href='#'><img src='https://i.imgur.com/mAKqONu.png' alt='' width='250px'/></a>";
            textContent += @"		    </table>";
            textContent += @"		</td>";
            textContent += @"	</tr>";
            textContent += @"	<tr style='display: flex; flex-direction: column; justify-content: center; align-items: center;'>";
            textContent += @"		<td>";
            textContent += @"			<h1>Olá, " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.Txnopessoa.Split(" ")[0].ToLower()) + "</h1>    ";
            textContent += @"		</td>";
            textContent += @"	</tr> ";
            textContent += @"	<tr style='width: 90%; text-align: justify'>";
            textContent += @"		<td style='font-size: 12pt; width: '>";
            textContent += @"			Foi solicitado a recuperação da conta no Portal do Lojista, clique no botão abaixo para realizar a alteração da sua senha";
            textContent += @"		</td>";
            textContent += @"	</tr>";
            // textContent += @"	<tr>";
            // textContent += @"		<td>";
            // textContent += @"			<button class='btnSenha' style='width: 208px; height: 55px; background-color: #007BFF;border-radius: 8px; font-size: 15px; border: none; margin: 30px 0'>";
            
            
            // textContent += @"					<b>ALTERAR SUA SENHA</b>";
            // textContent += @"				</a>";
            // textContent += @"			</button>";
            // textContent += @"		</td>";
            // textContent += @"	</tr>";
            
            // textContent += @"<a href='https://localhost:5001/login/newpwd?key=" + user.Hashkey + "'>";
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
                                                                                textContent += @"<a href='http://localhost:5001/login/newpwd?key=" + user.Hashkey + @"' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable' style='color:#ffffff; text-decoration:none; font-family:Segoe UI Semibold,SegoeUISemibold,Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:600; padding:12px 16px 12px 16px; text-align:left; line-height:1; font-size:16px; display:inline-block; border:0 solid #0078d4; border-radius:2px' data-linkindex='2'>Alterar senha</a>";
                                                                            }
                                                                            else
                                                                            {
                                                                                textContent += @"<a href='http://realemp.com.br/Login/newpwd?key=" + user.Hashkey + @"' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable' style='color:#ffffff; text-decoration:none; font-family:Segoe UI Semibold,SegoeUISemibold,Segoe UI,SegoeUI,Roboto,&quot;Helvetica Neue&quot;,Arial,sans-serif; font-weight:600; padding:12px 16px 12px 16px; text-align:left; line-height:1; font-size:16px; display:inline-block; border:0 solid #0078d4; border-radius:2px' data-linkindex='2'>Alterar senha</a>";
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

            // textContent += @"	<tr>";
            // textContent += @"		<td>";
            // textContent += @"		    <i>Caso não consiga acessar através do botão, clique no link abaixo</i>";
            // textContent += @"		</td>";
            // textContent += @"	</tr>";
            // textContent += @"	<tr>";
            // textContent += @"		<td>";

            textContent += @"		</td>";
            textContent += @"	</tr>";
            textContent += @"	<tr>";
            textContent += @"		<td>";
            textContent += @"			<p>";
            textContent += @"				Caso não tenha sido você, <i>ignore este e-mail</i>";
            textContent += @"			</p>";
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
            message.To.Add(MailboxAddress.Parse(user.Txemail));
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


            // client.Host = "smtp.kinghost.net";
            // client.Port = 587;
            // client.EnableSsl = true;

            // client.UseDefaultCredentials = false;
            // NetworkCredential myCredentials = new NetworkCredential("nao-responda@realemp.com.br", "J0s4Par@r3al", "smtp.kinghost.net");
            // client.Credentials = myCredentials;

            // client.Send(message);
            // client.Dispose();

            // var textContent = @"Nome: " + Environment.NewLine + Environment.NewLine;
            // textContent += @"Telefone: " + Environment.NewLine + Environment.NewLine;
            // textContent += @"Email: " + Environment.NewLine + Environment.NewLine;
            // textContent += @"Mensagem: " + Environment.NewLine;

            //var email = MailHelper.CreateSingleEmail(from, to, subject, "", textContent);

            //client.SendEmailAsync(email);

            // // Send E-mail

            // MailMessage message = new MailMessage();
            // message.IsBodyHtml = true;
            // message.BodyEncoding = Encoding.UTF8;

            // message.From = new System.Net.Mail.MailAddress("noreply@josapar.com.br", "Não Responda");
            // message.To.Add(new System.Net.Mail.MailAddress(user.Txemail));
            // message.Bcc.Add(new System.Net.Mail.MailAddress("gabriel.souza@josapar.com.br"));
            // message.Subject = "Portal do Logista";
            // if (Config.IsDevelopment)
            // {
            //     message.Body = "<a href='https://localhost:5001/login/newpwd?key=" + user.Hashkey + "'>Clique aqui para alterar sua senha...</a>";
            // }
            // else
            // {
            //     message.Body = "<a href='https://realemp.com.br/Login/newpwd?key=" + user.Hashkey + "'>Clique aqui para alterar sua senha...</a>";
            // }

            // String ipServer = "10.1.50.120";
            // new SmtpClient()
            // {
            //     Host = ipServer,
            //     EnableSsl = false,
            //     DeliveryMethod = SmtpDeliveryMethod.Network
            // }.Send(message);

            Global.outPutMessage = "Enviamos para seu e-mail (" + user.Txemail + ") um link com a recuperação da senha. Verifique sua caixa de spam!";
            // HttpContext.Session.SetString("OutputMessage", "Enviamos para seu e-mail(" + user.Txemail + ") um link com a recuperação da senha. Verifique sua caixa de spam!");

            Response.Redirect("./ResetInfo");
        }

    }
}
