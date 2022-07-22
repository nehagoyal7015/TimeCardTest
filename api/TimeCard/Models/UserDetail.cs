using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            ProjectDetails = new HashSet<ProjectDetail>();
        }

        public int PkId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Desgination { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public bool IsActive { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string AlternateNumber { get; set; }
        public string PersonalEmail { get; set; }
        public string SkypeId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool? IsMaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public string SpousePhone { get; set; }
        public string SpouseEmail { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Skills { get; set; }
        public string Profile { get; set; }
        public string EmpId { get; set; }

        public virtual UserAccount User { get; set; }
        public virtual ICollection<ProjectDetail> ProjectDetails { get; set; }
    }
}
