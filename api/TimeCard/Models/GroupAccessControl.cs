using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class GroupAccessControl
    {
        public int GroupId { get; set; }
        public int AccessControlId { get; set; }
        public bool IsCreateAllowed { get; set; }
        public bool IsReadAllowed { get; set; }
        public bool IsUpdateAllowed { get; set; }
        public bool IsDeleteAllowed { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual AccessControl AccessControl { get; set; }
        public virtual Group Group { get; set; }
    }
}
