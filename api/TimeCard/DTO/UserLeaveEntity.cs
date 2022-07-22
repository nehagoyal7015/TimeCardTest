using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO  
{
    public class UserLeaveEntity
    {
        public int LeaveId { get; set;}
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; }
        public string LeaveType { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public int? CancelledBy { get; set; }
        public int Total { get; set; }
        public int plannedtotal { get; set; }
        public int unplannedtotal { get; set; }
        public int holidaytotal { get; set; }
        public int floatingtotal { get; set; }
        public int totalCount { get; set; }

    }

    public class UserAcountDtos
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class CountDto 
    {
        public int Total { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int plannedtotal { get; set; }
        public int unplannedtotal { get; set; }
        public int holidatotal { get; set; }
        public int floatingtotal { get; set; }

        public int totalCount { get; set; }
    }

    public class UserLeaveDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Reason { get; set; }
        public string LeaveType { get; set;}
        public string EmpId { get; set; }
        public bool? IsVoided { get; set; }
        public bool? IsOpting { get; set; }
        public bool IsFloating { get; set; }
        public int HolidayId { get; set; }
        public string HolidayDate { get; set; }
        public string HolidayReason { get; set; }
    }


    public class UpcomingfloatingHoliday
    {
        public string  UserName  { get; set; }
        public string EmpId { get; set; }
        public bool? IsVoided { get; set; }
        public bool? IsOpting { get; set; }
        public bool IsFloating { get; set; }
        public int HolidayId { get; set; }
        public string HolidayDate { get; set; }
        public string HolidayReason { get; set; }

    }

    public class leavesDetails 
    {
        public int UserId { get; set; }
        public string Employee { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public int Total { get; set; }
        public string LeaveType { get; set; }
    }
    public class SearchDate 
    {
        public string fromDate { get; set;}
        public string todate { get; set; }
    }
}
