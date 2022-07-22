using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class UserGroup
    {
        public int UserGroupId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Group Group { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
