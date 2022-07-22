using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class DomainSettingEntity
    {
        public string Title { get; set; }
        public string LogoUrl { get; set; }
        public float WorkingHours { get; set; }
        public string WorkingDay { get; set; }
        public string TimeZone { get; set; }
        public int OptingHolidayCount { get; set; }
        public int SickLeaveCount { get; set; }
        public int UnPlannedLeaveCount { get; set; }
        public int TotalLeave { get; set; }
    
    }
}
