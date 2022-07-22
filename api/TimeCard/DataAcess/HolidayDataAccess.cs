using System;
using System.Collections.Generic;
using System.Linq;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class HolidayDataAccess : BaseDataAccess
    {
        public HolidayDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {

        }
        public bool AddHoliday(AddHolidayEntity holiday, int userId)
        {

            var info = new Holiday();
            info.HolidayDate = Convert.ToDateTime(holiday.HolidayDate);
            var day = Convert.ToDateTime(holiday.HolidayDate);
            info.HolidayReason = holiday.HolidayReason;
            info.HolidayDay = day.DayOfWeek.ToString();
            info.CreatedBy = userId;
            info.CreatedOn = DateTime.Now;
            info.ModifiedBy = userId;
            info.ModifiedOn = DateTime.Now;

            if (holiday.IsFloating == true)
            {
                info.HolidayType = "Floating Holiday";
                info.IsOpting = false;
                info.IsFloating = true;
                DataContext.Holidays.Add(info);
                if (DataContext.SaveChanges() == 1)
                    return true;
                else
                    return false;

            }
            else
            {
                info.HolidayType = "Public Holiday";
                info.IsOpting = true;
                info.IsFloating = false;
                DataContext.Holidays.Add(info);
                if (DataContext.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }



        }

        public bool DeleteHoliday(int id)
        {

            var info = DataContext.Holidays.FirstOrDefault(u => u.HolidayId == id);
            if (info != null)
            {
                DataContext.Holidays.Remove(info);
                DataContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<HolidayEntity> GetHoliday(int year, int userId)
        {
            var holiday = (from h in DataContext.Holidays
                           where h.HolidayDate.Year == year
                           orderby h.HolidayDate
                           select new HolidayEntity
                           {
                               HolidayId = h.HolidayId,
                               HolidayDate = h.HolidayDate.ToString("MM-dd-yyyy"),
                               HolidayReason = h.HolidayReason,
                               HolidayDay = h.HolidayDay,
                               IsFloating = h.IsFloating,
                               IsOpting = null,
                               HolidayType = h.HolidayType
                           }).ToList();
            var userOpt = (from uOpt in DataContext.UserOptHolidays
                           where uOpt.CreatedBy == userId
                           select new
                           {
                               UserOptHolidayId = uOpt.UserOptHolidayId,
                               HolidayId = uOpt.HolidayId,
                               IsOpting = uOpt.IsOpting,
                               IsVoided = uOpt.IsVoided,
                           }).ToList();

            // var result = new HolidayEntity();
            foreach (var h in holiday)
            {
                foreach (var uO in userOpt)
                {
                    if (h.HolidayId == uO.HolidayId)
                    {
                        h.IsOpting = uO.IsOpting;
                        h.UserOptHolidayId = uO.UserOptHolidayId;
                        h.IsVoided = uO.IsVoided;
                    }
                }
            }

            foreach (var h in holiday)
            {
                if (h.IsFloating == true)
                {
                    h.OptByAnyone = DataContext.UserOptHolidays.Where(u => u.HolidayId == h.HolidayId).Select(u => u).Count();
                }


            }


            return holiday;
        }


        public bool UpdateHoliday(AddHolidayEntity update, int userId)
        {
            var info = DataContext.Holidays.Where(u => u.HolidayId == update.HolidayId).Select(u => u).FirstOrDefault();
            if (info != null)
            {
                info.HolidayDate = Convert.ToDateTime(update.HolidayDate);
                var day = Convert.ToDateTime(update.HolidayDate);
                info.HolidayReason = update.HolidayReason;
                info.HolidayDay = day.DayOfWeek.ToString();
                info.CreatedBy = userId;
                info.CreatedOn = DateTime.Now;
                info.ModifiedBy = userId;
                info.ModifiedOn = DateTime.Now;
                info.IsFloating = update.IsFloating;
                if (update.IsFloating == true)
                {
                    info.HolidayType = "Floating Holiday";
                }
                else
                {
                    info.HolidayType = "Public Holiday";
                }
                if (DataContext.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        public bool OptHoliday(HolidayEntity isOptData, int userId)
        {
            var info = new UserOptHoliday();
            info.HolidayId = isOptData.HolidayId;
            info.IsOpting = true;
            info.IsVoided = false;
            info.CreatedBy = userId;
            info.CreatedOn = DateTime.Now;
            DataContext.UserOptHolidays.Add(info);
            if (DataContext.SaveChanges() == 1)
                return true;
            else
                return false;

        }

        public bool EditOptHoliday(HolidayEntity isOptData, int userId)
        {
            var updation = DataContext.UserOptHolidays.Where(u => u.UserOptHolidayId == isOptData.UserOptHolidayId).Select(u => u).FirstOrDefault();
            if (isOptData.IsOpting == true)
            {
                updation.IsOpting = false;
                updation.IsVoided = true;
            }
            else
            {
                updation.IsOpting = true;
                updation.IsVoided = false;
            }


            updation.ModifiedBy = userId;
            updation.ModifiedOn = DateTime.Now;
            if (DataContext.SaveChanges() == 1)
                return true;
            else
                return false;
        }

        public List<HolidayEntity> GetUpcomingHoliday()
        {
            var dt = DateTime.Now;
            var holiday = DataContext.Holidays.Where(u => u.HolidayDate > dt).OrderBy(holiday => holiday.HolidayDate)
                .Select(u => new HolidayEntity
                {
                    HolidayId = u.HolidayId,
                    HolidayDate = u.HolidayDate.ToString("MM-dd-yyyy"),
                    HolidayReason = u.HolidayReason,
                    HolidayDay = u.HolidayDay,
                    IsOpting = u.IsOpting,
                    HolidayType = u.HolidayType,
                    IsFloating = u.IsFloating,
                    OptBy = new List<UserOptEntity>()
                }).ToList();

            foreach (var h in holiday)
            {
                if (h.IsFloating)
                {
                    var listEmployees = (from uOpt in DataContext.UserOptHolidays
                                         join uA in DataContext.UserAccounts on uOpt.CreatedBy equals uA.UserId
                                         join uD in DataContext.UserDetails on uA.UserId equals uD.UserId
                                         where uOpt.HolidayId == h.HolidayId
                                         where uOpt.IsOpting == true
                                         select new UserOptEntity
                                         {
                                             UserName = uA.UserName,
                                             EmpId = uD.EmpId,
                                             IsOpting = uOpt.IsOpting,
                                             IsVoided = uOpt.IsVoided,
                                         }).ToList();
                    h.OptBy.AddRange(listEmployees);
                }
            }


            return holiday;
        }

        public List<ListEmpOpt> GetEmployeesOpt(int floatingHolidayId)
        {
            var listEmployees = (from uOpt in DataContext.UserOptHolidays
                                 join h in DataContext.Holidays on uOpt.HolidayId equals h.HolidayId
                                 join uA in DataContext.UserAccounts on uOpt.CreatedBy equals uA.UserId
                                 join uD in DataContext.UserDetails on uA.UserId equals uD.UserId
                                 where uOpt.HolidayId == floatingHolidayId
                                 select new ListEmpOpt
                                 {
                                     UserId = uOpt.CreatedBy,
                                     UserName = uA.UserName,
                                     UserEmail = uA.UserEmail,
                                     EmpId = uD.EmpId,
                                     IsOpting = uOpt.IsOpting,
                                     IsVoided = uOpt.IsVoided,
                                 }).ToList();

            return listEmployees;
        }
    }
}
