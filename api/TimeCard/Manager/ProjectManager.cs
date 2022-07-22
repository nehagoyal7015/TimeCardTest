using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;
using TimeCard.Models;
using KeyValuePair = TimeCard.Common.KeyValuePair;

namespace TimeCard.Manager
{
    public class ProjectManager : BaseManager,IProject
    {
        private readonly ProjectDataAccess _projectDataAccess;
        public ProjectManager(ProjectDataAccess dataAccess)
        {
            _projectDataAccess = dataAccess;
        }
            public void AddProject(ProjectEntity project, int userId)
            {
            _projectDataAccess.AddProject(project, userId);  
            }
        public void UpdateProject(ProjectEntity project, int userId)
        {
            _projectDataAccess.UpdateProject(project, userId);
            _projectDataAccess.Commit();
        }
        public List<ProjectList> GetProject() 
        {
            return _projectDataAccess.GetProject();
        }
        public List<KeyValuePair> GetEmployee() 
        {
            return _projectDataAccess.GetEmployee();
        }
        public List<TaskList> GetProjedctDetail(int ProjectId)
        {
            var ProjectDetails = new List<TaskList>();
            foreach (var info in _projectDataAccess.GetProjedctDetail(ProjectId))
            {
                var data = new TaskList();
                data.projectTaskList = new List<projectTaskList>();
                var projectInfo = GetProjectTask(ProjectId);
                float count = 0;
                foreach (var info2 in projectInfo) 
                {
                    count = count + info2.ActualBudget;
                }
                info.totalBudgetHours = count;
                data = info;
                data.projectTaskList = projectInfo;
                ProjectDetails.Add(data);
            }
            return ProjectDetails;
        }
        public List<projectTaskList> GetProjectTask(int ProjectId)
        {
            var EmpSubmitedHours = new List<projectTaskList>(ProjectId);
            foreach (var info1 in _projectDataAccess.GetProjectTask(ProjectId))
            {
                var data = new projectTaskList();
                data.GetEmpSubmittedHours = new List<GetEmpSubmittedHour>();
                var EmpHourInfo = _projectDataAccess.GetEmpSubmitHour(info1.userId,info1.TaskId);
               
                float Count = 0;
                foreach (var info in EmpHourInfo)
                {
                    Count = Count + info.Hours;
                }
                info1.ActualBudget = Count;
                data = info1;
                data.EmployeeCount = EmpHourInfo.Count();
                data.GetEmpSubmittedHours = EmpHourInfo;
              
                EmpSubmitedHours.Add(data);
            }
            return EmpSubmitedHours;
            }
        public ProjectInformation GetProjectList(int ProjectId)
        {
 
            var projectinfo = _projectDataAccess.GetProjectList(ProjectId);
            //projectinfo.TaskList = new List<string>();
            
            //foreach (var task in projectinfo.Task.Split(','))
            //{
            //    projectinfo.TaskList.Add(task);
            //}
            return projectinfo;

        }

        //public List<GetEmpSubmittedHour> GetEmpSubmitHour(int ProjectId)
        //{
        //    return _projectDataAccess.GetEmpSubmitHour(ProjectId);
        //}
        /**/
        public List<EmployeeTaskList> EmployeeTaskList(int TaskId, int userId)
        {
            return _projectDataAccess.EmployeeTaskList(TaskId,userId);
        }

        /* Get the All Project List Information*/
        public List<ProjectListInformation> ProjectListInfo(int domainId)
        {   
            var ProjectInfo = new List<ProjectListInformation>(domainId);
            var projectDetailInfo = _projectDataAccess.ProjectListInfo(domainId);
            foreach (var info1 in projectDetailInfo)
            {  
                var data = new ProjectListInformation();
                data = info1;
                data.TotalHourSpent = _projectDataAccess.TotalSpendHours(info1.ProjectId);
                data.CurrentMonthSpent = _projectDataAccess.CurrentMonthHoursInfo(info1.ProjectId);
                ProjectInfo.Add(data);
                
            }
            return ProjectInfo;
        }
        public List<KeyValuePair> EmployeeInformation()
        {
            return _projectDataAccess.EmployeeInformation();
        }
        public void Archeive(int projectId, int userId, bool isArcheive)
        {
            _projectDataAccess.Archeive(projectId,userId,isArcheive);
        }
        public List<ClientInfo> GetUserProjects()
        {
            return _projectDataAccess.GetUserProjects();
        }

}
}
