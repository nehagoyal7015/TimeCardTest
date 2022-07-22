using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.Interface
{
    public interface IUserLeave
    {
        void AddUserLeave(UserLeaveEntity userLeave, int userId);
        List<UserLeaveEntity> GetUserLeave(int userId, int year);
        void CancelLeave(int LeaveId,int userId);
        List<UserAcountDtos> GetUserName();
        UserLeaveEntity CountLeaves(int userId);
        List<UserLeaveDto> GetAllUpcomingLeave();
        List<leavesDetails> GetleavesDetails();
        List<leavesDetails> SearchLeaveDetails(string userName, int Year,string date);
        void AddNewUserLeave(UserLeaveEntity userLeave, int userId);
    }
}
