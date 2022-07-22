using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class UserLeaveDataAccess : BaseDataAccess
    {
        public UserLeaveDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        public void AddUserLeave(UserLeaveEntity userLeave, int userId)
        {
            var leaveInfo = new UserLeave();

            leaveInfo.UserId = userId;
            leaveInfo.FromDate = Convert.ToDateTime(userLeave.FromDate);
            leaveInfo.ToDate = Convert.ToDateTime(userLeave.ToDate);
            leaveInfo.Reason = userLeave.Reason;
            leaveInfo.LeaveType = userLeave.LeaveType;
            leaveInfo.IsCancelled = userLeave.IsCancelled;
            leaveInfo.CancelledDate = userLeave.CancelledDate;
            leaveInfo.CancelledBy = userLeave.CancelledBy;
            leaveInfo.CreatedBy = userId;
            leaveInfo.CreatedOn = DateTime.Now;
            leaveInfo.ModifiedBy = userId;
            leaveInfo.ModifiedOn = DateTime.Now;

            DataContext.UserLeaves.Add(leaveInfo);
            DataContext.SaveChanges();
        }

        public void AddNewUserLeave(UserLeaveEntity userLeave, int userId)
        {
            
            var leaveInfo = new UserLeave();

            leaveInfo.UserId = userId;
            leaveInfo.FromDate = Convert.ToDateTime(userLeave.FromDate);
            leaveInfo.ToDate = Convert.ToDateTime(userLeave.ToDate);
            leaveInfo.Reason = userLeave.Reason;
            leaveInfo.LeaveType = userLeave.LeaveType;
            leaveInfo.IsCancelled = userLeave.IsCancelled;
            leaveInfo.CancelledDate = userLeave.CancelledDate;
            leaveInfo.CancelledBy = userLeave.CancelledBy;
            leaveInfo.CreatedBy = userId;
            leaveInfo.CreatedOn = DateTime.Now;
            leaveInfo.ModifiedBy = userId;
            leaveInfo.ModifiedOn = DateTime.Now;

            DataContext.UserLeaves.Add(leaveInfo);
            DataContext.SaveChanges();
        }

        // Get User All Leave
        public List<UserLeaveEntity> GetUserLeave(int userId, int year)
        {
            var userLeave = (from ul in DataContext.UserLeaves
                             where ul.UserId == userId && ul.FromDate.Year == year
                             orderby ul.FromDate
                             select new UserLeaveEntity
                             {
                                 LeaveId = ul.LeaveId,
                                 LeaveType = ul.LeaveType,
                                 FromDate = ul.FromDate,
                                 ToDate = ul.ToDate,
                                 Reason = ul.Reason,
                                 IsCancelled = ul.IsCancelled == false ? (ul.FromDate >= DateTime.Today ? false : true) : ul.IsCancelled,
                                 CancelledBy = ul.UserId
                             }).ToList();

            foreach (var info in userLeave)
            {
                if (info.ToDate != null)
                {
                    info.Total = (int)((info.ToDate - info.FromDate).TotalDays) + 1;
                }
                else
                {
                    info.Total = 1;
                }
            }
            return userLeave;
        }


        public void CancelLeave(int leaveId, int userId)
        {
            var DeleteUserInfo = DataContext.UserLeaves.FirstOrDefault(u => u.LeaveId == leaveId);

            if (DeleteUserInfo != null)
            {
                DeleteUserInfo.IsCancelled = true;
                DeleteUserInfo.CancelledDate = DateTime.Now;
                DeleteUserInfo.CancelledBy = userId;
                DataContext.SaveChanges();
            }

        }

        public List<UserAcountDtos> GetUserName()
        {

            List<UserAcountDtos> userAccount = (from u in DataContext.UserAccounts
                                                join ud in DataContext.UserDetails on u.UserId equals ud.UserId

                                                select new UserAcountDtos
                                                {
                                                    UserId = ud.UserId,
                                                    UserName = u.UserName
                                                }).ToList();

            return userAccount;
        }
        public List<UserLeaveDto> GetAllUpcomingLeave()
        {
            var dt = DateTime.Now;
            var allUser = (from ul in DataContext.UserLeaves
                           where ul.ToDate >= dt && ul.ToDate <= dt.AddDays(7)
                           where ul.IsCancelled == false
                           join u in DataContext.UserAccounts on ul.UserId equals u.UserId

                           select new UserLeaveDto
                           {
                               UserId = u.UserId,
                               UserName = u.UserName,
                               FromDate = ul.FromDate.ToString("MM/dd/yyyy"),
                               ToDate = ul.ToDate.ToString("MM/dd/yyyy"),
                               Reason = ul.Reason,
                               LeaveType = ul.LeaveType
                           }).ToList();

            var dtn = DateTime.Now;

            var floating = (from uOpt in DataContext.UserOptHolidays
                            join h in DataContext.Holidays on uOpt.HolidayId equals h.HolidayId
                            join uA in DataContext.UserAccounts on uOpt.CreatedBy equals uA.UserId
                            join uD in DataContext.UserDetails on uA.UserId equals uD.UserId
                            where h.HolidayDate >= dtn && h.HolidayDate <= dtn.AddDays(7)
                            orderby h.HolidayDate

                            where uOpt.IsOpting == true
                            select new UserLeaveDto
                            {
                                HolidayId = h.HolidayId,
                                HolidayDate = h.HolidayDate.ToString("MM-dd-yyyy"),
                                HolidayReason = h.HolidayReason,
                                UserName = uA.UserName,
                                EmpId = uD.EmpId,
                                IsOpting = uOpt.IsOpting,
                                IsVoided = uOpt.IsVoided,
                                IsFloating = h.IsFloating,
                                FromDate = h.HolidayDate.ToString("MM-dd-yyyy"),

                            }).ToList();

            allUser.AddRange(floating);
            allUser.OrderBy(u => u.FromDate);

            return allUser;
        }


        public UserLeaveEntity CountLeaves(int userId)
        {
            UserLeaveEntity cnt = new UserLeaveEntity();

            var userLeave = DataContext.UserLeaves.Where(x => x.UserId == userId).Select(s => s).ToList();
            var userLeavelist = userLeave.Distinct().Select(s => s).ToList();


            cnt.plannedtotal = 0;
            cnt.unplannedtotal = 0;
            cnt.holidaytotal = 0;
            cnt.floatingtotal = 0;
            foreach (var info in userLeavelist)
            {
                var newCount = new UserLeaveEntity();

                if (info.LeaveType == "planned")
                {
                    if (info.IsCancelled == false)
                    {
                        if (info.ToDate != null)
                        {
                            cnt.plannedtotal = cnt.plannedtotal + (int)((Convert.ToDateTime(info.ToDate)) - (Convert.ToDateTime(info.FromDate))).TotalDays + 1;

                        }
                        else
                        {
                            cnt.plannedtotal = cnt.plannedtotal + 1;

                        }
                    }
                }
                else if (info.LeaveType == "Unplanned")
                {
                    if (info.IsCancelled == false)
                    {
                        if (info.ToDate != null)
                        {
                            cnt.unplannedtotal = cnt.unplannedtotal + (int)((Convert.ToDateTime(info.ToDate)) - (Convert.ToDateTime(info.FromDate))).TotalDays + 1;
                            //unplannedtotal++;
                        }
                        else
                        {
                            cnt.unplannedtotal = cnt.unplannedtotal + 1;

                        }
                        //unplannedtotal++;
                    }
                }


            }
            cnt.totalCount = cnt.plannedtotal + cnt.unplannedtotal + cnt.holidaytotal + cnt.floatingtotal;
            return cnt;
        }
        public List<leavesDetails> GetleavesDetails()
        {
            var leaveInfo = (from ul in DataContext.UserLeaves
                             join u in DataContext.UserAccounts on ul.UserId equals u.UserId

                             select new leavesDetails
                             {
                                 UserId = ul.UserId,
                                 LeaveType = ul.LeaveType,
                                 FromDate = ul.FromDate.ToString("MM/dd/yyyy"),
                                 ToDate = ul.ToDate.ToString("MM/dd/yyyy"),
                                 Reason = ul.Reason,
                                 Employee = u.UserName
                             }).ToList();
            foreach (var info in leaveInfo)
            {
                if (info.ToDate != null)
                {
                    info.Total = (int)((Convert.ToDateTime(info.ToDate)) - (Convert.ToDateTime(info.FromDate))).TotalDays + 1;
                }
                else
                {
                    info.Total = 1;
                }
            }
            return leaveInfo;
        }

        public List<leavesDetails> SearchLeaveDetails(string userName, int Year, string date)
        {

            var filterDate = Convert.ToDateTime(date);
            var seachinfo = (from ul in DataContext.UserLeaves
                             join u in DataContext.UserAccounts on ul.UserId equals u.UserId
                             where (string.IsNullOrEmpty(userName) || u.UserName == userName.ToLower()) && (Year == 0 || ul.FromDate.Year == Year)
                              && (string.IsNullOrEmpty(date) || (ul.FromDate >= filterDate))
                             select new leavesDetails
                             {
                                 UserId = ul.UserId,
                                 LeaveType = ul.LeaveType,
                                 FromDate = ul.FromDate.ToString("MM/dd/yyyy"),
                                 ToDate = ul.ToDate.ToString("MM/dd/yyyy"),
                                 Reason = ul.Reason,
                                 Employee = u.UserName
                             }).ToList();
            foreach (var info in seachinfo)
            {
                if (info.ToDate != null)
                {
                    info.Total = (int)((Convert.ToDateTime(info.ToDate)) - (Convert.ToDateTime(info.FromDate))).TotalDays + 1;
                }
                else
                {
                    info.Total = 1;
                }
            }
            return seachinfo;
        }

    }
}
