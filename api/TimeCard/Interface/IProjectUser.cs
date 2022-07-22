using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IProjectUser
    {
        List<Clients> GetUserProjects(int userId);
        List<GetRecentProj> GetRecentProjects(int userId);
    }
}
