using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface ITimeSheet
    {
        bool AddTimeSheet(AddTimeSheetEntity timeSheet, int userId);
        // bool UpdateTimeSheet(UpdateTimeSheetEntity updateTimeSheet,int timeSheetId);
        List<TimeSheetAccess> GetTimeSheet(string date,int userId);
        List<float> GetWorkHr(string date,int userId);
        public bool DeleteTimeSheet(int timeSheetId);
        
        WeekData GetMonthHr(int userId);
        WeekData GetWeekHr(int userId);
        List<TimeSheetReport> GetTimeSheetReport();
        List<TimeSheetReport> SearchReports(SearchDto data);
        List<ClientProjectInfo> clientProjectDetail(int clientId,int userId);
        List<EmployeeProjectDetails> EmployeeProjectInfo(int projectId, List<int> userId);
        //List<EmpDetail> EmployeeEnteredTaskList(int userId);
    }
}
