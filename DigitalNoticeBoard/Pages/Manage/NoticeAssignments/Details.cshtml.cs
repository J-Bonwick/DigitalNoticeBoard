using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;

namespace DigitalNoticeBoard.Pages.Manage.NoticeAssignments
{
    public class DetailsModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;

        public DetailsModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context)
        {
            _context = context;
        }

        public NoticeAssignment NoticeAssignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NoticeAssignment = await _context.NoticeAssignment
                .Include(n => n.Notice)
                .Include(n => n.NoticeDisplay).FirstOrDefaultAsync(m => m.NoticeAssignmentID == id);

            if (NoticeAssignment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
