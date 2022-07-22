using System.Collections.Generic;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class GroupAccessControlManager : BaseManager, IGroupAccessControl
    {
        private readonly GroupAccessControlDataAccess _groupAccessControlDataAccess;
        public GroupAccessControlManager(GroupAccessControlDataAccess groupAccessControlDataAccess)
        {
            _groupAccessControlDataAccess = groupAccessControlDataAccess;
        }

        public bool AddGroupAccessControl(List<GroupAccessControlDto> groupAccessControl,int groupId, int userId)
        {
            return _groupAccessControlDataAccess.AddGroupAccessControl(groupAccessControl,groupId,userId);
        }


        public List<UpdateGroupAccessControlDto> GetAssignedAccessControl(int groupId)
        {
            return _groupAccessControlDataAccess.GetAssignedAccessControl(groupId);
        }

        public List<UpdateAccessControlDto> GetAvialableAccessControl(int groupId)
        {
            return _groupAccessControlDataAccess.GetAvialableAccessControl(groupId);
        }
    }
}