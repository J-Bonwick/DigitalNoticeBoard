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

        public DbSet<DigitalNoticeBoard.Models.Notice> Notices { get; set; }
        public DbSet<DigitalNoticeBoard.Models.NoticeDisplay> NoticeDisplays { get; set; }
        public DbSet<DigitalNoticeBoard.Models.NoticeAssignment> NoticeAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notice>().ToTable("Notice");
            modelBuilder.Entity<NoticeAssignment>().ToTable("NoticeAssignment");
            modelBuilder.Entity<NoticeDisplay>().ToTable("NoticeDisplay");
        }
    }
}
