using System.Collections.Generic;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class AccessControlManager : BaseManager, IAccessControl
    {
        private readonly AccessControlDataAccess _accessControlDataAccess;
        public AccessControlManager(AccessControlDataAccess accessControlDataAccess)
        {
            _accessControlDataAccess = accessControlDataAccess;
        }

        public bool AddAccessControl(AccessControlDto accessControl, int userId, int domainId)
        {
            return _accessControlDataAccess.AddAccessControl(accessControl, userId, domainId);
        }

        public bool DeleteAccessControl(int accessControlID)
        {
            return _accessControlDataAccess.DeleteAccessControl(accessControlID);
        }

        public List<UpdateAccessControlDto> GetAccessControl(int domainId)
        {
            return _accessControlDataAccess.GetAccessControl(domainId);
        }

        public bool UpdateAccessControl(UpdateAccessControlDto accessControl, int userId)
        {
            return _accessControlDataAccess.UpdateAccessControl(accessControl, userId);
        }
    }
}