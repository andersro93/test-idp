using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestIdp.Pages.Home;

[AllowAnonymous]
public class Index : PageModel
{
    public void OnGet()
    {
    }
}