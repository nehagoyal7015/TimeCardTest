using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IGroup
    {
        bool AddGroup(AddGroupDto group, int userId, int domainId);
        bool DeleteGroup(int groupId);

        bool EditGroup(UpdateGroupDto group, int userId);
        List<UpdateGroupDto> GetGroup(int domainId);

        List<UpdateGroupDto> GetAvailableGroup(int userId, int domainId);
        List<UpdateGroupDto> GetAssignedGroup(int userId);
    }
}