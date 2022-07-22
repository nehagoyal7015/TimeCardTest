using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class GroupAccessControlDto
    {
        public int GroupId { get; set; }
        public int AccessControlId { get; set; }
        public bool IsCreateAllowed { get; set; }
        public bool IsReadAllowed { get; set; }
        public bool IsUpdateAllowed { get; set; }
        public bool IsDeleteAllowed { get; set; }

    }

    public class UpdateGroupAccessControlDto
    {
        public int GroupId { get; set; }
        public string AccessName { get; set; }
        public int AccessControlId { get; set; }
        public bool IsCreateAllowed { get; set; }
        public bool IsReadAllowed { get; set; }
        public bool IsUpdateAllowed { get; set; }
        public bool IsDeleteAllowed { get; set; }

    }
    public class Id 
    {
        public int AccessControlId { get; set; }
    }
}