using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class HolidayEntity
    {
        public int UserOptHolidayId { get; set; }
        public int HolidayId { get; set; }
        public string HolidayDate { get; set; }
        public string HolidayReason { get; set; }
        public string HolidayDay { get; set; } 
        public bool? IsOpting { get; set; }
        public bool? IsVoided { get; set; }
        public bool IsFloating { get; set; }
        public string? HolidayType { get; set; }
        public int OptByAnyone { get; set; }
        public List<UserOptEntity> OptBy { get; set; }
    }

    public class AddHolidayEntity
    {
        public int HolidayId { get; set; }
        public string HolidayDate { get; set; }
        public string HolidayReason { get; set; }
        public string HolidayDay { get; set; } 
        public bool? IsOpting { get; set; }
        public bool IsFloating { get; set; }
        public string? HolidayType { get; set; }
    }

    public class UserOptEntity
    {
        public string  UserName  { get; set; }
        public string EmpId { get; set; }
        public bool? IsVoided { get; set; }
        public bool? IsOpting { get; set; }
    }

    

    public class ListEmpOpt
    {
        public int UserId { get; set; }
        public string  UserName  { get; set; }
        public string UserEmail { get; set; }
        public string EmpId { get; set; }
        public bool? IsVoided { get; set; }
        public bool? IsOpting { get; set; }
        
    }
}
