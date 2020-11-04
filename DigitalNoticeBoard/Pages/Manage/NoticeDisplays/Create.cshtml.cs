using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;
using Microsoft.AspNetCore.SignalR;

namespace DigitalNoticeBoard.Pages.Manage.NoticeDisplays
{
    public class CreateModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;
        private readonly IHubContext<ReloadHub> _hubContext;

        public CreateModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context, IHubContext<ReloadHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public NoticeDisplay NoticeDisplay { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NoticeDisplays.Add(NoticeDisplay);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("Reload");

            return RedirectToPage("./Index");
        }
    }
}
