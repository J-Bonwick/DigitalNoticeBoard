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
    public class DetailsModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;

        public DetailsModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context)
        {
            _context = context;
        }

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
    }
}
