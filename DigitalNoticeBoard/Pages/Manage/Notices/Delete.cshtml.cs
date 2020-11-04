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
using System.IO;
using Microsoft.Extensions.Hosting;

namespace DigitalNoticeBoard.Pages.Manage.Notices
{
    public class DeleteModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;
        private readonly IHostEnvironment hostingEnvironment;
        private readonly IHubContext<ReloadHub> _hubContext;

        public DeleteModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context, IHubContext<ReloadHub> hubContext, IHostEnvironment environment)
        {
            _context = context;
            _hubContext = hubContext;
            this.hostingEnvironment = environment;
        }

        [BindProperty]
        public Notice Notice { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notice = await _context.Notices.FindAsync(id);

            if (Notice != null)
            {
                _context.Notices.Remove(Notice);
                await _context.SaveChangesAsync();
            }
            //Delete the local file
            var dir = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/imageUpload");
            var filePath = Path.Combine(dir, Notice.NoticeImagePath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            await _hubContext.Clients.All.SendAsync("Reload");

            return RedirectToPage("./Index");
        }
    }
}
