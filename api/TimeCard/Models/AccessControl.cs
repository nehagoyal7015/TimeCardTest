using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class AccessControl
    {
        public AccessControl()
        {
            GroupAccessControls = new HashSet<GroupAccessControl>();
        }

        public int AccessControlId { get; set; }
        public int DomainId { get; set; }
        public string AccessName { get; set; }
        public string PossibleActions { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual AppDomain Domain { get; set; }
        public virtual ICollection<GroupAccessControl> GroupAccessControls { get; set; }
    }
}
