using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;
using KeyValuePair = TimeCard.Common.KeyValuePair;

namespace TimeCard.Manager
{
    public class ClientManager : BaseManager, IClient
    {
        private readonly ClientDataaAccess _clientDataAccess;

        public ClientManager(ClientDataaAccess dataAccess)
        {
            _clientDataAccess = dataAccess;
        }


        public void AddClient(ClientEntity client, int userId)
        {
            _clientDataAccess.AddClient(client, userId);
            _clientDataAccess.Commit();
        }
        public void UpdateClient(ClientUpdateEntity client, int UserId)
        {
            _clientDataAccess.UpdateClient(client, UserId);
            _clientDataAccess.Commit();
        }
        public List<ClientProject> GetClientProject()
        {
            var clientProjectDetails = new List<ClientProject>();
            foreach(var info in _clientDataAccess.GetClientProject())
            {
                var data = new ClientProject();
                var projectInfo = _clientDataAccess.ProjectDetails(info.ClientId);
                data = info;
                data.ProjectCount = projectInfo.Count();
                data.ProjectList = projectInfo;
                clientProjectDetails.Add(data);
            }
            return clientProjectDetails;
        } 

        public List<KeyValuePair> GetClientList()
        {
           return _clientDataAccess.GetClientList(); 
        }
    }
}