using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class Document
    {
        public int DocumentId { get; set; }
        public int? DomainId { get; set; }
        public int? ClientId { get; set; }
        public string DocumentName { get; set; }
        public int? CategoryId { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
        public string Details { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual DocumentCategory Category { get; set; }
        public virtual Client Client { get; set; }
        public virtual AppDomain Domain { get; set; }
        public virtual Project Project { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
