using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pWPortalLogista.Pages.Login
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            Global.idLucHome = null;
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // Reset all session parameters...
            Response.Redirect("./Login");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            Global.idLucHome = null;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // Reset all session parameters...
            return RedirectToPage("../Index");
        }
    }
}
