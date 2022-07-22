using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IUserGroup
    {
        bool AddUserGroup(List<UserGroupDto> userGroup, int userId);
        UserGroupDto GetUserGroup(int userId);
        bool EditUserGroup(UserGroupDto userGroup, int userId);
        bool DeleteUserGroup(int userId);
    }
}