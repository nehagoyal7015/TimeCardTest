using System.Collections.Generic;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class GroupManager : BaseManager, IGroup
    {
        private readonly GroupDataAccess _groupDataAccess;
        public GroupManager(GroupDataAccess groupDataAccess)
        {
            _groupDataAccess = groupDataAccess;
        }

        public bool AddGroup(AddGroupDto group, int userId, int domainId)
        {
            return _groupDataAccess.AddGroup(group,userId,domainId);
        }

        public bool DeleteGroup(int groupId)
        {
            return _groupDataAccess.DeleteGroup(groupId);
        }

        public bool EditGroup(UpdateGroupDto group, int userId)
        {
            return _groupDataAccess.EditGroup(group, userId);
        }

        public List<UpdateGroupDto> GetAssignedGroup(int userId)
        {
            return _groupDataAccess.GetAssignedGroup(userId);
        }

        public List<UpdateGroupDto> GetAvailableGroup(int userId, int domainId)
        {
            return _groupDataAccess.GetAvailableGroup(userId, domainId);
        }

        public List<UpdateGroupDto> GetGroup(int domainId)
        {
            return _groupDataAccess.GetGroup(domainId);
        }
    }
}