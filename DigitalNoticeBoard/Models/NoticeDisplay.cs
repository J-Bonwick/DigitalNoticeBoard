using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNoticeBoard.Models
{
    public enum TransitionType
    {
        Slide,
        Fade
    }
    public class NoticeDisplay
    {

        public int NoticeDisplayID { get; set; }
        [Required]
        [Display(Name = "Notice Display Name")]
        public string NoticeDisplayName { get; set; }
        [Required]
        [Display(Name = "Transition Interval")]
        public int NoticeDisplayInterval { get; set; }
        [Required]
        [Display(Name = "Transition Type")]
        public TransitionType NoticeDisplayTransitionType { get; set; } // true = slide, false = fade

        public ICollection<NoticeAssignment> NoticeAssignments { get; set; }
    }
}
