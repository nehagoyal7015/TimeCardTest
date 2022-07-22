using System.Collections.Generic;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class UserGroupManager : BaseManager,IUserGroup
    {
        private readonly UserGroupDataAccess _userGroupDataAccess;
        public UserGroupManager(UserGroupDataAccess userGroupDataAccess)
        {
            _userGroupDataAccess = userGroupDataAccess;
        }

        public bool AddUserGroup(List<UserGroupDto> userGroup,int userId)
        {
            return _userGroupDataAccess.AddUserGroup(userGroup, userId);
        }

        public bool DeleteUserGroup(int userId)
        {
            return _userGroupDataAccess.DeleteUserGroup(userId);
        }

        public bool EditUserGroup(UserGroupDto userGroup, int userId)
        {
            return _userGroupDataAccess.EditUserGroup(userGroup,userId);
        }

        public UserGroupDto GetUserGroup(int userId)
        {
            return _userGroupDataAccess.GetUserGroup(userId);
        }
    }
}