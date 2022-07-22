using System;
using System.Collections.Generic;
using System.Linq;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class GroupAccessControlDataAccess : BaseDataAccess
    {
        public GroupAccessControlDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        public bool AddGroupAccessControl(List<GroupAccessControlDto> group, int groupId, int userId)
        {
            var foundGroup = DataContext.GroupAccessControls.Where(u => u.GroupId == groupId).ToList();
            if (foundGroup != null)
            {
                DataContext.GroupAccessControls.RemoveRange(foundGroup);
                DataContext.SaveChanges();
            }

            var cont = 0;
            foreach (var item in group)
            {
            var groupInfo = new GroupAccessControl();

            groupInfo.GroupId = item.GroupId;
            groupInfo.AccessControlId = item.AccessControlId;
            groupInfo.IsCreateAllowed = item.IsCreateAllowed;
            groupInfo.IsDeleteAllowed = item.IsDeleteAllowed;
            groupInfo.IsReadAllowed = item.IsReadAllowed;
            groupInfo.IsUpdateAllowed = item.IsUpdateAllowed;
            groupInfo.CreatedBy = userId;
            groupInfo.CreatedOn = DateTime.Now;
            
            DataContext.GroupAccessControls.Add(groupInfo);
            cont++;
            }
            if (cont > 0){
                DataContext.SaveChanges();
                return true;}
            else
                return false;
        }


        

        public List<UpdateGroupAccessControlDto> GetAssignedAccessControl(int groupId)
        {
            var groups = (from groupAccessControl in DataContext.GroupAccessControls
                             join accessControl in DataContext.AccessControls on groupAccessControl.AccessControlId equals accessControl.AccessControlId 
                             where groupAccessControl.GroupId == groupId  
                             select new UpdateGroupAccessControlDto
                {   
                    GroupId = groupAccessControl.GroupId,
                    AccessName = accessControl.AccessName,
                    AccessControlId = groupAccessControl.AccessControlId,
                    IsCreateAllowed = groupAccessControl.IsCreateAllowed,
                    IsReadAllowed = groupAccessControl.IsReadAllowed,
                    IsUpdateAllowed = groupAccessControl.IsUpdateAllowed,
                    IsDeleteAllowed = groupAccessControl.IsDeleteAllowed
                }).ToList();

            return groups;
        }

        public List<UpdateAccessControlDto> GetAvialableAccessControl(int groupId)
        {

            var assigned = (from groupAccessControl in DataContext.GroupAccessControls
                             join accessControl in DataContext.AccessControls on groupAccessControl.AccessControlId equals accessControl.AccessControlId 
                             where groupAccessControl.GroupId == groupId 
                            select new UpdateAccessControlDto
                            {
                                      AccessControlId = accessControl.AccessControlId,
                                      AccessName = accessControl.AccessName,
                                      PossibleActions = accessControl.PossibleActions,
                                      Description = accessControl.Description
                            }).ToList();

            

            var accessCont = (from accessControl in DataContext.AccessControls
                                  select new UpdateAccessControlDto
                                  {
                                      AccessControlId = accessControl.AccessControlId,
                                      AccessName = accessControl.AccessName,
                                      PossibleActions = accessControl.PossibleActions,
                                      Description = accessControl.Description
                                  }).ToList();

            
            accessCont.RemoveAll(r => assigned.Any(a => a.AccessControlId==r.AccessControlId));

            return accessCont;

            

            
        }

        // from groupAccessControl in DataContext.GroupAccessControls
        //                      join accessControl in DataContext.AccessControls on groupAccessControl.AccessControlId equals accessControl.AccessControlId
        //                      where groupAccessControl.GroupId == groupId && groupAccessControl.AccessControlId != accessControl.AccessControlId
        //                      select new UpdateAccessControlDto

    //     (from accessControl in DataContext.AccessControls
    //                           from groupAccessControl in DataContext.GroupAccessControls
    //                          where groupAccessControl.GroupId == groupId && groupAccessControl.AccessControlId != accessControl.AccessControlId
    //                          select new UpdateAccessControlDto
    //             {
    //             AccessControlId = accessControl.AccessControlId,
    //             AccessName = accessControl.AccessName,
    //             PossibleActions = accessControl.PossibleActions,
    //             Description = accessControl.Description                
    //         }).ToList();

            
    //         return accessCont;
       
    }
}