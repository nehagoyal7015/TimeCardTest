using System;
using System.Collections.Generic;
using System.Linq;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class UserGroupDataAccess : BaseDataAccess
    {
        public UserGroupDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }
        public bool AddUserGroup(List<UserGroupDto> userGroup, int userId)
        {

            var found = DataContext.UserGroups.Where(u => u.UserId == userGroup[0].UserId).ToList();
            if (found != null)
            {
                DataContext.UserGroups.RemoveRange(found);
                DataContext.SaveChanges();
            }
            var cont = 0;
            foreach (var item in userGroup)
            {
                var groupInfo = new UserGroup();
            groupInfo.GroupId = item.GroupId;
            groupInfo.UserId = item.UserId;
            groupInfo.CreatedBy = userId;
            groupInfo.CreatedOn = DateTime.Now;

             DataContext.UserGroups.Add(groupInfo);
             DataContext.SaveChanges();
             cont++;
            }
            
            if (cont > 0){
                DataContext.SaveChanges();
                return true;}
            else
                return false;
        }


        public bool EditUserGroup(UserGroupDto userGroup, int userId)
        {
            var groupInfo = DataContext.UserGroups.Where(u => u.UserId == userGroup.UserId).Select(u => u).FirstOrDefault();
           
            groupInfo.GroupId = userGroup.GroupId;
            groupInfo.ModifiedBy = userId;
            groupInfo.ModifiedOn = DateTime.Now;

             DataContext.UserGroups.Update(groupInfo);
            if (DataContext.SaveChanges() == 1)
                return true;
            else
                return false;
        }

        public UserGroupDto GetUserGroup(int userId)
        {
            var userGroups = DataContext.UserGroups.Where(u => u.UserId == userId).Select(u => new UserGroupDto
                {
                    UserId = u.UserId,
                    GroupId = u.GroupId,                   

                }).FirstOrDefault();

            return userGroups;
        }

        public bool DeleteUserGroup(int userId)
        {

            var info = DataContext.UserGroups.FirstOrDefault(u => u.UserId == userId);
            if (info != null)
            {
                DataContext.UserGroups.Remove(info);
                DataContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}