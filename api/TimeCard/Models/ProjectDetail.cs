using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class ProjectDetail
    {
        public int ProjectId { get; set; }
        public int PkId { get; set; }
        public int CretedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual UserDetail Pk { get; set; }
        public virtual Project Project { get; set; }
    }
}
