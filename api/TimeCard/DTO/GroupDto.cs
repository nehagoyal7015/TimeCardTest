using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class AddGroupDto
    {
        public string GroupName  { get; set; }
        public string ShortDescription  { get; set; }
        // public int ApplicationId  { get; set; }
        
    }

    public class UpdateGroupDto
    {
        public int DomainId { get; set; }
        public int GroupId { get; set; }
        public string GroupName  { get; set; }
        public string ShortDescription  { get; set; }
        
    }

    
}