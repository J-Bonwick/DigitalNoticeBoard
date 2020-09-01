using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNoticeBoard.Models
{
    public class NoticeAssign
    {
        public int NoticeAssignID { get; set; }
        public int NoticeID { get; set; }
        public Notice Notice { get; set; }
        public int NoticeDisplayID { get; set; }
        public NoticeDisplay NoticeDisplay { get; set; }
    }
}
