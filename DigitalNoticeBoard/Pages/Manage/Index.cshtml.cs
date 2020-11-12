using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitalNoticeBoard.Pages.Manage
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (!User.IsInRole("~SCH8870ALL"))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
