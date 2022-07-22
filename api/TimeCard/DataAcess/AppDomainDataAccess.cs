using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Errors;
using TimeCard.Models;
using AppDomain = TimeCard.Models.AppDomain;

namespace TimeCard.DataAcess
{
    public class AppDomainDataAccess : BaseDataAccess
    {
        public AppDomainDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }
        public void AddAppDomain(AppDomainEntity AppDomainEntity)
        {

            var domainInfo = new AppDomain();

            domainInfo.DomainName = AppDomainEntity.DomainName;
            domainInfo.ContactPerson = AppDomainEntity.ContactPerson;
            domainInfo.Email = AppDomainEntity.Email;
            domainInfo.PhoneNumber = AppDomainEntity.PhoneNumber;
            domainInfo.Address = AppDomainEntity.Address;
            domainInfo.IsActive = AppDomainEntity.IsActive;
            domainInfo.ClientCode = AppDomainEntity.ClientCode;
            domainInfo.CreatedBy = 2;
            domainInfo.CreatedOn = DateTime.Now;
            domainInfo.ModifiedBy = 2;
            domainInfo.ModifiedOn = DateTime.Now;

            DataContext.AppDomains.Add(domainInfo);
            DataContext.SaveChanges();
        }

        public void UpdateDomain(AppDomainEntity domain)
        {
            var info = DataContext.AppDomains.Where(a => a.DomainId == domain.DomainId).Select(s => s).FirstOrDefault();

            if (info != null)
            {

                info.DomainName = domain.DomainName;
                info.ContactPerson = domain.ContactPerson;
                info.Email = domain.Email;
                info.PhoneNumber = domain.PhoneNumber;
                info.Address = domain.Address;
                info.IsActive = domain.IsActive;
                info.ClientCode = domain.ClientCode;

                DataContext.SaveChanges();
            }
            else 
            {
                throw new AppException("Domain Detail Not Found");
            }
        }
    }
}
