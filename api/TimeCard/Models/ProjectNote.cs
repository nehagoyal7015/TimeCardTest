using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCard.Models​
{
    public partial class ProjectNote
    {
        public int NoteId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual Project Project { get; set; }
    }
}
