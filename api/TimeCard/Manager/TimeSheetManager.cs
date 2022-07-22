using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class TimeSheetManager : BaseManager, ITimeSheet
    {
        private readonly TimeSheetDataAccess _timeSheetDataAccess;
        public TimeSheetManager(TimeSheetDataAccess dataAccess)
        {
            _timeSheetDataAccess = dataAccess;
        }

        public bool AddTimeSheet(AddTimeSheetEntity timeSheet, int userId)
        {
            return _timeSheetDataAccess.AddTimeSheet(timeSheet,userId);
        }

        public bool DeleteTimeSheet(int timeSheetId)
        {
            return _timeSheetDataAccess.DeleteTimeSheet(timeSheetId);
        }

        public WeekData GetMonthHr(int userId)
        {
            return _timeSheetDataAccess.GetMonthHr(userId);
        }

        public List<TimeSheetAccess> GetTimeSheet(string date, int userId)
        {
            return _timeSheetDataAccess.GetTimeSheet(date,userId);
        }

        public WeekData GetWeekHr(int userId)
        {
            return _timeSheetDataAccess.GetWeekHr(userId);
        }

        public List<float> GetWorkHr(string date,int userId)
        {
            return _timeSheetDataAccess.GetWorkHr(date,userId);
        }

        // public bool UpdateTimeSheet(UpdateTimeSheetEntity updateTimeSheet,int timeSheetId)
        // {
        //     return _timeSheetDataAccess.UpdateTimeSheet(updateTimeSheet,timeSheetId);
        // }

        public List<TimeSheetReport> GetTimeSheetReport() 
        {
            var reportInfo = new List<TimeSheetReport>();
            var reportdetailInfo = _timeSheetDataAccess.GetTimeSheetReport();
            foreach (var reportdetail in reportdetailInfo)
            {
                var data = new TimeSheetReport();
                data.EmpDetailInfo = new List<ClientProjectInfo>();
                var  clientDetailInfo = _timeSheetDataAccess.clientProjectDetail(reportdetail.ClientId, new List<int>());

                data = reportdetail;
                data.TimeEntered = _timeSheetDataAccess.GetTotalHours(reportdetail.ClientId);
                data.EmpDetailInfo = clientDetailInfo;
                reportInfo.Add(data);
            }
            return reportInfo;
        }
        public List<TimeSheetReport> SearchReports(SearchDto data)
        {   
            //var reportInfo = new List<TimeSheetReport>();
            var reportdetailInfo = _timeSheetDataAccess.SearchReports(data);
            foreach (var reportdetail in reportdetailInfo)
            {
                //var info = new TimeSheetReport();
                //info.EmpDetailInfo = new List<ClientProjectInfo>();
                reportdetail.EmpDetailInfo = _timeSheetDataAccess.clientProjectDetail(reportdetail.ClientId, reportdetail.UserId);
                //info = reportdetail;
                //info.EmpDetailInfo = clientDetailInfo;
                reportdetail.TimeEntered = _timeSheetDataAccess.SearchTotalHours(reportdetail.ClientId);
                //reportInfo.Add(info);
            }
            return reportdetailInfo;
        }
        public List<ClientProjectInfo> clientProjectDetail(int clientid, int userId)
        {
            List<int> userIds = new List<int>();
            userIds.Add(userId);
            return _timeSheetDataAccess.clientProjectDetail(clientid, userIds);
        }
        public List<EmployeeProjectDetails> EmployeeProjectInfo(int projectId, List<int> userId)
        {
            var empProjectInfo = new List<EmployeeProjectDetails>();
            foreach( var info in _timeSheetDataAccess.EmployeeProjectInfo(projectId, userId))
            {
                var data = new EmployeeProjectDetails();
                data.EmpDetails = new List<EmpDetail>();

               var enterTimeInfo = _timeSheetDataAccess.EmployeeEnteredTaskList(info.TaskId,info.UserId);
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
       //public List<EmpDetail> EmployeeEnteredTaskList(int userId)
       // {
       //     return _timeSheetDataAccess.EmployeeEnteredTaskList(userId);
       // }
    }
}
