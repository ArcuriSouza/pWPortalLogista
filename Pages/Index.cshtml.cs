using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pWPortalLogista.Pages
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            if(User.IsInRole("Administrator") || User.IsInRole("Normal")){
                return RedirectToPage("/Files/CheckFiles");
            }
            return Page();
        }
    }
}
