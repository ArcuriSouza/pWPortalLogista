using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pWPortalLogista.Pages.Login
{
    [AllowAnonymous]
    public class ResetInfoModel : PageModel
    {
        public string OutputMessage { get; set; }
        public void OnGet()
        {
            if(Global.outPutMessage != null || Global.outPutMessage != ""){
                OutputMessage = Global.outPutMessage;
            }
            Global.outPutMessage = null;
            // if (HttpContext.Session.GetString("OutputMessage") != null && HttpContext.Session.GetString("OutputMessage") != "")
            // {
            //     OutputMessage = HttpContext.Session.GetString("OutputMessage");
            //     HttpContext.Session.Remove("OutputMessage");
            // }
        }
    }
}
