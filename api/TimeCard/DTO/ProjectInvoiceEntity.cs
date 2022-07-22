using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class ProjectInvoiceEntity
    {
        public int ProjectId { get; set; }
        public DateTime? InvoiceMonth { get; set; }
        public DateTime? InvoiceYear { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public float? TotalBillableHours { get; set; }
        public float? TotalNonBillableHours { get; set; }
        public string Comment { get; set; }
    }
}
