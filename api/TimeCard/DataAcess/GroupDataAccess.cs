using System;
using System.Collections.Generic;
using System.Linq;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class GroupDataAccess : BaseDataAccess
    {
        public GroupDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        public bool AddGroup(AddGroupDto group, int userId, int domainId)
        {
            var groupInfo = new Group();
            groupInfo.DomainId = domainId;
            groupInfo.GroupName = group.GroupName;
            groupInfo.ShortDescription = group.ShortDescription;
            groupInfo.CreatedBy = userId;
            groupInfo.CreatedOn = DateTime.Now;
            groupInfo.ModifiedBy = userId;
            groupInfo.ModifiedOn = DateTime.Now;

             DataContext.Groups.Add(groupInfo);
            if (DataContext.SaveChanges() == 1)
                return true;
            else
                return false;
        }


        public bool EditGroup(UpdateGroupDto group, int userId)
        {
            var groupInfo = DataContext.Groups.Where(u => u.GroupId == group.GroupId).Select(u => u).FirstOrDefault();
           
            groupInfo.GroupName = group.GroupName;
            groupInfo.ShortDescription = group.ShortDescription;
            groupInfo.ModifiedBy = userId;
            groupInfo.ModifiedOn = DateTime.Now;

             DataContext.Groups.Update(groupInfo);
            if (DataContext.SaveChanges() == 1)
                return true;
            else
                return false;
        }

        public List<UpdateGroupDto> GetGroup(int domainId)
        {
            var groups = DataContext.Groups.Where(u => u.DomainId == domainId).Select(u => new UpdateGroupDto
                {
                    DomainId = u.DomainId,
                    GroupId = u.GroupId,
                    GroupName = u.GroupName,
                    ShortDescription = u.ShortDescription,
                    

                }).ToList();

            return groups;
        }


       

        public bool DeleteGroup(int groupId)
        {
            var userCheck = DataContext.UserGroups.Where(u => u.GroupId == groupId).FirstOrDefault();

            if (userCheck != null)
            {
                throw new Exception("Group is assigned to user, please remove from there first");
                
            }

            var foundGroup = DataContext.GroupAccessControls.Where(u => u.GroupId == groupId).ToList();
            if (foundGroup != null)
            {
                DataContext.GroupAccessControls.RemoveRange(foundGroup);
                DataContext.SaveChanges();
            }
            var info = DataContext.Groups.FirstOrDefault(u => u.GroupId == groupId);
            if (info != null)
            {
                DataContext.Groups.Remove(info);
                DataContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }



        public List<UpdateGroupDto> GetAvailableGroup(int userId, int domainId)
        
        {
            var assigned = (from asinGroup in DataContext.UserGroups
                             join allGroups in DataContext.Groups on asinGroup.GroupId equals allGroups.GroupId 
                             where asinGroup.UserId == userId  
                             select new UpdateGroupDto
                {
                    DomainId = allGroups.DomainId,
                    GroupId = allGroups.GroupId,
                    GroupName = allGroups.GroupName,
                    ShortDescription = allGroups.ShortDescription,
                }).ToList();

            

            var available = (from allGroups in DataContext.Groups
                             where allGroups.DomainId == domainId
                                  select new UpdateGroupDto
                {
                    DomainId = allGroups.DomainId,
                    GroupId = allGroups.GroupId,
                    GroupName = allGroups.GroupName,
                    ShortDescription = allGroups.ShortDescription,
                    

                }).ToList();

            available.RemoveAll(r => assigned.Any(a => a.GroupId==r.GroupId));
            return available;

        }
        
        

        public List<UpdateGroupDto> GetAssignedGroup(int userId)
        {
            var groups = (from asinGroup in DataContext.UserGroups
                             join allGroups in DataContext.Groups on asinGroup.GroupId equals allGroups.GroupId 
                             where asinGroup.UserId == userId  
                             select new UpdateGroupDto
                {
                    DomainId = allGroups.DomainId,
                    GroupId = allGroups.GroupId,
                    GroupName = allGroups.GroupName,
                    ShortDescription = allGroups.ShortDescription,
                }).ToList();

            return groups;
        }
    }
}