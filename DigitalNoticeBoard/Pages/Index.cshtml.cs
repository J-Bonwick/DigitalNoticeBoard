using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalNoticeBoard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;

        [BindProperty(SupportsGet = true)]
        public string displayID { get; set; }
        public IList<NoticeAssignment> NoticeAssignment { get; set; }

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public IndexModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            NoticeAssignment = await _context.NoticeAssignments
                .Include(n => n.Notice)
                .Include(n => n.NoticeDisplay).ToListAsync();
        }
    }
}
