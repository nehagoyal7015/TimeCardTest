using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class UserAccountEntity
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "UserEmail is required")]
        public string UserEmail { get; set; }
        public int? Ssoid { get; set; }
        public bool IsActive { get; set; }
      
    }

    public class ProfileEntity
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string AlternateNumber { get; set; }
        public string PersonalEmail { get; set; }
        public string SkypeId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool? IsMaritalstatus { get; set; }
        public string SpouseName { get; set; }
        public string SpousePhone { get; set; }
        public string SpouseEmail { get; set; }
        public List<string> SkillList { get; set; }
        public string Skills { get; set; }
        public string UserEmail { get; set; }
    }
}
