using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class DomainSetting
    {
        public int PkId { get; set; }
        public int? DomainId { get; set; }
        public string Title { get; set; }
        public string LogoUrl { get; set; }
        public float WorkingHours { get; set; }
        public string WorkingDay { get; set; }
        public string TimeZone { get; set; }
        public int OptingHolidayCount { get; set; }
        public int SickLeaveCount { get; set; }
        public int UnPlannedLeaveCount { get; set; }
        public int TotalLeave { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual AppDomain Domain { get; set; }
    }
}
