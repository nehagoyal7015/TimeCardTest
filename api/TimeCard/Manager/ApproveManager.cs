using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class ApproveManager : BaseManager, IApprove
    {
        private readonly ApproveDataAccess _approveDataAccess;

        public ApproveManager(ApproveDataAccess approveDataAccess)
        {
            _approveDataAccess = approveDataAccess;
        }

        
        public List<AppReport> SearchReport(AppSearchDto data)
        {
           
            var reportdetailInfo = new List<AppReport>();
            var empsearch = new ApproveSearchDto();
            foreach (var reportdetail in _approveDataAccess.SearchReport(data))
            {
                var empdt = new AppReport();
                empdt.EmpDetailInfo = new List<taskProject>();
                var taskproj = _approveDataAccess.TaskListProject(reportdetail.ProjectId);
                empdt = reportdetail;
                empdt.EmpDetailInfo = taskproj;


                /* var seemp = new AppReport();
                 seemp.ApproveReports = new List<ApproveReport>();
                 var empproj = _approveDataAccess.EmployeeDetail(empsearch, reportdetail.TaskId);
                 seemp = reportdetail;
                 seemp.ApproveReports = empproj;*/




                reportdetail.TimeEntered = _approveDataAccess.AppSearchTotalHours(reportdetail.ClientId);
                reportdetailInfo.Add(empdt);
                /*reportdetailInfo.Add(seemp);*/
            }

           
            return reportdetailInfo;
        }
        public List<ApprovalClientProjectInfo> AppClientProjectDetail(int clientid, int userId)
        {
            List<int> userIds = new List<int>();
            userIds.Add(userId);
            return _approveDataAccess.AppClientProjectDetail(clientid, userIds);
        }
       
        public List<EmpDetailApp> AppEmployeeEnteredTaskList(int taskId, int userId)
        {
            return _approveDataAccess.AppEmployeeEnteredTaskList(taskId, userId);
        }

        public List<taskProject> TaskListProject(int ProjectId)
        {
            
            var projectTaskInfo = new List<taskProject>();

            foreach (var info in _approveDataAccess.TaskListProject(ProjectId))
            {
                var searchtask = new ApproveSearchDto();
                var taskdata = new taskProject();
                taskdata.ApproveReports = new List<ApproveReport>();


                var taskListData = EmployeeDetail(searchtask, taskdata.TaskId);
                /*float Count = 0;
                foreach (var empBillhour in taskListData)
                {
                    Count = Count + empBillhour.BillableHours;
                }*/
                taskdata = info;
                taskdata.ApproveReports = taskListData;
                projectTaskInfo.Add(taskdata);

            }
            return projectTaskInfo;
        }

        public List<ApproveReport> EmployeeDetail(ApproveSearchDto data, int TaskId)
        {

            var reportdetailInfo = _approveDataAccess.EmployeeDetail(data, TaskId);
            var empval = new ApproveReport();
            {
                var empProjectInfo = new List<ApproveReport>();
                foreach (var info in _approveDataAccess.EmployeeDetail(data, TaskId))
                {

                    empval.EmpDetails = new List<EmpDetailApp>();

                    var enterTimeInfo = _approveDataAccess.AppEmployeeEnteredTaskList(info.TaskId, info.UserId);
                    float Count = 0;
                    foreach (var info1 in enterTimeInfo)
                    {
                        Count = Count + info1.BillableHours;
                    }
                    info.Total = Count;
                    empval = info;
                    empval.EmpDetails = enterTimeInfo;
                    empProjectInfo.Add(empval);
                }
                return empProjectInfo;
            }
            return reportdetailInfo;
        }


        public List<Clients1> GetRecentApprovals(int userId)
        {
            var taskInfo = _approveDataAccess.GetRecentApprovals(userId);
            if (taskInfo.Count == 1)
            {
                foreach (var info in taskInfo)
                {
                    if (info.Project.Count == 1)
                    {
                        info.ProjectId = info.Project.Select(s => s.ProjectId).FirstOrDefault();
                        foreach (var task in info.Project)
                        {
                            if (task.ProjectTask.Count == 1)
                            {
                                info.TaskId = task.ProjectTask.Select(s => s.TaskId).FirstOrDefault();
                            }
                        }

                    }
                }
            }
            return taskInfo;
        }

        /*public List<AppEmployeeProjectDetails> AppEmployeeProjectInfo(int taskId)
       {
           var empProjectInfo = new List<AppEmployeeProjectDetails>();
           foreach (var info in _approveDataAccess.AppEmployeeProjectInfo(taskId))
           {
               var data = new AppEmployeeProjectDetails();
               data.EmpDetails = new List<EmpDetailApp>();

               var enterTimeInfo = _approveDataAccess.AppEmployeeEnteredTaskList(info.TaskId, info.UserId);
               float Count = 0;
               foreach (var info1 in enterTimeInfo)
               {
                   Count = Count + info1.BillableHours;
               }
               info.Total = Count;
               data = info;
               data.EmpDetails = enterTimeInfo;
               empProjectInfo.Add(data);
           }
           return empProjectInfo;
       }
*/
    }
}
