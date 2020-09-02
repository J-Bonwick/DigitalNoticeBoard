using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;

namespace DigitalNoticeBoard.Pages.Manage.NoticeAssignments
{
    public class CreateModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;

        public CreateModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["NoticeID"] = new SelectList(_context.Notice, "NoticeID", "NoticeID");
        ViewData["NoticeDisplayID"] = new SelectList(_context.NoticeDisplay, "NoticeDisplayID", "NoticeDisplayID");
            return Page();
        }

        [BindProperty]
        public NoticeAssignment NoticeAssignment { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NoticeAssignment.Add(NoticeAssignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
