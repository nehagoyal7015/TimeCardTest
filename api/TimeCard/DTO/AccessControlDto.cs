using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class AccessControlDto
    {
        public string AccessName { get; set; }
        public string PossibleActions { get; set; }
        public string Description { get; set; }
        
    }

    public class UpdateAccessControlDto
    {

        public int AccessControlId { get; set; }
        public string AccessName { get; set; }
        public string PossibleActions { get; set; }
        public string Description { get; set; }
        
    }

    public class ToDeleteAccessControlDto
    {

        public int AccessControlId { get; set; }
        public string AccessName { get; set; }
        public string PossibleActions { get; set; }
        public string Description { get; set; }
        
    }
}