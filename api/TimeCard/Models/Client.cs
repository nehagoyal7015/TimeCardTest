using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class Client
    {
        public Client()
        {
            Documents = new HashSet<Document>();
            Projects = new HashSet<Project>();
        }

        public int ClientId { get; set; }

        public int DomainId { get; set; }
        public string ClientName { get; set; }
        public bool IsActive { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public string RegistrationNo { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Address { get; set; }
        public bool? IsDefault { get; set; }

        public virtual AppDomain Domain { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
