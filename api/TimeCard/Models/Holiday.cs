using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class Holiday
    {
        public Holiday()
        {
            UserOptHolidays = new HashSet<UserOptHoliday>();
        }

        public int HolidayId { get; set; }
        public DateTime HolidayDate { get; set; }
        public bool IsOpting { get; set; }
        public string HolidayType { get; set; }
        public bool IsFloating { get; set; }
        public string HolidayReason { get; set; }
        public string HolidayDay { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<UserOptHoliday> UserOptHolidays { get; set; }
    }
}
