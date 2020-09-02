using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigitalNoticeBoard.Models;

namespace DigitalNoticeBoard.Data
{
    public class DigitalNoticeBoardContext : DbContext
    {
        public DigitalNoticeBoardContext (DbContextOptions<DigitalNoticeBoardContext> options)
            : base(options)
        {
        }

        public DbSet<DigitalNoticeBoard.Models.Notice> Notice { get; set; }

        public DbSet<DigitalNoticeBoard.Models.NoticeDisplay> NoticeDisplay { get; set; }

        public DbSet<DigitalNoticeBoard.Models.NoticeAssignment> NoticeAssignment { get; set; }
    }
}
