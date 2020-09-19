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
        [Display(Name = "Name")]
        public string NoticeName { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime NoticeStartDate { get; set; }
        [Display(Name = "Stop Date")]
        [DataType(DataType.Date)]
        public DateTime NoticeStopDate { get; set; }
        public String NoticeImagePath { get; set; }



        public ICollection<NoticeAssignment> NoticeAssignments { get; set; }
    }
}
