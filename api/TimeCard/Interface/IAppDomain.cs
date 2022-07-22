using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IAppDomain
    {
        void AddAppDomain(AppDomainEntity AppDomainEntity);
        void UpdateDomain(AppDomainEntity domain);
    }
}
