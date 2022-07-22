using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Errors;
using TimeCard.Models;
using KeyValuePair = TimeCard.Common.KeyValuePair;

namespace TimeCard.DataAcess
{
    public class ClientDataaAccess: BaseDataAccess
    {
     public ClientDataaAccess(TimeCardAppContext dataContext) : base(dataContext)
     {
     }

        public void AddClient(ClientEntity client, int userId)
        {
            var info = new Client()
            {
                DomainId = DataContext.UserAccounts.Where(u => u.UserId == userId).Select(s => s.DomainId).FirstOrDefault(),
                ClientName = client.ClientName,
                IsActive = client.IsActive,
                Country = client.Country,
                ContactNo = client.ContactNo,
                EmailAddress = client.EmailAddress,
                Website = client.Website,
                Address = client.Address,
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                ModifiedBy = userId,
                ModifiedOn = DateTime.Now
            };
            DataContext.Clients.Add(info);
        }

        public void UpdateClient(ClientUpdateEntity client, int userId) 
        {
            var clientinfo = DataContext.Clients.Where(c => c.ClientId == client.ClientId).Select(s => s).FirstOrDefault();
            if (clientinfo != null)
            {
                clientinfo.ClientName = client.ClientName;
                clientinfo.IsActive = client.IsActive;
                clientinfo.Country = client.Country;
                clientinfo.ContactNo = client.ContactNo;
                clientinfo.EmailAddress = client.EmailAddress;
                clientinfo.Website = client.Website;
                clientinfo.Address = client.Address;
                clientinfo.ModifiedBy = userId;
                clientinfo.ModifiedOn = DateTime.Now;
            }
            else
            {
                throw new AppException("Client Not Found");
            }

        }

        public List<ClientProject> GetClientProject()
        {
            var clientInfo = new List<ClientProject>();
            List<ClientProject> clientPro = (from c in DataContext.Clients
                                             join u in DataContext.UserAccounts on c.DomainId equals u.DomainId
                                             where c.CreatedBy == u.UserId
                                             select new ClientProject 
                                             {
                                                 ClientId = c.ClientId,
                                                 ClientName = c.ClientName,
                                                 IsActive = c.IsActive,
                                                 CreatedBy = u.UserName,
                                                 CreatedOn = c.CreatedOn,
                                                 ModifiedOn = c.ModifiedOn,
                                             }).ToList();
            foreach (var info in clientPro.GroupBy(g => new { g.ClientId, g.ClientName })) 
            {
                var info1 = new ClientProject();
                info1.ClientId = info.Select(s => s.ClientId).FirstOrDefault();
                info1.ClientName = info.Select(s => s.ClientName).FirstOrDefault();
                info1.IsActive = info.Select(s => s.IsActive).FirstOrDefault();
                info1.CreatedBy = info.Select(s => s.CreatedBy).FirstOrDefault();
                info1.CreatedOn = info.Select(s => s.CreatedOn).FirstOrDefault();
                info1.ModifiedOn = info.Select(s => s.ModifiedOn).FirstOrDefault();
                clientInfo.Add(info1);
            }
            return clientInfo;
        }

        public List<ProjectList> ProjectDetails(int clientId)
        {
            var pojectInfo = (from p in DataContext.Projects
                              join ud in DataContext.UserAccounts on p.ManagerId equals ud.UserId
                              join u in DataContext.UserDetails on ud.UserId equals u.UserId
                              
                              where p.ClientId == clientId
                              select new ProjectList
                              {
                                  ProjectId = p.ProjectId,
                                  Name = p.Name,
                                  BudgetHours = p.BudgetHours,
                                  OwnerShips = ud.UserName
                              }).ToList();

            return pojectInfo;

        }
        public List<KeyValuePair> GetClientList()
        {

            var clientList = (from u in DataContext.Clients
                                         select new KeyValuePair
                                         {
                                             Key = u.ClientId,
                                             Description = u.ClientName
                                         }).ToList();

            return clientList;
        }

      
    }
}
