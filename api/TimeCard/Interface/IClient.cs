using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Models;
using KeyValuePair = TimeCard.Common.KeyValuePair;

namespace TimeCard.Interface
{
    public interface IClient
    {
        void AddClient(ClientEntity client, int userId);
        void UpdateClient(ClientUpdateEntity client,int userId);
        List<ClientProject> GetClientProject();
        List<KeyValuePair> GetClientList();
    }
}
