using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class UserLoginDetail
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int UserId { get; set; }
        public int DomainId { get; set;}
        public string Token { get; set; }
    }

    public class Cred { 
        [Required(ErrorMessage = "email is required")]
        public string email { get; set; }
    }

    public class GetUserName
    {
        public int UserId { get; set; }
        public string UserName { get; set;}
    }

}
