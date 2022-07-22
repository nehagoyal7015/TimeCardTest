using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.Interface
{
    public interface IUserAccount
    {
        void AddUserAccount(UserAccountEntity useraccountEntity);
        void UpdateUserAccount(UserAccountEntity useraccountEntity);
        List<UserAccountEntity> GetUserAccount(int userId);
        bool DeleteUserAccount(int UserId);
        UserLoginDetail UserAuthentication(UserCrediential userCredential);
        UserLoginDetail gAuth(GoogleCrediential googleCrediential);
        GetUserName currentUser(int userId);
        ProfileEntity ProfileInformation(int userId);
        void UpdateProfieInfo(ProfileEntity userInfo, int userId);
    }
}
