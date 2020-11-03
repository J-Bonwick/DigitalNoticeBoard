using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace DigitalNoticeBoard.Pages.Manage.Notices
{
    public class CreateModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;
        private readonly IHostEnvironment hostingEnvironment;
        private readonly IHubContext<ReloadHub> _hubContext;
        public IList<NoticeDisplay> NoticeDisplay { get; set; }

        [BindProperty]
        public Notice Notice { get; set; }
        [Required]
        [BindProperty]
        public IFormFile UploadFile { get; set; }
        [BindProperty]
        public List<int> checkedDisplays { get; set; }
        private List<NoticeAssignment> NoticeAssignments { get; set; }
        //private NoticeAssignment NoticeAssignment { get; set; }

        public CreateModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context, IHostEnvironment environment, IHubContext<ReloadHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
            this.hostingEnvironment = environment;
        }



        public async Task OnGetAsync()
        {
            NoticeDisplay = await _context.NoticeDisplays.ToListAsync();
            //return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadFile != null)
            {
                if (UploadFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var dir = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/imageUpload");
                    Notice.NoticeImagePath = fileName;
                    var filePath = Path.Combine(dir, fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await UploadFile.CopyToAsync(stream);
                    }
                }
            }
            else
            {
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Notices.Add(Notice);
            await _context.SaveChangesAsync();
            NoticeAssignments = new List<NoticeAssignment>();
            foreach(var display in checkedDisplays)
            {
                NoticeAssignment assignment = new NoticeAssignment();
                assignment.NoticeID = Notice.NoticeID;
                assignment.NoticeDisplayID = display;
                NoticeAssignments.Add(assignment);
            }
            _context.NoticeAssignments.AddRange(NoticeAssignments);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("Reload");

            return RedirectToPage("./Index");
        }
    }
}
