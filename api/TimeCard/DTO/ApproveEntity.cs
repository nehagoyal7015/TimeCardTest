using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class ApproveEntity
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public bool IsActive { get; set; }
        public proj Project { get; set; }
    }

    public class project1
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        //public string ProjectDescription { get; set; }
        //public bool ProjectIsActive { get; set; }
    }

    /* GetRecentApprovals */
    public class Clients1
    {
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
       
        public string ClientName { get; set; }
        public string UserName { get; set; }
        public int TaskId { get; set; }

        public List<Projects1> Project { get; set; }

    }

    public class Projects1
    {
        public string Client { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public float BillHours { get; set; }
        public float? NonBillHours { get; set; }
        public string  TaskName { get; set; }

        public List<ProjectTasks> ProjectTask { get; set; }

    }

    //public class ProjectTasks1
    //{
    //    public int TaskId { get; set; }
    //    public string TaskName { get; set; }
    //    public bool? IsArchived { get; set; }
    //}



    public class GetRecentProj1
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        //public List<ProjectTasks> ProjectTask { get; set; }
    }


    public class ApproveReport
    {
        
        public string UserName { get; set; }

        public int UserId { get; set; }
        public int TaskId { get; set; }

        public float Total { get; set; }

        public List<EmpDetailApp> EmpDetails { get; set; }


    }

    public class AppClientProjectInfo
    {
        public List<int> UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public float BillableHours { get; set; }
        public float Total { get; set; }
    }

    public class ApproveSearchDto
    {
        public List<int?> ProjectIds { get; set; }
        

    }

    public class ApprovalAccess
    {
        public int TimeSheetId { get; set; }
        public int ProjectId { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public float BillableHours { get; set; }
        public string BillableHoursNote { get; set; }
        public float? NonBillableHours { get; set; }
        public string NonBillableHoursNote { get; set; }
        public bool IsBilled { get; set; }
    }

    public class ApprovalReport
    {
        public List<int> UserId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public List<int> ProjectId { get; set; }
        public float TimeEntered { get; set; }
        public int Project { get; set; }
        public bool IsBillable { get; set; }
        public bool IsExpanded { get; set; }
        public List<ApprovalClientProjectInfo> EmpDetailInfo { get; set; }
    }
    /*Search Report*/
    public class AppReport
    {
        public List<int> UserId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
     /*   public string TaskName { get; set; }*/
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public float TimeEntered { get; set; }
        public int Project { get; set; }
        public bool IsBillable { get; set; }
        public bool IsExpanded { get; set; }
        public List<taskProject> EmpDetailInfo { get; set; }

        public List<ApproveReport> ApproveReports { get; set; }
    }

    /*Search Report*/
    public class AppSearchDto
    {
        public List<int?> ProjectIds { get; set; }
        public int? UserId { get; set; }
        public string currentDate { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public bool? Billable { get; set; }
        public bool? NonBillable { get; set; }

    }

    /* Client Project Detail*/
    public class ApprovalClientProjectInfo
    {
        public List<int> UserId { get; set; }
        public int ProjectId { get; set; }

        public int TaskId { get; set; }
        public string ProjectName { get; set; }
        public float BillableHours { get; set; }
        public float Total { get; set; }

       /* public List<taskProject> taskProject { get; set; }*/
    }

    /* Employee Project Info*/
    public class AppEmployeeProjectDetails
    {

        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string EmployeeName { get; set; }
        public float projectTotal { get; set; }
        public float Total { get; set; }
        public float BillableHours { get; set; }
        public bool IsExpanded { get; set; }
        public DateTime? LastSubmittedOn { get; set; }
       /* public List<EmpDetailApp> EmpDetails { get; set; }*/

    }

    public class AppSearchSelectedProject
    {
        public int ProjectId { get; set; }

        public List<int> UserId { get; set; }
    }

    /* Task List Project */
    public class taskProject
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public float TaskTotal { get; set; }
        public float BillableHours { get; set; }
        public float Total { get; set; }
        public bool IsExpanded { get; set; }
        public DateTime? LastSubmittedOn { get; set; }
       /* public List<AppEmployeeProjectDetails> AppEmployeeProjectDetail { get; set; }*/

        public List<ApproveReport> ApproveReports { get; set; }
    }

    /* Task List Project */
    public class taskData
    {
        public int ProjectId { get; set; }
        public List<int> UserId { get; set; }
         
       /* public List<AppSearchDto> AppSearchDto { get; set; }*/
    }

    public class EmpDetailApp
    {
        public DateTime Date { get; set; }
        public float BillableHours { get; set; }
        public float? NonBillableHours { get; set; }
        public string NonBillableHoursNotes { get; set; }
        public string BillableNotes { get; set; }
        public float? Total { get; set; }
    }
}

