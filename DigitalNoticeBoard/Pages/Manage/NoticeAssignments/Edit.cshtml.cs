using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;

namespace DigitalNoticeBoard.Pages.Manage.NoticeAssignments
{
    public class EditModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;

        public EditModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NoticeAssignment NoticeAssignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NoticeAssignment = await _context.NoticeAssignments
                .Include(n => n.Notice)
                .Include(n => n.NoticeDisplay).FirstOrDefaultAsync(m => m.NoticeAssignmentID == id);

            if (NoticeAssignment == null)
            {
                return NotFound();
            }
           ViewData["NoticeID"] = new SelectList(_context.Notices, "NoticeID", "NoticeID");
           ViewData["NoticeDisplayID"] = new SelectList(_context.NoticeDisplays, "NoticeDisplayID", "NoticeDisplayID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NoticeAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticeAssignmentExists(NoticeAssignment.NoticeAssignmentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NoticeAssignmentExists(int id)
        {
            return _context.NoticeAssignments.Any(e => e.NoticeAssignmentID == id);
        }
    }
}
