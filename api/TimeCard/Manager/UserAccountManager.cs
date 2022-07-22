using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.CustomAuthentication;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Errors;
using TimeCard.Interface;
using TimeCard.Models;

namespace TimeCard.Manager
{
    public class UserAccountManager : BaseManager, IUserAccount
    {
        private readonly UserAccountDataAccess _userAccountDA;
        private readonly JwtAuth jwtAuth;
        public UserAccountManager(UserAccountDataAccess dataAccess, JwtAuth auth)
        {
            _userAccountDA = dataAccess;
            jwtAuth = auth;
        }

        public void AddUserAccount(UserAccountEntity useraccountEntity)
        {
            _userAccountDA.AddUserAccount(useraccountEntity);
            _userAccountDA.Commit();
        }
        public void UpdateUserAccount(UserAccountEntity useraccountEntity)
        {
            _userAccountDA.UpdateUserAccount(useraccountEntity);
        }
        public List<UserAccountEntity> GetUserAccount(int userId)
        {
            return _userAccountDA.GetUserAccount(userId);
        }
        public bool DeleteUserAccount(int UserId)
        {
            return _userAccountDA.DeleteUserAccount(UserId);
        }
        public UserLoginDetail UserAuthentication(UserCrediential userCredential)
        {
            var domain = _userAccountDA.DomainDetail(userCredential.DomainName);
            if (domain == null)
            {
                throw new AppException("Damain detail not found.");
            }
            var isValid = _userAccountDA.ValidateUser(userCredential, domain.DomainId);
            if (isValid)
            {
                var user = _userAccountDA.UserInfo(userCredential.UserEmail,userCredential.DomainName);

                user.Token = jwtAuth.Token(user.UserId, user.UserEmail);
                return user;
            }
            else
            {
                throw new AppException("User detail not found.");
            }

        }

        public UserLoginDetail gAuth(GoogleCrediential googleCrediential)
        {           
            var isValid = _userAccountDA.ValidateGoogleUser(googleCrediential);
            if (isValid)
            {
                var googleuser = _userAccountDA.gAuth(googleCrediential.UserEmail);

                googleuser.Token = jwtAuth.Token(googleuser.UserId, googleuser.UserEmail);
                return googleuser;
            }
            else
            {
                throw new AppException("User Email not found.");
            }
        }

        public GetUserName currentUser(int userId)
        {
            return _userAccountDA.currentUser(userId);
        }
        public ProfileEntity ProfileInformation(int userId)
        {
            var userDetails = _userAccountDA.ProfileInformation(userId);
            userDetails.SkillList = new List<string>();
            foreach (var skill in userDetails.Skills.Split(','))
            {
                userDetails.SkillList.Add(skill);
            }
            return userDetails;
        }
        public void UpdateProfieInfo(ProfileEntity userInfo, int userId) 
        {
            _userAccountDA.UpdateProfieInfo(userInfo,userId);
        }
    }
}
