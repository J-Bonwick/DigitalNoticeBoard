using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNoticeBoard.Models
{
    public class Notice
    {
        public int NoticeID { get; set; }
        [Required]
        public string NoticeName { get; set; }
        //public byte[] NoticeImage { get; set; }
        public DateTime NoticeStartDate { get; set; }
        public DateTime NoticeStopDate { get; set; }
        public String NoticeImagePath { get; set; }



        public ICollection<NoticeAssignment> NoticeAssignments { get; set; }
    }
}
