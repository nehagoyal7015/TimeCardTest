using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class AppDomainEntity
    {
        public int DomainId { get; set; }
        public string DomainName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int ClientCode { get; set; }
        
    }
    public class GetDomain
    {
        public int DomainId { get; set; }
    }
}
