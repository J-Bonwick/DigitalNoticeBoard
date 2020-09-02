using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNoticeBoard.Models
{
    public class NoticeDisplay
    {
        public int NoticeDisplayID { get; set; }
        [Display(Name = "Notice Display Name")]
        public string NoticeBoardName { get; set; }

        public ICollection<NoticeAssignment> NoticeAssignments { get; set; }
    }
}
