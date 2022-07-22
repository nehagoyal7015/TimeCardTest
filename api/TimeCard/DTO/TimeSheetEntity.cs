using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class AddTimeSheetEntity
    {
    
        public int TimeSheetId { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public DateTime RequestDate { get; set; }
        public float BillableHours { get; set; }
        public string BillableHoursNote { get; set; }
        public float? NonBillableHours { get; set; }
        public string NonBillableHoursNote { get; set; }
        // public DateTime? StartTime { get; set; }
        // public DateTime? EndTime { get; set; }

    }

    public class UpdateTimeSheetEntity
    {
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public float BillableHours { get; set; }
        public string BillableHoursNote { get; set; }
        public float? NonBillableHours { get; set; }
        public string NonBillableHoursNote { get; set; }
        // public DateTime? StartTime { get; set; }
        // public DateTime? EndTime { get; set; }
    }

    public class TimeSheetAccess
    {
        public int TimeSheetId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public float BillableHours { get; set; }
        public string BillableHoursNote { get; set; }
        public float? NonBillableHours { get; set; }
        public string NonBillableHoursNote { get; set; }
        public bool IsBilled { get; set; }
    }

    public class WorkHours
    {
        public float WorkingHours { get; set; }
    }
    
    public class TimeSheetReport 
    {
        public List<int> UserId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public List<int> ProjectId { get; set; }
        public float TimeEntered { get; set; } 
        public int Project { get; set;}
        public bool IsBillable { get; set; }
        public bool IsExpanded { get; set; }
        public List<ClientProjectInfo> EmpDetailInfo { get; set;}
    }

    public class ClientProjectInfo
    {
        public List<int> UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public float BillableHours { get; set; }
        public float Total { get; set; }
    }
    public class SearchReport 
    {
        public string clientName { get; set;}
        public string managerName { get; set;}
        public DateTime? searchdate { get; set;}
        public int? billable { get; set; }
        public int? nonBillable { get; set; }
      
    }

  

    public class EmployeeProjectDetails
    {
  
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string EmployeeName { get; set; }
        public float projectTotal { get; set; }
        public float Total { get; set; }
        public float BillableHours { get; set; }
        public bool IsExpanded { get; set; }
        public DateTime? LastSubmittedOn { get; set; }
        public List<EmpDetail> EmpDetails{ get; set; }

    }
    public class EmpDetail
    {
        public DateTime Date { get; set; }
        public float BillableHours { get; set; }
        public float? NonBillableHours { get; set; }
        public string NonBillableHoursNotes { get; set; }
        public string BillableNotes { get; set; }
        public float? Total { get; set; }
    }
    public class WeekData
    {
        public float workedHrs { get; set; }
        public float totalWorkHr { get; set; }

    }

    public class SearchDto
    {
        public List<int?> ProjectIds { get; set; }
        public int? UserId { get; set; }
        public string currentDate { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public bool? Billable { get; set; }
        public bool? NonBillable { get; set; }

    }

    public class SearchSelectedProject
    {
        public int ProjectId { get; set; }
        
        public List<int> UserId { get; set; }
    }
}
