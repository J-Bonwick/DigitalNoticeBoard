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

namespace DigitalNoticeBoard.Pages.Manage.Notices
{
    public class CreateModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;
        private readonly IHostEnvironment hostingEnvironment;

        public CreateModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context, IHostEnvironment environment)
        {
            _context = context;
            this.hostingEnvironment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Notice Notice { get; set; }
        [BindProperty]
        public IFormFile UploadFile { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadFile != null)
            {
                if (UploadFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(UploadFile.FileName);
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

            return RedirectToPage("./Index");
        }
    }
}
