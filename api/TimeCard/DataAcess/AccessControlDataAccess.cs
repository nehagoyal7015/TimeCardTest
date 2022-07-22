using System;
using System.Collections.Generic;
using System.Linq;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class AccessControlDataAccess : BaseDataAccess
    {
        public AccessControlDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        public bool AddAccessControl(AccessControlDto accessControl, int userId, int domainId)
        {
            var groupInfo = new AccessControl();
            groupInfo.DomainId = domainId; 
            groupInfo.AccessName = accessControl.AccessName;
            groupInfo.Description = accessControl.Description;
            groupInfo.PossibleActions = accessControl.PossibleActions;
            groupInfo.CreatedBy = userId;
            groupInfo.CreatedOn = DateTime.Now;
            groupInfo.ModifiedBy = userId;
            groupInfo.ModifiedOn = DateTime.Now;
             DataContext.AccessControls.Add(groupInfo);
            if (DataContext.SaveChanges() == 1)
                return true;
            else
                return false;
        }

        public bool UpdateAccessControl(UpdateAccessControlDto updateAccessControl, int userId)
        {
            var groupInfo = DataContext.AccessControls.Where(u => u.AccessControlId == updateAccessControl.AccessControlId).Select(u => u).FirstOrDefault();
            
            groupInfo.AccessName = updateAccessControl.AccessName;
            groupInfo.Description = updateAccessControl.Description;
            groupInfo.PossibleActions = updateAccessControl.PossibleActions;
            groupInfo.ModifiedBy = userId;
            groupInfo.ModifiedOn = DateTime.Now;

             DataContext.AccessControls.Update(groupInfo);
            if (DataContext.SaveChanges() == 1)
                return true;
            else
                return false;
        }

       

        public bool DeleteAccessControl(int accessControlId)
        {

            var info = DataContext.AccessControls.FirstOrDefault(u => u.AccessControlId == accessControlId);
            if (info != null)
            {
                DataContext.AccessControls.Remove(info);
                DataContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<UpdateAccessControlDto> GetAccessControl(int domainId)
        {
            var accessControl = DataContext.AccessControls.Where(n => n.DomainId == domainId).Select(u =>  new UpdateAccessControlDto
            {
                AccessControlId = u.AccessControlId,
                AccessName = u.AccessName,
                PossibleActions = u.PossibleActions,
                Description = u.Description
            }).ToList();

            return accessControl;
            
        }
    }
}