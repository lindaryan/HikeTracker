using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public partial class Mountain
    {
        public Mountain()
        {
            HikeLogMountain = new HashSet<HikeLog>();
            HikeLogMountainNameNavigation = new HashSet<HikeLog>();
        }
        [System.ComponentModel.DisplayName("Mountain")]
        public int MountainId { get; set; }
        [Required]
        [StringLength(100)]
        [System.ComponentModel.DisplayName("Mountain")]
        public string MountainName { get; set; }
        [Required]
        [StringLength(10)]
        [System.ComponentModel.DisplayName("Elevation (ft.)")]
        [Range(0, 5, ErrorMessage = "Must be between 1 and 5")]
        public int Elevation { get; set; }
        public int? Difficulty { get; set; }

        [InverseProperty("Mountain")]
        public virtual ICollection<HikeLog> HikeLogMountain { get; set; }
        public virtual ICollection<HikeLog> HikeLogMountainNameNavigation { get; set; }
    }
}
