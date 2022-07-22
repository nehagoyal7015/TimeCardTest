using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class ProjectDetailDataAccess : BaseDataAccess
    {
        public ProjectDetailDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }
        public void AddProjectDetail(ProjectDetailEntity projectDetail)
        {
            var infoProject =  DataContext.UserDetails.Where(u=>u.IsActive == true ).ToList();

            if (infoProject.Count > 0)
            {
                foreach (var info in infoProject)
                {
                    var project = new Project();
                    var projectuser = new ProjectDetail();
                    projectuser.ProjectId = project.ProjectId;
                    projectuser.PkId = info.PkId;
                    projectuser.CretedBy = 1;
                    projectuser.CreatedOn = DateTime.Now;
                    projectuser.ModifiedBy = 1;
                    projectuser.ModifiedOn = DateTime.Now;

                    DataContext.ProjectDetails.Add(projectuser);
                }

            }

            DataContext.SaveChanges();

        }
    }
}
