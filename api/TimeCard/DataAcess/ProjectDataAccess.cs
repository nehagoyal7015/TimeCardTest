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
    public class ProjectDataAccess : BaseDataAccess
    {
        public ProjectDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }
        public void AddProject(ProjectEntity project, int userId)
        {
           
            var info = new Project();

            info.DomainId = project.DomainId;
            info.ClientId = project.ClientId;
            info.ManagerId = project.UserId;
            info.Name = project.Name;
            info.BudgetCost = project.BudgetCost;   
            info.BudgetHours = project.BudgetHours;
            info.StartDate = project.StartDate;
            info.EstimateCompletion = project.EstimateCompletion;
            info.CloseDate = project.CloseDate;
            info.IsActive = project.IsActive;  
            info.ActualBudget = project.ActualBudget;
            info.CreatedBy = userId;
            info.CreatedOn = DateTime.Now;
            info.ModifiedBy = userId;
            info.ModifiedOn = DateTime.Now;
            DataContext.Projects.Add(info);
            DataContext.SaveChanges();

            if (project.UserInfo.Count > 0)
            {
                foreach (var id in project.UserInfo)
                {
                    var projectuser = new ProjectUser();
                    projectuser.ProjectId = info.ProjectId;
                    projectuser.UserId = id.UserId;
                    projectuser.CreatedBy = userId;
                    projectuser.CreatedOn = DateTime.Now;
                    projectuser.ModifiedBy = userId;
                    projectuser.ModifiedOn = DateTime.Now;
                    DataContext.ProjectUsers.Add(projectuser);
                }
                DataContext.SaveChanges();
            }
            foreach (var task in project.TaskList)
            { 
                var taskInfo = new  ProjectTask();

                taskInfo.ProjectId = info.ProjectId;
                taskInfo.IsBillable = project.IsBillable;
                taskInfo.Name = task.Task;
                taskInfo.BudgetCost = task.TaskBudgetCost;
                taskInfo.BudgetHours = task.TaskBudgetHours;
                taskInfo.IsArchived = task.IsArchive;
                taskInfo.CreatedBy = userId;
                taskInfo.CreatedOn = DateTime.Now;
                taskInfo.ModifiedBy = userId;
                taskInfo.ModifiedOn = DateTime.Now;
                DataContext.ProjectTasks.Add(taskInfo);
            }
            DataContext.SaveChanges();

        }
        public void UpdateProject(ProjectEntity project,  int userId)
        {
            var infoProject = DataContext.Projects.Where(p => p.ProjectId == project.ProjectId).Select(p => p).FirstOrDefault();
            if (infoProject != null)
            {
                infoProject.ManagerId = project.UserId;
                infoProject.Name = project.Name;
                infoProject.Description = project.Description;
                infoProject.BudgetCost = project.BudgetCost;
                infoProject.BudgetHours = project.BudgetHours;
                infoProject.StartDate = project.StartDate;
                infoProject.EstimateCompletion = project.EstimateCompletion;
                infoProject.CloseDate = project.CloseDate;
                infoProject.IsActive = project.IsActive;
                infoProject.ModifiedBy = userId;
                infoProject.ModifiedOn = DateTime.Now;
            }

            var existingUserIdList = DataContext.ProjectUsers.Where(pu => pu.ProjectId == project.ProjectId).Select(s => s.UserId).ToArray();
            var newUserIdList = project.UserInfo.Select(s => s.UserId).ToArray();
            var newIds = newUserIdList.Except(existingUserIdList).ToList();
            var removeIdList = existingUserIdList.Except(newUserIdList).ToList();

            if (removeIdList.Count > 0)
            {
                foreach (var id in removeIdList)
                {
                    var info = DataContext.ProjectUsers.Where(pu => pu.UserId == id).Select(pu => pu).FirstOrDefault();
                    DataContext.ProjectUsers.Remove(info);
                    DataContext.SaveChanges();
                }
            }


            if (newIds.Count > 0)
                {
                    foreach (var id in newIds)
                    {
                        var projectuser = new ProjectUser();
                        projectuser.ProjectId = infoProject.ProjectId;
                        projectuser.UserId = id;
                        projectuser.CreatedBy = userId;
                        projectuser.CreatedOn = DateTime.Now;
                        projectuser.ModifiedBy = userId;
                        projectuser.ModifiedOn = DateTime.Now;
                        DataContext.ProjectUsers.Add(projectuser);
                    }
                }
          

            var existingTaskList = (from pt in DataContext.ProjectTasks
                                    where pt.ProjectId == project.ProjectId
                                    select new 
                                    {
                                        TaskId = pt.TaskId,
                                        Task = pt.Name,
                                        TaskBudgetCost = pt.BudgetCost,
                                        TaskBudgetHours = pt.BudgetHours,
                                        IsArchive = pt.IsArchived
                                    }).ToList();
            var newTaskList = project.TaskList.Select(s => new { s.TaskId, s.Task, s.TaskBudgetCost, s.TaskBudgetHours,s.IsArchive}).ToList();
            var newTasklst = newTaskList.Except(existingTaskList).ToList();
            var removeTaskList = (existingTaskList.Select(s => s.TaskId).ToList()).Except(newTaskList.Select(s => s.TaskId).ToList()).ToList();


            if (removeTaskList.Count > 0)
            {
                foreach (var task in removeTaskList)
                {
                    var info = DataContext.ProjectTasks.Where(pt => pt.TaskId == task).Select(pt =>pt).FirstOrDefault();
                    DataContext.ProjectTasks.Remove(info);
                    DataContext.SaveChanges();
                        
                }
            }
            if (newTasklst.Count > 0)
            {
                foreach (var task in newTasklst)
                {
                    var taskdetails = DataContext.ProjectTasks.Where(x => x.TaskId == task.TaskId).FirstOrDefault();

                    if(taskdetails != null)
                    {
                        taskdetails.Name = task.Task;
                        taskdetails.BudgetHours = task.TaskBudgetHours;
                        taskdetails.BudgetCost = task.TaskBudgetCost;
                        taskdetails.IsArchived = task.IsArchive;
                        taskdetails.ModifiedBy = userId;
                        taskdetails.ModifiedOn = DateTime.Now;
                    }
                    else
                    {
                        var taskInfo = new ProjectTask();

                        taskInfo.ProjectId = infoProject.ProjectId;
                        taskInfo.IsBillable = project.IsBillable;
                        taskInfo.Name = task.Task;
                        taskInfo.BudgetCost = task.TaskBudgetCost;
                        taskInfo.BudgetHours = task.TaskBudgetHours;
                        taskInfo.IsArchived = task.IsArchive;
                        taskInfo.CreatedBy = userId;
                        taskInfo.CreatedOn = DateTime.Now;
                        taskInfo.ModifiedBy = userId;
                        taskInfo.ModifiedOn = DateTime.Now;
                        DataContext.ProjectTasks.Add(taskInfo);
                    }
                    
                }
            }

            DataContext.SaveChanges();
        }

        public List<ProjectList> GetProject()
        {
            var pojectInfo = (from p in DataContext.Projects                             
                              select new ProjectList
                              {
                                  ProjectId = p.ProjectId,
                                  Name = p.Name,
                                  BudgetHours = p.BudgetHours,
                                  OwnerShips = p.OwnerShips
                              }).ToList();

            return pojectInfo;

        }
        public List<KeyValuePair> GetEmployee()
        {
            var empInfo = (from u in DataContext.UserAccounts
                           join ud in DataContext.UserDetails on u.UserId equals ud.UserId
                                where u.IsActive == true
                              select new KeyValuePair
                              {
                                 Key = u.UserId,
                                 Value = u.IsActive,
                                 Description = u.UserName,
                              }).ToList();
          
            return empInfo;
        }


        // Get  the  ProjectList
        public ProjectInformation  GetProjectList(int projectId)
        {
            var empInfo1 = new ProjectInformation();
             empInfo1 = (from pr in DataContext.Projects 
                            where pr.ProjectId == projectId
                            select new ProjectInformation
                            {
                                UserId = pr.ManagerId,
                                Name = pr.Name,
                                Description = pr.Description,
                                BudgetCost = pr.BudgetCost,
                                BudgetHours = pr.BudgetHours,
                                StartDate = pr.StartDate,
                                EstimateCompletion = pr.EstimateCompletion,
                                CloseDate = pr.CloseDate,
                                IsActive = pr.IsActive,
                                IsArchive = pr.IsArchive,
                                ActualBudget = pr.ActualBudget,
                               
                            }).FirstOrDefault();
            
            if(empInfo1 != null)
            {
                empInfo1.TaskList = (from pt in DataContext.ProjectTasks
                                     where pt.ProjectId == projectId
                                     select new TaskListInfo
                                     {
                                         TaskId = pt.TaskId,
                                         Task = pt.Name,
                                         TaskBudgetCost = pt.BudgetCost,
                                         TaskBudgetHours = pt.BudgetHours,
                                         IsArchive = pt.IsArchived
                                     }).ToList();
            }
            empInfo1.UserInfo = (from pu in DataContext.ProjectUsers
                                         where pu.ProjectId == projectId
                                         select new GetEmployeeList
                                         {
                                             UserId = pu.UserId
                                         }).ToList();
            return empInfo1;
           
        }
      

        public List<ProjectListInformation> ProjectListInfo(int domainId) 
        {
            var projectListInfo = new List<ProjectListInformation>();
            var month = DateTime.Now.Month;
            var projList = (from p in DataContext.Projects
                            join c in DataContext.Clients on p.ClientId equals c.ClientId
                            join t in DataContext.TimeSheets on p.ProjectId equals t.ProjectId
                            join u in DataContext.UserAccounts on t.UserId equals u.UserId
                            where u.DomainId == domainId 
                            select new ProjectListInformation
                            {
                                ProjectId = p.ProjectId,
                                ProjectName = p.Name,
                                ClientName = c.ClientName,
                                AsignTo = u.UserName,
                                Managar = p.OwnerShips,
                                BillableHours = t.BillableHours
                            }).ToList();
            
            foreach (var info in projList.GroupBy(g => new { g.AsignTo,g.ClientName,g.ProjectName,g.Managar})) 
            {
                var projectInformation = new ProjectListInformation();
                projectInformation.ProjectId = info.Select(s => s.ProjectId).FirstOrDefault();
                projectInformation.ProjectName = info.Select(s => s.ProjectName).FirstOrDefault();
                projectInformation.ClientName = info.Select(s => s.ClientName).FirstOrDefault();
                projectInformation.AsignTo = info.Select(s => s.AsignTo).FirstOrDefault();
                projectInformation.Managar = info.Select(s => s.Managar).FirstOrDefault();
                projectInformation.BillableHours = info.Sum(s => s.BillableHours);
                projectListInfo.Add(projectInformation);
            }
            
            return projectListInfo;
           
        }
       
        public float CurrentMonthHoursInfo(int projectId)
        {
            var month = DateTime.Now.Month;
            var CurrentMonthHours = (from t in DataContext.TimeSheets 
                                    where t.ProjectId == projectId && t.RequestDate.Month == month
                                    select new
                                    {
                                        CurrentMonthSpent = t.BillableHours
                                    }).Sum(s => s.CurrentMonthSpent);
            return CurrentMonthHours;
        }
        public float TotalSpendHours(int projectId)
        {
            var totalHours = (from t in DataContext.TimeSheets 
                                    where t.ProjectId == projectId
                                    select new
                                    {
                                        Hours = t.BillableHours
                                    }).Sum(s => s.Hours);
            return totalHours;
        }
        public List<TaskList> GetProjedctDetail(int ProjectId)
        {
            var Projectinfo = (from p in DataContext.Projects
                               where p.ProjectId == ProjectId
                               select new TaskList
                               {
                                   ProjectId = p.ProjectId,
                                   OwnerShips = p.OwnerShips,
                                   CreatedOn = p.CreatedOn,
                               }).ToList();
            return Projectinfo;
        }
        public List<projectTaskList> GetProjectTask(int ProjectId)
        {
            var projectTaskInfo = new List<projectTaskList>();
            var taskInfo = (from pu in DataContext.ProjectUsers
                            join t in DataContext.TimeSheets on pu.UserId equals t.UserId
                            join pt in DataContext.ProjectTasks on t.TaskId equals pt.TaskId
                            join p in DataContext.Projects on t.ProjectId equals p.ProjectId
                            where t.ProjectId == ProjectId
                            select new
                            {
                                ProjectId = p.ProjectId,
                                UserId = t.UserId,
                                TaskId = t.TaskId,
                                Task = pt.Name,
                                BudgetHours = p.BudgetHours
                            }).ToList();
            foreach (var info in taskInfo.GroupBy(g => new { g.Task, g.BudgetHours, g.UserId }))
            {
                var projectTask = new projectTaskList();
                projectTask.userId = info.Select(s => s.UserId).FirstOrDefault();  
                projectTask.ProjectId = info.Select(s => s.ProjectId).FirstOrDefault();   
                projectTask.TaskId = info.Select(s => s.TaskId).FirstOrDefault();
                projectTask.Task = info.Select(s => s.Task).FirstOrDefault();
                projectTask.BudgetHours = info.Select(s => s.BudgetHours).FirstOrDefault();
                projectTaskInfo.Add(projectTask);
            }
            return projectTaskInfo;

        }
        public List<GetEmpSubmittedHour> GetEmpSubmitHour(int userId , int TaskId)
        {
            var hoursInfo = new List<GetEmpSubmittedHour>();
            var submitedHours = (from t in DataContext.TimeSheets
                                 join pt in DataContext.ProjectTasks on t.TaskId equals pt.TaskId
                                 join u in DataContext.UserAccounts on t.UserId equals u.UserId
                                 join p in DataContext.Projects on t.ProjectId equals p.ProjectId
                                 where t.UserId == userId && t.TaskId == TaskId
                                 select new
                                 {
                                     UserId = t.UserId,
                                     TaskId = t.TaskId,
                                     UserName = u.UserName,
                                     Hours = t.BillableHours,
                                     LastTimeEntered = t.ModifiedOn,
                                     Total = p.BudgetHours,
                                 }).ToList();
            foreach (var info in submitedHours.GroupBy(g => new { g.TaskId, g.UserId, g.UserName }))
            {
                var hoursDetails = new GetEmpSubmittedHour();
                hoursDetails.UserId = info.Select(s => s.UserId).FirstOrDefault();
                hoursDetails.TaskId = info.Select(s => s.TaskId).FirstOrDefault();
                hoursDetails.UserName = info.Select(s => s.UserName).FirstOrDefault();
                hoursDetails.LastTimeEntered = info.Select(s => s.LastTimeEntered).FirstOrDefault();
                hoursDetails.Total = info.Select(s => s.Total).FirstOrDefault();
                hoursDetails.Hours = info.Sum(s => s.Hours);
                hoursInfo.Add(hoursDetails);
            }
            return hoursInfo;
        }

        public List<EmployeeTaskList> EmployeeTaskList(int TaskId,int userId)
        {
            var EmpInfo = (from t in DataContext.TimeSheets
                           join p in DataContext.Projects on t.ProjectId equals p.ProjectId
                           orderby t.RequestDate descending
                           where t.TaskId == TaskId && t.UserId == userId 
                           select new EmployeeTaskList
                           {
                               TaskId = t.TaskId,
                               TaskDate = t.RequestDate,
                               BillableHours = t.BillableHours,
                               BillableHoursNote = t.BillableHoursNote
                           }).ToList();

            return EmpInfo;
        }
        public List<KeyValuePair> EmployeeInformation()
        {
            var EmpInformation = ( from u in DataContext.UserAccounts
                                   join ud in DataContext.UserDetails on u.UserId equals ud.UserId
                                   select new KeyValuePair
                                   {
                                       Key = ud.UserId,
                                       Description = u.UserName
                                   }).ToList();

            return EmpInformation;
        }

        public void Archeive(int projectId, int userId, bool isArcheive)
        {
            var archeiveInfo = DataContext.ProjectTasks.FirstOrDefault(p => p.ProjectId == projectId);
            if (archeiveInfo != null)
            {
                archeiveInfo.IsArchived = isArcheive;
                archeiveInfo.ModifiedBy = userId;
                archeiveInfo.ModifiedOn = DateTime.Now;
                DataContext.SaveChanges();
            }
        }

        public List<ClientInfo> GetUserProjects()
        {
            var projectInfo = (from projuser in DataContext.ProjectUsers
                               join project in DataContext.Projects on projuser.ProjectId equals project.ProjectId
                               join client in DataContext.Clients on project.ClientId equals client.ClientId
                               select new
                               {
                                   ClientId = client.ClientId,
                                   ClientName = client.ClientName,
                                   ProjectId = project.ProjectId,
                                   ProjectName = project.Name,
                                   Checked = false,
                               }).ToList();
            // Itrate for the client 

            var clients = new List<ClientInfo>();
            foreach (var clientrec in projectInfo.ToList().GroupBy(s => new { s.ClientId, s.ClientName }))
            {

                var client = new ClientInfo() { ClientId = clientrec.Key.ClientId, ClientName = clientrec.Key.ClientName };
                client.ProjectData = new List<ProjectInfoList>();
                foreach (var prjRec in clientrec.ToList().GroupBy(s => new { s.ProjectId, s.ProjectName }))
                {
                    var project = new ProjectInfoList() { ProjectId = prjRec.Key.ProjectId, ProjectName = prjRec.Key.ProjectName, isSelected = false };

                    // may be need to intiaizre Projects object before add a record to it.

                    client.ProjectData.Add(project);
                }
                clients.Add(client);
            }
            return clients;

        }
    }
}

