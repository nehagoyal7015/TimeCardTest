using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class Project
    {
        public Project()
        {
            Documents = new HashSet<Document>();
            ProjectDetails = new HashSet<ProjectDetail>();
            ProjectInvoices = new HashSet<ProjectInvoice>();
            ProjectNotes = new HashSet<ProjectNote>();
            ProjectTasks = new HashSet<ProjectTask>();
            ProjectUsers = new HashSet<ProjectUser>();
            TimeSheets = new HashSet<TimeSheet>();
        }

        public int ProjectId { get; set; }
        public int DomainId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ManagerId { get; set; }
        public decimal? BudgetCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EstimateCompletion { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int ClientId { get; set; }
        public string OwnerShips { get; set; }
        public string Task { get; set; }
        public float? BudgetHours { get; set; }
        public float? ActualBudget { get; set; }
        public bool? IsArchive { get; set; }
        public bool? IsDefault { get; set; }

        public virtual Client Client { get; set; }
        public virtual AppDomain Domain { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<ProjectDetail> ProjectDetails { get; set; }
        public virtual ICollection<ProjectInvoice> ProjectInvoices { get; set; }
        public virtual ICollection<ProjectNote> ProjectNotes { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
    }
}
