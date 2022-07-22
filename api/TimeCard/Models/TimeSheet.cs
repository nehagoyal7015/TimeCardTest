using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class TimeSheet
    {
        public int TimeSheetId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public DateTime RequestDate { get; set; }
        public float BillableHours { get; set; }
        public string BillableHoursNote { get; set; }
        public float? NonBillableHours { get; set; }
        public string NonBillableHoursNote { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsBilled { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Project Project { get; set; }
        public virtual ProjectTask Task { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
