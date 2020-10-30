using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalNoticeBoard.Data;
using DigitalNoticeBoard.Models;

namespace DigitalNoticeBoard.Pages.Manage.Notices
{
    public class IndexModel : PageModel
    {
        private readonly DigitalNoticeBoard.Data.DigitalNoticeBoardContext _context;

        public IndexModel(DigitalNoticeBoard.Data.DigitalNoticeBoardContext context)
        {
            _context = context;
        }

        public IList<Notice> Notice { get;set; }
        public IList<NoticeAssignment> NoticeAssignment { get; set; }

        public async Task OnGetAsync()
        {
            Notice = await _context.Notices.ToListAsync();
            NoticeAssignment = await _context.NoticeAssignments
                .Include(n => n.Notice)
                .Include(n => n.NoticeDisplay).ToListAsync();
        }
    }
}
