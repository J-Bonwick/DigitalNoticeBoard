using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;
using Microsoft.AspNetCore.SignalR;

namespace DigitalNoticeBoard.Pages.Manage.NoticeDisplays
{
    public class DeleteModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;
        private readonly IHubContext<ReloadHub> _hubContext;

        public DeleteModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context, IHubContext<ReloadHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public NoticeDisplay NoticeDisplay { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NoticeDisplay = await _context.NoticeDisplays.FirstOrDefaultAsync(m => m.NoticeDisplayID == id);

            if (NoticeDisplay == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NoticeDisplay = await _context.NoticeDisplays.FindAsync(id);

            if (NoticeDisplay != null)
            {
                _context.NoticeDisplays.Remove(NoticeDisplay);
                await _context.SaveChangesAsync();
            }

            await _hubContext.Clients.All.SendAsync("Reload");

            return RedirectToPage("./Index");
        }
    }
}
