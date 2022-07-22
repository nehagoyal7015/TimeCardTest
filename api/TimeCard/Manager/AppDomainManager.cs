using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class AppDomainManager : BaseManager, IAppDomain
    {
        private readonly AppDomainDataAccess _appDomainDataAccess;

        public AppDomainManager(AppDomainDataAccess dataAccess)
        {
            _appDomainDataAccess = dataAccess;
        }
        public void AddAppDomain(AppDomainEntity AppDomainEntity)
        {
            _appDomainDataAccess.AddAppDomain(AppDomainEntity);
            _appDomainDataAccess.Commit();
        }
        public void UpdateDomain(AppDomainEntity domain)
        {
            _appDomainDataAccess.UpdateDomain(domain);
            _appDomainDataAccess.Commit();
        }
    }
}
