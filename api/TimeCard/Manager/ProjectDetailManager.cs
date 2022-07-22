using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class ProjectDetailManager : BaseManager, IProjectDetail
    {
        private readonly ProjectDetailDataAccess _projectDetailDataAccess;
        public ProjectDetailManager(ProjectDetailDataAccess dataAccess)
        {
            _projectDetailDataAccess = dataAccess;
        }

        public void AddProjectDetail(ProjectDetailEntity projectdetail)
        {
            _projectDetailDataAccess.AddProjectDetail(projectdetail);
            _projectDetailDataAccess.Commit();
        }
    }
}
