using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class ProjectInvoice
    {
        public int InvoiceId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? InvoiceMonth { get; set; }
        public DateTime? InvoiceYear { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public float? TotalBillableHours { get; set; }
        public float? TotalNonBillableHours { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual Project Project { get; set; }
    }
}
