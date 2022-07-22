using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IAccessControl
    {
        bool AddAccessControl(AccessControlDto accessControl, int userId, int domainId);
        bool DeleteAccessControl(int accessControlID);
        bool UpdateAccessControl(UpdateAccessControlDto accessControl, int userId);
        List<UpdateAccessControlDto> GetAccessControl(int domainId);
    }
}