using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IGroupAccessControl
    {
        bool AddGroupAccessControl(List<GroupAccessControlDto> group, int groupId, int userId);
        List<UpdateGroupAccessControlDto> GetAssignedAccessControl(int groupId);
        List<UpdateAccessControlDto> GetAvialableAccessControl(int groupId);
       
    }
}