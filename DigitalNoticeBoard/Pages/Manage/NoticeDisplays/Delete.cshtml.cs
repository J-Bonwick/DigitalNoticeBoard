using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;

namespace DigitalNoticeBoard.Pages.Manage.NoticeDisplays
{
    public class DeleteModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;

        public DeleteModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NoticeDisplay NoticeDisplay { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NoticeDisplay = await _context.NoticeDisplay.FirstOrDefaultAsync(m => m.NoticeDisplayID == id);

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

            NoticeDisplay = await _context.NoticeDisplay.FindAsync(id);

            if (NoticeDisplay != null)
            {
                _context.NoticeDisplay.Remove(NoticeDisplay);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
