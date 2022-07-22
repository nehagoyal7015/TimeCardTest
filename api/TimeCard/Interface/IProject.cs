using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Models;
using KeyValuePair = TimeCard.Common.KeyValuePair;

namespace TimeCard.Interface
{
    public interface IProject
    {
        void AddProject(ProjectEntity project, int userId);
        void UpdateProject(ProjectEntity project,int userId); 
        List<ProjectList> GetProject();
        List<KeyValuePair> GetEmployee();
        List<TaskList> GetProjedctDetail(int ProjectId);
        List<projectTaskList> GetProjectTask(int ProjectId);
        ProjectInformation GetProjectList(int projectId);
        //List<GetEmpSubmittedHour> GetEmpSubmitHour(int ProjectId);
        List<EmployeeTaskList> EmployeeTaskList(int TaskId, int userId);
        List<ProjectListInformation> ProjectListInfo(int domainId);
        List<KeyValuePair> EmployeeInformation();
        void Archeive(int projectId, int userId, bool isArcheive);
        List<ClientInfo> GetUserProjects();
    }
}
