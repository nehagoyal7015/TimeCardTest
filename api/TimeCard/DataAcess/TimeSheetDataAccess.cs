
using System;
using System.Collections.Generic;
using System.Linq;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class TimeSheetDataAccess : BaseDataAccess
    {
        public TimeSheetDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        public bool AddTimeSheet(AddTimeSheetEntity timeSheet, int userId)
        {

            var info = DataContext.TimeSheets.FirstOrDefault(u => u.TimeSheetId == timeSheet.TimeSheetId);
            if (info != null)
            {
                DataContext.TimeSheets.Remove(info);
                DataContext.SaveChanges();
            }
            var timeInfo = new TimeSheet();
            timeInfo.UserId = userId;
            timeInfo.ProjectId = timeSheet.ProjectId;
            timeInfo.TaskId = timeSheet.TaskId;
            timeInfo.RequestDate = timeSheet.RequestDate;
            timeInfo.BillableHours = timeSheet.BillableHours;
            timeInfo.BillableHoursNote = timeSheet.BillableHoursNote;
            timeInfo.NonBillableHours = timeSheet.NonBillableHours;
            timeInfo.NonBillableHoursNote = timeSheet.NonBillableHoursNote;
            // timeInfo.StartTime = timeSheet.StartTime;
            // timeInfo.EndTime = timeSheet.EndTime;
            timeInfo.IsBilled = true;
            timeInfo.CreatedBy = userId;
            timeInfo.CreatedOn = DateTime.Now;
            timeInfo.ModifiedBy = userId;
            timeInfo.ModifiedOn = DateTime.Now;

            DataContext.TimeSheets.Add(timeInfo);
            if (DataContext.SaveChanges() == 1)
                return true;
            else
                return false;

        }



        public List<TimeSheetAccess> GetTimeSheet(string date, int userId)
        {
            var dt = Convert.ToDateTime(date).Date;
            var timeSheet = (from timesheetRec in DataContext.TimeSheets
                             join projtask in DataContext.ProjectTasks on timesheetRec.TaskId equals projtask.TaskId
                             join proj in DataContext.Projects on timesheetRec.ProjectId equals proj.ProjectId
                             where timesheetRec.RequestDate.Date == dt
                             where timesheetRec.UserId == userId
                             select new TimeSheetAccess

                             {
                                 TimeSheetId = timesheetRec.TimeSheetId,
                                 ProjectId = timesheetRec.ProjectId,
                                 ProjectName = proj.Name,
                                 TaskId = timesheetRec.TaskId,
                                 TaskName = projtask.Name,
                                 BillableHours = timesheetRec.BillableHours,
                                 BillableHoursNote = timesheetRec.BillableHoursNote,
                                 NonBillableHours = timesheetRec.NonBillableHours,
                                 NonBillableHoursNote = timesheetRec.NonBillableHoursNote,
                                 IsBilled = timesheetRec.IsBilled
                             }).ToList();

            return timeSheet;
        }

        public bool DeleteTimeSheet(int timeSheetId)
        {
            var info = DataContext.TimeSheets.FirstOrDefault(u => u.TimeSheetId == timeSheetId);
            if (info != null)
            {
                DataContext.TimeSheets.Remove(info);
                DataContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<float> GetWorkHr(string date, int userId)
        {
            var whr = new List<float>();
            for (int i = 0; i < 7; i++)
            {
                var cdt = Convert.ToDateTime(date);
                var dt = cdt.AddDays(i - 3);
                var hr = (from timesheetRec in DataContext.TimeSheets
                          where timesheetRec.RequestDate == dt
                          where timesheetRec.UserId == userId
                          select new
                          {
                              WorkinHours = timesheetRec.BillableHours
                          }).ToList();
                float count = 0;
                foreach (var item in hr)
                {
                    count = item.WorkinHours + count;
                }
                whr.Add(count);
            }
            return whr;
        }

        public WeekData GetMonthHr(int userId)
        {
            var month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            var hr = (from timesheetRec in DataContext.TimeSheets
                      where timesheetRec.RequestDate.Month == month
                      where timesheetRec.RequestDate.Year == year
                      where timesheetRec.UserId == userId
                      select new
                      {
                          WorkinHours = timesheetRec.BillableHours
                      }).ToList();

            float workingHrsOfMonth = 0;
            foreach (var item in hr)
            {
                workingHrsOfMonth = item.WorkinHours + workingHrsOfMonth;
            }

            var data = new WeekData();
            data.workedHrs = workingHrsOfMonth;
            var daysInAMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var workingDays = Enumerable.Range(1, daysInAMonth)
                 .Select(i => new DateTime(DateTime.Now.Year, DateTime.Now.Month, i))
                 .Count(i => i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday);

            data.totalWorkHr = workingDays * 8;
            return data;

        }

        public WeekData GetWeekHr(int userId)
        {
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;
            int daysTillCurrentDay = currentDay - DayOfWeek.Sunday;
            DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);
            DateTime currentWeekEndDate = DateTime.Now.AddDays(7);
            var hr = (from timesheetRec in DataContext.TimeSheets
                      where timesheetRec.RequestDate >= currentWeekStartDate && timesheetRec.RequestDate <= currentWeekEndDate
                      where timesheetRec.UserId == userId
                      select new
                      {
                          WorkinHours = timesheetRec.BillableHours
                      }).ToList();

            float workingHrsOfWeek = 0;
            foreach (var item in hr)
            {
                workingHrsOfWeek = item.WorkinHours + workingHrsOfWeek;

            }

            // var holidays = (from holidayRec in DataContext.Holidays
            //                 where holidayRec.HolidayDate >= currentWeekStartDate && holidayRec.HolidayDate <= currentWeekEndDate
            //                 where holidayRec.IsFloating == false
            //                 select new
            //                 {
            //                     holidayHours = holidayRec.HolidayDate
            //                 }).ToList();
            // var holidayHrsOfWeek = 0;
            // foreach (var item in holidays)
            // {
            //     if (item.holidayHours.DayOfWeek != DayOfWeek.Saturday || item.holidayHours.DayOfWeek != DayOfWeek.Sunday)
            //     {
            //         holidayHrsOfWeek = holidayHrsOfWeek + 1;
            //     }

            // }
            // holidayHrsOfWeek = holidayHrsOfWeek * 8;
            // var totalWorkingHrsOfWeek = 40 - holidayHrsOfWeek;
            var data = new WeekData();
            data.workedHrs = workingHrsOfWeek;
            data.totalWorkHr = 40;
            return data;
        }


        public List<TimeSheetReport> GetTimeSheetReport()
        {    
            var timeSheetdetails = new List<TimeSheetReport>();
            var timereportInfo = (from c in DataContext.Clients
                                  from p in DataContext.Projects
                                  select new TimeSheetReport
                                  {
                                      ClientId = c.ClientId,
                                      ClientName = c.ClientName,
                                      IsExpanded = false
                                  }).ToList();
     

            foreach (var info in timereportInfo.GroupBy(g => new { g.ClientName, g.ClientId }))
            {
                var timesheetinfo = new TimeSheetReport();

                timesheetinfo.ClientId = info.Select(s => s.ClientId).FirstOrDefault();
                timesheetinfo.ClientName = info.Select(s => s.ClientName).FirstOrDefault();
                timesheetinfo.Project = DataContext.Projects.Where(u => u.ClientId == timesheetinfo.ClientId).Select(u => u.ProjectId).Count();
                timeSheetdetails.Add(timesheetinfo);
            }
            return timeSheetdetails;

        }

        public float GetTotalHours(int ClientId)
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
        public List<TimeSheetReport> SearchReports(SearchDto data)
        {
            var splitedDate = data.currentDate.Split('-');
            if(splitedDate.Length > 1)
            {
                var startdate = new DateTime(Convert.ToInt32(splitedDate[1]), Convert.ToInt32(splitedDate[0]), 1);
                data.Startdate = startdate;
                data.Enddate = startdate.AddMonths(1).AddDays(-1);
            }
            var timeSheetdetails = new List<TimeSheetReport>();
            if(data.UserId == 0)
            {
                data.UserId = null;
            }
            var timereportInfo = (from t in DataContext.TimeSheets
                                  join p in DataContext.Projects on t.ProjectId equals p.ProjectId
                                  join c in DataContext.Clients on p.ClientId equals c.ClientId
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
                                      IsExpanded = false
                                  }).Distinct().ToList();
            foreach (var info in timereportInfo.GroupBy(g => new { g.ClientName, g.ClientId }))
            {
                var timesheetinfo = new TimeSheetReport();
                timesheetinfo.EmpDetailInfo = new List<ClientProjectInfo>();
                var lst = info.ToList();
                timesheetinfo.ClientId = info.Key.ClientId;
                timesheetinfo.ClientName = info.Key.ClientName;
                timesheetinfo.Project = DataContext.Projects.Where(u => u.ClientId == timesheetinfo.ClientId).Select(u => u.ProjectId).Count();

                timesheetinfo.UserId = new List<int>();
                foreach (var rec in lst)
                {
                    timesheetinfo.UserId.Add(rec.UserId);
                }
                
                timeSheetdetails.Add(timesheetinfo);
            }
            return timeSheetdetails;

        }

        public float SearchTotalHours(int ClientId)
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

        public List<ClientProjectInfo> clientProjectDetail(int clientId, List<int> userId)
        {
            var clientList = new List<ClientProjectInfo>();
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
                var clientInfo = new ClientProjectInfo();
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

        public List<EmployeeProjectDetails> EmployeeProjectInfo(int projectId, List<int> userId)
        {
            var employeeDetail = new List<EmployeeProjectDetails>();
            var Empinfo = (from p in DataContext.Projects
                           join t in DataContext.TimeSheets on p.ProjectId equals t.ProjectId
                           join ud in DataContext.UserDetails on t.UserId equals ud.UserId
                           join u in DataContext.UserAccounts on ud.UserId equals u.UserId
                           where (t.ProjectId == projectId && projectId != 0) && userId.Contains(t.UserId)
                           select new EmployeeProjectDetails
                           {
                               UserId = t.UserId,
                               TaskId = t.TaskId,
                               EmployeeName = u.UserName,
                               LastSubmittedOn = t.ModifiedOn,
                               IsExpanded = false
                           }).ToList();

            foreach (var info in Empinfo.GroupBy(g => new { g.UserId, g.TaskId }))
            {
                var empprojInfo = new EmployeeProjectDetails();

                empprojInfo.UserId = info.Select(s => s.UserId).FirstOrDefault();
                empprojInfo.TaskId = info.Select(s => s.TaskId).FirstOrDefault();
                empprojInfo.EmployeeName = info.Select(s => s.EmployeeName).FirstOrDefault();
                empprojInfo.LastSubmittedOn = info.Select(s => s.LastSubmittedOn).FirstOrDefault();
                employeeDetail.Add(empprojInfo);
            }
            foreach(var projectcount in employeeDetail)
            {
                projectcount.projectTotal = (from p in DataContext.Projects
                                             join t in DataContext.TimeSheets on p.ProjectId equals t.ProjectId
                                             where t.ProjectId == projectId
                                             select new
                                             {
                                                 projectTotal = t.BillableHours
                                             }).Sum(s => s.projectTotal);
            }
            return employeeDetail;
           
        }
        public List<EmpDetail> EmployeeEnteredTaskList(int taskId, int userId)
        {
            var TaskInfo = (from t in DataContext.TimeSheets
                            join u in DataContext.UserAccounts on t.UserId equals u.UserId
                            where t.UserId == userId && t.TaskId == taskId 
                            select new EmpDetail
                            {
                                Date = t.RequestDate,
                                BillableHours = t.BillableHours,
                                NonBillableHours = t.NonBillableHours,
                                BillableNotes = t.BillableHoursNote,
                                NonBillableHoursNotes = t.NonBillableHoursNote                               
                            }).ToList();

            float Count = 0;
            foreach(var info in TaskInfo)
            {
                Count = info.BillableHours + (float)(info.NonBillableHours != null ? info.NonBillableHours : 0);
                info.Total = Count;
            }

            return TaskInfo;
        }



    }
}