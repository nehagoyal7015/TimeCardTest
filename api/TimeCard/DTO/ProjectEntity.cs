using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyValuePair = TimeCard.Common.KeyValuePair;


namespace TimeCard.DTO
{
    public class ProjectEntity
    {

        public int DomainId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BudgetCost { get; set; }
        public float BudgetHours { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EstimateCompletion { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool IsActive { get; set; }
        public string OwnerShips { get; set; }
        public float? ActualBudget { get; set; }
        public List<TaskDataList> TaskList { get; set; }
        public List<UserdataInfo> UserInfo { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public bool IsBillable { get; set; }
    }
        
    public class UserdataInfo
    {
        public int UserId { get; set; }
    }
    public class TaskDataList
    {
        public int TaskId { get; set; }
        public string Task { get; set; }
        public decimal? TaskBudgetCost { get; set; }
        public float? TaskBudgetHours { get; set; }
        public bool? IsArchive { get; set; }

    }

    public class UserIdList
    {
        public int userId { get; set; }
    }


    public class ProjectList 
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public float? BudgetHours { get; set; }
        public string OwnerShips { get; set; }
    }   
   

    public class DomainEntity 
    {
        public int DomainId { get; set; }
    }


    public class ProjectInformation
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? BudgetCost { get; set; }
        public float? BudgetHours { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EstimateCompletion { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool IsActive { get; set; }
        public string OwnerShips { get; set; }
        public float? ActualBudget { get; set; }
        public string Task { get; set;}
        public List<ProjectTaskList> TaskInfoList { get; set; }
        public List<GetEmployeeList> UserInfo { get; set; }
        public bool IsBillable { get; set; }
        public decimal? TaskBudgetCost { get; set; }
        public float? TaskBudgetHours { get; set; }
        public bool? IsArchive { get; set; }
        public List<TaskListInfo> TaskList { get; set; }
    }
    public class TaskListInfo
    {
        public int TaskId { get; set; }
        public string Task { get; set; }
        public decimal? TaskBudgetCost { get; set; }
        public float? TaskBudgetHours { get; set; }
        public bool? IsArchive { get; set; }

    }
    public class ProjectTaskList
    {
        public string Task { get; set; }
    }
    public class GetEmployeeList
    {
       public int UserId { get; set; }
     
    }
    public class TaskList
    {
        public int ProjectId { get; set; }
        public string OwnerShips { get; set; }
        public DateTime? CreatedOn { get; set; }
        public float? totalBudgetHours { get; set; }
        public List<projectTaskList> projectTaskList { get; set; }
      
    }
    
    public class projectTaskList
    {
    
        public int userId { get; set; }
       public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public string Task { get; set; }
        public float? BudgetHours { get; set; }
        public float ActualBudget { get; set; }
        public int EmployeeCount { get; set; }
        public List<GetEmpSubmittedHour> GetEmpSubmittedHours { get; set; }
    }
    public class GetEmpSubmittedHour
    {
        public int UserId { get; set; }
         public int TaskId { get; set; }
        public string UserName { get; set; }
        public float Hours { get; set; }
        public float? Total { get; set; }
        public DateTime? LastTimeEntered { get; set; }
        public List<EmployeeTaskList> EmployeeTaskLists { get; set; }
    }

    public class EmployeeTaskList 
    {
        public int TaskId { get; set; }
        public DateTime TaskDate { get; set;}
        public float BillableHours { get; set; }
        public string BillableHoursNote { get; set; }
    }
    public class ProjectListInformation
    {
        public int ProjectId { get; set; }  
        public string ClientName { get; set; }  
        public string ProjectName { get; set; }
        public string AsignTo { get; set; }
        public string Managar { get; set; }
        public float BillableHours { get; set; }
        public float TotalHourSpent { get; set; }
        public float CurrentMonthSpent { get; set; }
    }
    public class EmpList
    {
       public int UserId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class AllEmployeeList
    {
        public List<UserdataInfo> UserInfo { get; set; }
        public string UserName { get; set; }
        public bool Value { get; set; }
    }

    public class ClientInfo
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public bool Checked { get; set; }
        
        public List<ProjectInfoList> ProjectData { get; set; }
    }
    public class ProjectInfoList
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool isSelected { get; set; }
      
    }
}



