using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class ProjectUserManager : BaseManager,IProjectUser
    {
        private readonly ProjectUserDataAccess _projectUserDataAccess;

        public ProjectUserManager(ProjectUserDataAccess projectUserDataAccess)
        {
            _projectUserDataAccess = projectUserDataAccess;
        }

        public List<GetRecentProj> GetRecentProjects(int userId)
        {
            return _projectUserDataAccess.GetRecentProjects(userId);
        }

        public List<Clients> GetUserProjects(int userId)
        {
            var taskInfo = _projectUserDataAccess.GetUserProjects(userId);
            if(taskInfo.Count == 1)
            {
                foreach (var info in taskInfo)
                {
                    if (info.Project.Count == 1)
                    {
                        info.ProjectId = info.Project.Select(s => s.ProjectId).FirstOrDefault();
                        
                        foreach(var task in info.Project)
                        {
                            if(task.ProjectTask.Count == 1)
                            {
                                info.TaskId = task.ProjectTask.Select(s => s.TaskId).FirstOrDefault();
                            }
                        }
                    }  
                }
            } 
            return taskInfo;
        }
    }
}
