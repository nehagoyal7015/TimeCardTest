using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class Group
    {
        public Group()
        {
            GroupAccessControls = new HashSet<GroupAccessControl>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int GroupId { get; set; }
        public int DomainId { get; set; }
        public string GroupName { get; set; }
        public string ShortDescription { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual AppDomain Domain { get; set; }
        public virtual ICollection<GroupAccessControl> GroupAccessControls { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
