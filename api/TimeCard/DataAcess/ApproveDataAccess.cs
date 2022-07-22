using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.DataAcess;
using TimeCard.Interface;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class ApproveDataAccess : BaseDataAccess
    {

        public ApproveDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {

        }

        public List<AppReport> SearchReport(AppSearchDto data)
        {
            var splitedDate = data.currentDate.Split('-');
            if (splitedDate.Length > 1)
            {
                var startdate = new DateTime(Convert.ToInt32(splitedDate[1]), Convert.ToInt32(splitedDate[0]), 1);
                data.Startdate = startdate;
                data.Enddate = startdate.AddMonths(1).AddDays(-1);
            }
            var approvedetails = new List<AppReport>();
            if (data.UserId == 0)
            {
                data.UserId = null;
            }
            var timereportInfo = (from t in DataContext.TimeSheets
                                  join p in DataContext.Projects on t.ProjectId equals p.ProjectId
                                  join c in DataContext.Clients on p.ClientId equals c.ClientId
                                 /* join pt in DataContext.ProjectTasks on t.TaskId equals pt.TaskId*/
                                  where (data.ProjectIds.Count == 0 || data.ProjectIds.Contains(t.ProjectId))
                                   && (data.UserId == null || t.UserId == data.UserId)

                                  && ((!data.Startdate.HasValue && !data.Enddate.HasValue) || (t.RequestDate >= data.Startdate && t.RequestDate <= data.Enddate))
                                  && t.IsBilled == data.Billable
                                  select new
                                  {
                                      UserId = t.UserId,
                                      ClientId = c.ClientId,
                                      ClientName = c.ClientName,
                                      IsBillable = t.IsBilled,
                                      ProjectId =t.ProjectId,
                                      ProjectName = p.Name,
                                        /*  TaskId =pt.TaskId,
                                          TaskName =pt.Name,*/
                                      IsExpanded = false
                                  }).Distinct().ToList();
            foreach (var info in timereportInfo.GroupBy(g => new { g.ClientName, g.ClientId, g. ProjectId,g.ProjectName}))
            {
                var timesheetinfo = new AppReport();
                timesheetinfo.EmpDetailInfo = new List<taskProject>();
                var lst = info.ToList();
                timesheetinfo.ClientId = info.Key.ClientId;
                timesheetinfo.ClientName = info.Key.ClientName;
                timesheetinfo.ProjectId = info.Key.ProjectId;
                timesheetinfo.ProjectName = info.Key.ProjectName;
                /*timesheetinfo.TaskName = info.Key.TaskName;*/
                timesheetinfo.Project = DataContext.Projects.Where(u => u.ClientId == timesheetinfo.ClientId).Select(u => u.ProjectId).Count();

                timesheetinfo.UserId = new List<int>();
                foreach (var rec in lst)
                {
                    timesheetinfo.UserId.Add(rec.UserId);
                }

                approvedetails.Add(timesheetinfo);
            }
            return approvedetails;

        }

       

        public List<ApprovalClientProjectInfo> AppClientProjectDetail(int clientId, List<int> userId)
        {
            var clientList = new List<ApprovalClientProjectInfo>();
            var client = (from c in DataContext.Clients
                          join p in DataContext.Projects on c.ClientId equals p.ClientId
                          join t in DataContext.TimeSheets on p.ProjectId equals t.ProjectId
                          where c.ClientId == clientId && (userId.Count == 0 || userId.Contains(t.UserId))
                          select new
                          {
                              UserId = t.UserId,
                              ProjectId = p.ProjectId,
                              ProjectName = p.Name
                          }).Distinct().ToList();

            foreach (var info in client.GroupBy(g => new { g.ProjectId, g.ProjectName }))
            {
                var clientInfo = new ApprovalClientProjectInfo();
                clientInfo.ProjectId = info.Key.ProjectId;
                clientInfo.ProjectName = info.Key.ProjectName;
                clientInfo.UserId = new List<int>();
                foreach (var rec in info.ToList())
                {
                    clientInfo.UserId.Add(rec.UserId);
                }
                clientList.Add(clientInfo);
            }

            foreach (var info in clientList)
            {
                info.Total = (from p in DataContext.Projects
                              join t in DataContext.TimeSheets on p.ProjectId equals t.ProjectId
                              where t.ProjectId == info.ProjectId && (info.UserId.Count == 0 || info.UserId.Contains(t.UserId))
                              select new
                              {
                                  Total = t.BillableHours
                              }).Sum(s => s.Total);
            }
            return clientList;
        }


        public List<ApproveReport> EmployeeDetail(ApproveSearchDto data ,int TaskId)
        {

            var approvedetails = new List<ApproveReport>();

            var clientInfo = (from cl in DataContext.Clients
                              join prj in DataContext.Projects on cl.ClientId equals prj.ClientId
                              join prjuser in DataContext.ProjectUsers on prj.ProjectId equals prjuser.ProjectId
                              join prjtask in DataContext.ProjectTasks on prjuser.ProjectId equals prjtask.ProjectId
                              join user in DataContext.UserAccounts on prjuser.UserId equals user.UserId
                              where (data.ProjectIds.Count == 0 || data.ProjectIds.Contains(prjuser.ProjectId)) 
                              /*&&
                              prjtask.TaskId == TaskId
*/

                              /*  where prjuser.ProjectId == data.ProjectId */
                              select new
                              {
                                  UserId = user.UserId,
                                  UserName = user.UserName,
                                 /* TaskId = prjtask.TaskId*/


                              }).Distinct().ToList();
            foreach (var info in clientInfo)
            {
                var approveinfo = new ApproveReport();



                approveinfo.UserName = info.UserName;



                approvedetails.Add(approveinfo);
            }
            return approvedetails;

        }
       
        public List<EmpDetailApp> AppEmployeeEnteredTaskList(int taskId, int userId)
        {
            var TaskInfo = (from t in DataContext.TimeSheets
                            join u in DataContext.UserAccounts on t.UserId equals u.UserId
                            where t.UserId == userId && t.TaskId == taskId
                            select new EmpDetailApp
                            {
                                Date = t.RequestDate,
                                BillableHours = t.BillableHours,
                                NonBillableHours = t.NonBillableHours,
                               /* BillableNotes = t.BillableHoursNote,
                                NonBillableHoursNotes = t.NonBillableHoursNote*/
                            }).ToList();

            float Count = 0;
            foreach (var info in TaskInfo)
            {
                Count = info.BillableHours + (float)(info.NonBillableHours != null ? info.NonBillableHours : 0);
                info.Total = Count;
            }

            return TaskInfo;
        }

        public List<taskProject> TaskListProject(int ProjectId)
        {
            var projectTaskList = new List<taskProject>();
            var task = ( from  p in DataContext.Projects
                        join pt in DataContext.ProjectTasks on p.ProjectId equals pt.ProjectId
                        join t in DataContext.TimeSheets on pt.ProjectId equals t.ProjectId
                        where p.ProjectId == ProjectId

                         select new taskProject
                        {
                            TaskId = pt.TaskId,
                            UserId = t.UserId,
                            TaskName = pt.Name,
                            LastSubmittedOn = pt.ModifiedOn,
                            IsExpanded = false,
                        }).Distinct().ToList();

            foreach (var info in task.GroupBy(g => new { g.TaskId, g.TaskName }))
            {
                var projectInfo = new taskProject();
                projectInfo.UserId = info.Select(s => s.UserId).FirstOrDefault();
                projectInfo.TaskId = info.Select(s => s.TaskId).FirstOrDefault();
                projectInfo.TaskName = info.Select(s => s.TaskName).FirstOrDefault();
                projectInfo.LastSubmittedOn = info.Select(s => s.LastSubmittedOn).FirstOrDefault();

                projectInfo.TaskTotal = DataContext.ProjectTasks.Where(u => u.TaskId == projectInfo.TaskId).Select(u => u.TaskId).Count();
                projectTaskList.Add(projectInfo);
            }

            foreach (var taskCount in projectTaskList)
            {
                taskCount.TaskTotal = (from p in DataContext.Projects
                                       join t in DataContext.TimeSheets on p.ProjectId equals t.ProjectId
                                       where t.TaskId == taskCount.TaskId
                                       select new
                                       {
                                           projectTotal = t.BillableHours
                                       }).Sum(s => s.projectTotal);
            }
            return projectTaskList;
        }






        public List<Clients1> GetRecentApprovals(int userId)
        {
            var projectInfo = (from approve in DataContext.TimeSheets
                               join project in DataContext.Projects on approve.ProjectId equals project.ProjectId
                               join client in DataContext.Clients on project.ClientId equals client.ClientId
                               join user in DataContext.UserAccounts on approve.UserId equals user.UserId
                               join projtask in DataContext.ProjectTasks on project.ProjectId equals projtask.ProjectId
                               
                               select new
                               {
                                   ClientId = client.ClientId,
                                   ClientName = client.ClientName,
                                   ProjectId = project.ProjectId,
                                   ProjectName = project.Name,
                                   UserId = approve.UserId,
                                   BillHours = approve.BillableHours,
                                   NonBillHours = approve.NonBillableHours,
                                   UserName = user.UserName,
                                   TaskId = projtask.TaskId,
                                   TaskName = projtask.Name,
                                   IsArchived = projtask.IsArchived,


                               }).ToList();


            // Itrate for the client 

            var clients1 = new List<Clients1>();
            foreach (var clientrec in projectInfo.ToList().GroupBy(s => new { s.ClientId, s.ClientName ,s.UserName}))
            {

                var client = new Clients1() { ClientId = clientrec.Key.ClientId, ClientName = clientrec.Key.ClientName, UserName =clientrec.Key. UserName};

                client.Project = new List<Projects1>();
               
                foreach (var prjRec in clientrec.ToList().GroupBy(s => new { s.ProjectId, s.ProjectName, s.TaskName,  s.BillHours, s.NonBillHours}))
                {
                    var project1 = new Projects1() { ProjectId = prjRec.Key.ProjectId, ProjectName = prjRec.Key.ProjectName, 
                        BillHours = prjRec.Key.BillHours, 
                        NonBillHours = prjRec.Key.NonBillHours,
                        TaskName =prjRec.Key.TaskName
                    };
                    project1.ProjectTask = new List<ProjectTasks>();
                    client.Project.Add(project1);
                }
                clients1.Add(client);
            }
            return clients1;

        }

        public float GetApproveTotalHours(int ClientId)
        {
            var totalSenthour = (from t in DataContext.TimeSheets
                                 join p in DataContext.Projects on t.ProjectId equals p.ProjectId

                                 where p.ClientId == ClientId
                                 select new
                                 {
                                     totalhours = t.BillableHours
                                 }).Sum(s => s.totalhours);
            return totalSenthour;
        }


        public float AppSearchTotalHours(int ClientId)
        {
            var totalSenthour = (from t in DataContext.TimeSheets
                                 join p in DataContext.Projects on t.ProjectId equals p.ProjectId
                                 where p.ClientId == ClientId
                                 select new
                                 {
                                     totalhours = t.BillableHours
                                 }).Sum(s => s.totalhours);
            return totalSenthour;
        }


        /* public List<AppEmployeeProjectDetails> AppEmployeeProjectInfo(int taskId)
        {
            var employeeDetail = new List<AppEmployeeProjectDetails>();
            var Empinfo = (from p in DataContext.Projects
                           join t in DataContext.TimeSheets on p.ProjectId equals t.ProjectId
                         *//*  join ud in DataContext.UserDetails on t.UserId equals ud.UserId*//*
                           join u in DataContext.UserAccounts on t.UserId equals u.UserId
                           *//* where (t.ProjectId == projectId && projectId != 0) && userId.Contains(t.UserId)*//*
                           where t.TaskId == taskId
                           select new AppEmployeeProjectDetails
                           {
                               UserId = t.UserId,
                               TaskId = t.TaskId,
                               EmployeeName = u.UserName,
                               LastSubmittedOn = t.ModifiedOn,
                               IsExpanded = false
                           }).ToList();

            foreach (var info in Empinfo.GroupBy(g => new { g.UserId, g.TaskId }))
            {
                var empprojInfo = new AppEmployeeProjectDetails();

                empprojInfo.UserId = info.Select(s => s.UserId).FirstOrDefault();
                empprojInfo.TaskId = info.Select(s => s.TaskId).FirstOrDefault();
                empprojInfo.EmployeeName = info.Select(s => s.EmployeeName).FirstOrDefault();
                empprojInfo.LastSubmittedOn = info.Select(s => s.LastSubmittedOn).FirstOrDefault();
                employeeDetail.Add(empprojInfo);
            }
           *//* foreach (var projectcount in employeeDetail)
            {
                projectcount.projectTotal = (from p in DataContext.Projects
                                             join t in DataContext.TimeSheets on p.ProjectId equals t.ProjectId
                                             where t.ProjectId == projectId
                                             select new
                                             {
                                                 projectTotal = t.BillableHours
                                             }).Sum(s => s.projectTotal);
            }*//*
            return employeeDetail;

        }*/

    }
}

