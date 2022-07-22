using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Documents = new HashSet<Document>();
            ProjectUsers = new HashSet<ProjectUser>();
            TimeSheets = new HashSet<TimeSheet>();
            UserDetails = new HashSet<UserDetail>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int UserId { get; set; }
        public int DomainId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public int? Ssoid { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual AppDomain Domain { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
