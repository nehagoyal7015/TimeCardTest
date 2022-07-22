using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class DocumentCategory
    {
        public DocumentCategory()
        {
            Documents = new HashSet<Document>();
            InverseCategoryParent = new HashSet<DocumentCategory>();
        }

        public int CategoryId { get; set; }
        public int? DomainId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryParentId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual DocumentCategory CategoryParent { get; set; }
        public virtual AppDomain Domain { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DocumentCategory> InverseCategoryParent { get; set; }
    }
}
