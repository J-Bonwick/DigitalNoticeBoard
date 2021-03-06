﻿using System;
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
        [Display(Name = "Transition Interval (Sec)")]
        public double NoticeDisplayInterval { get; set; }
        [Required]
        [Display(Name = "Transition Type")]
        public TransitionType NoticeDisplayTransitionType { get; set; }

        public ICollection<NoticeAssignment> NoticeAssignments { get; set; }
    }
}
