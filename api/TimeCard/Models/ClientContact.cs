using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class ClientContact
    {
        public int ClientId { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Other { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual Client Client { get; set; }
    }
}
