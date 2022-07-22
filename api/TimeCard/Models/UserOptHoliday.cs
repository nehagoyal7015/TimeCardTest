using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class UserOptHoliday
    {
        public int UserOptHolidayId { get; set; }
        public int HolidayId { get; set; }
        public bool IsOpting { get; set; }
        public bool IsVoided { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Holiday Holiday { get; set; }
    }
}
