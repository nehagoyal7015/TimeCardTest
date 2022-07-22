using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class ClientEntity
    {

    
        public string ClientName { get; set; }
        public bool IsActive { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }

    }

    public class ClientUpdateEntity
    {

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public bool IsActive { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }

    }
    public class ClientProject
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int ProjectCount { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<ProjectList> ProjectList { get; set; }

    }



}
