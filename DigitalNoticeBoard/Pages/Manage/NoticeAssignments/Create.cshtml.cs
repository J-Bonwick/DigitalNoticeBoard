﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;
using Microsoft.AspNetCore.SignalR;

namespace DigitalNoticeBoard.Pages.Manage.NoticeAssignments
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
        ViewData["NoticeID"] = new SelectList(_context.Notices, "NoticeID", "NoticeName");
        ViewData["NoticeDisplayID"] = new SelectList(_context.NoticeDisplays, "NoticeDisplayID", "NoticeDisplayName");
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

            _context.NoticeAssignments.Add(NoticeAssignment);
            await _context.SaveChangesAsync();

            //await ReloadHub.SendReload();

            await _hubContext.Clients.All.SendAsync("Reload");

            return RedirectToPage("./Index");
        }
    }
}