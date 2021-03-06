﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;
using Microsoft.AspNetCore.SignalR;

namespace DigitalNoticeBoard.Pages.Manage.Notices
{
    public class EditModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;
        private readonly IHubContext<ReloadHub> _hubContext;

        public EditModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context, IHubContext<ReloadHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Notice Notice { get; set; }
        public IList<NoticeDisplay> NoticeDisplay { get; set; }
        public IList<NoticeAssignment> NoticeAssignments { get; set; }
        [BindProperty]
        public List<int> checkedDisplays { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notice = await _context.Notices.FirstOrDefaultAsync(m => m.NoticeID == id);

            if (Notice == null)
            {
                return NotFound();
            }
            NoticeDisplay = await _context.NoticeDisplays.ToListAsync();
            NoticeAssignments = await _context.NoticeAssignments.ToListAsync();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //Set the date enable bit so that the model is still valid
            Notice.NoticeStartDate = DateTime.Now;
            Notice.NoticeStopDate = DateTime.Now;
            Notice.EnabelStopDate = false;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Notice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticeExists(Notice.NoticeID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //This could probably be done better but this should work for now.
            //Delete the old assignments
            List<NoticeAssignment> assignments = await _context.NoticeAssignments.Where(m => m.NoticeID == Notice.NoticeID).ToListAsync();
            foreach(var assignment in assignments)
            {
                _context.NoticeAssignments.Remove(assignment);
            }
            await _context.SaveChangesAsync();

            //Add the new assignmnets
            NoticeAssignments = new List<NoticeAssignment>();
            foreach (var display in checkedDisplays)
            {
                NoticeAssignment assignment = new NoticeAssignment();
                assignment.NoticeID = Notice.NoticeID;
                assignment.NoticeDisplayID = display;
                NoticeAssignments.Add(assignment);
            }
            _context.NoticeAssignments.AddRange(NoticeAssignments);
            await _context.SaveChangesAsync();


            //Reload the client displays
            await _hubContext.Clients.All.SendAsync("Reload");

            return RedirectToPage("./Index");
        }

        private bool NoticeExists(int id)
        {
            return _context.Notices.Any(e => e.NoticeID == id);
        }
    }
}
