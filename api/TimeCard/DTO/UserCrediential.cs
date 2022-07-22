using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class UserCrediential
    {
        [Required(ErrorMessage = "Domain is required")]
        public string DomainName { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    public class GoogleCrediential 
    {
        [Required(ErrorMessage = "Email is required")]
        public string UserEmail { get; set; }
      
             
    }
}
