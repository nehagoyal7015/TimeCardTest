using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class AppDomain
    {
        public AppDomain()
        {
            AccessControls = new HashSet<AccessControl>();
            Clients = new HashSet<Client>();
            DocumentCategories = new HashSet<DocumentCategory>();
            Documents = new HashSet<Document>();
            DomainSettings = new HashSet<DomainSetting>();
            Groups = new HashSet<Group>();
            Projects = new HashSet<Project>();
            UserAccounts = new HashSet<UserAccount>();
        }

        public int DomainId { get; set; }
        public string DomainName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int ClientCode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<AccessControl> AccessControls { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<DocumentCategory> DocumentCategories { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DomainSetting> DomainSettings { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
