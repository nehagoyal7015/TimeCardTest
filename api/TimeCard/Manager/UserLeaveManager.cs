using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;
using TimeCard.Models;

namespace TimeCard.Manager
{
    public class UserLeaveManager : BaseManager, IUserLeave
    {
        private readonly UserLeaveDataAccess _userLeaveDataAccess;
        public UserLeaveManager(UserLeaveDataAccess dataAccess)
        {
            _userLeaveDataAccess = dataAccess;
        }
        public void AddUserLeave(UserLeaveEntity userLeave,int userId)
        {
            _userLeaveDataAccess.AddUserLeave(userLeave,userId);
            _userLeaveDataAccess.Commit();
        }
        public void AddNewUserLeave(UserLeaveEntity userLeave, int userId)
        {
            _userLeaveDataAccess.AddNewUserLeave(userLeave,userId);
        }
        public List<UserLeaveEntity> GetUserLeave(int userId ,int year)
        {
            return _userLeaveDataAccess.GetUserLeave(userId,year); 
        }
        public void CancelLeave(int LeaveId, int userId)
        {
            _userLeaveDataAccess.CancelLeave(LeaveId, userId);
        }
        public List<UserAcountDtos> GetUserName()
        {
            return _userLeaveDataAccess.GetUserName();
        }
        public UserLeaveEntity CountLeaves(int userId)
        {
            return _userLeaveDataAccess.CountLeaves(userId);
        }
        public List<UserLeaveDto> GetAllUpcomingLeave()
        {
            return _userLeaveDataAccess.GetAllUpcomingLeave();
        }
        public List<leavesDetails> GetleavesDetails()
        {
            return _userLeaveDataAccess.GetleavesDetails();
        }
        public List<leavesDetails> SearchLeaveDetails(string userName, int Year , string date) 
        {
            return _userLeaveDataAccess.SearchLeaveDetails(userName, Year,date);
        }
    }
}
