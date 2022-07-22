using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class ProjectTask
    {
        public ProjectTask()
        {
            TimeSheets = new HashSet<TimeSheet>();
        }

        public int TaskId { get; set; }
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBillable { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal? BudgetCost { get; set; }
        public float? BudgetHours { get; set; }
        public bool? IsArchived { get; set; }
        public bool? IsDefault { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
    }
}
