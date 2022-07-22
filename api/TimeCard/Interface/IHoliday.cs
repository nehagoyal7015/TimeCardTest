using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IHoliday
    {
        bool AddHoliday(AddHolidayEntity holiday, int userId);
        List<HolidayEntity> GetHoliday(int year, int userId);
        bool UpdateHoliday(AddHolidayEntity update, int userId);
        public bool OptHoliday(HolidayEntity holiday, int userId);
        public bool EditOptHoliday(HolidayEntity holiday, int userId);
        public bool DeleteHoliday(int id);
        List<ListEmpOpt> GetEmployeesOpt(int floatingHolidayId);

        List<HolidayEntity> GetUpcomingHoliday();
    }
}
