using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public partial class HikeLog
    {
        [Key]
        public int HikeId { get; set; }
        [StringLength(100)]
        [System.ComponentModel.DisplayName("Mountain")]
        public string MountainName { get; set; }
        public int MountainId { get; set; }
        [Column(TypeName = "date")]
        [System.ComponentModel.DisplayName("Date Hiked")]
        public DateTime? DateHiked { get; set; }
        [System.ComponentModel.DisplayName("Time to Summit (hh:mm)")]
        public TimeSpan? TimeToSummit { get; set; }

        [ForeignKey("MountainId")]
        [InverseProperty("HikeLogMountain")]
        public virtual Mountain Mountain { get; set; }
        public virtual Mountain MountainNameNavigation { get; set; }
    }
}
