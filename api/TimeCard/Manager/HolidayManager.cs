using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class HolidayManager : BaseManager, IHoliday
    {
        private readonly HolidayDataAccess _holidayDataAccess;
        public HolidayManager(HolidayDataAccess dataAccess)
        {
            _holidayDataAccess = dataAccess;
        }
        public bool AddHoliday(AddHolidayEntity holiday, int userId)
        {
            return _holidayDataAccess.AddHoliday(holiday, userId);
        }
        public List<HolidayEntity> GetHoliday(int year, int userId)
        {
            return _holidayDataAccess.GetHoliday(year, userId);
        }

        public bool UpdateHoliday(AddHolidayEntity update, int userId)
        {
            return _holidayDataAccess.UpdateHoliday(update, userId);
        }

        public bool DeleteHoliday(int id)
        {
            return _holidayDataAccess.DeleteHoliday(id);
        }

        public bool OptHoliday(HolidayEntity holidayId, int userId)
        {
            return _holidayDataAccess.OptHoliday(holidayId, userId);
        }

        public List<HolidayEntity> GetUpcomingHoliday()
        {
            return _holidayDataAccess.GetUpcomingHoliday();
        }

        public List<ListEmpOpt> GetEmployeesOpt(int floatingHolidayId)
        {
            return _holidayDataAccess.GetEmployeesOpt(floatingHolidayId);
        }

        public bool EditOptHoliday(HolidayEntity holiday, int userId)
        {
            return _holidayDataAccess.EditOptHoliday(holiday, userId);
        }
    }
}