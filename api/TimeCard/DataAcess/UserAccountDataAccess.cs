using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Errors;
using TimeCard.Middleware;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class UserAccountDataAccess : BaseDataAccess
    {
        public UserAccountDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        // Add the User information
        public void AddUserAccount(UserAccountEntity useraccountEntity)
        {

            var userinfo = new UserAccount();

            userinfo.DomainId = 2;
            userinfo.UserName = useraccountEntity.UserName;
            userinfo.Password = useraccountEntity.Password;
            userinfo.UserEmail = useraccountEntity.UserEmail;
            userinfo.Ssoid = useraccountEntity.Ssoid;
            userinfo.IsActive = useraccountEntity.IsActive;
            userinfo.CreatedBy = 1;
            userinfo.CreatedOn = DateTime.Now;
            userinfo.ModifiedBy = 1;
            userinfo.ModifiedOn = DateTime.Now;

            DataContext.UserAccounts.Add(userinfo);
            DataContext.SaveChanges();
        }

        // Update the UserInformation
        public void UpdateUserAccount(UserAccountEntity useraccountEntity)
        {
            var UserInfo = DataContext.UserAccounts.Where(u => u.UserId == useraccountEntity.UserId).Select(u => u).FirstOrDefault();

            if (UserInfo != null)
            {
                UserInfo.UserName = useraccountEntity.UserName;
                UserInfo.Password = useraccountEntity.Password;
                UserInfo.UserEmail = useraccountEntity.UserEmail;
                UserInfo.Ssoid = useraccountEntity.Ssoid;
                UserInfo.IsActive = useraccountEntity.IsActive;
                UserInfo.ModifiedBy = 1;
                UserInfo.ModifiedOn = DateTime.Now;
            }

            DataContext.SaveChanges();
        }


        public List<UserAccountEntity> GetUserAccount(int userId)
        {


            List<UserAccountEntity> userAccount = (from u in DataContext.UserAccounts
                                                   where u.UserId == userId
                                                   select new UserAccountEntity
                                                   {
                                                       UserName = u.UserName,
                                                       Password = u.Password,
                                                       UserEmail = u.UserEmail,
                                                       Ssoid = u.Ssoid,
                                                       IsActive = u.IsActive,

                                                   }).ToList();
            if (userAccount == null)
            {
                throw new Exception("Record not found");
            }
            return userAccount;

        }

        // Delete the UserAccount Information 
        public bool DeleteUserAccount(int UserId)
        {
            var DeleteUserInfo = DataContext.UserAccounts.FirstOrDefault(u => u.UserId == UserId);

            if (DeleteUserInfo != null)
            {
                DataContext.UserAccounts.Remove(DeleteUserInfo);
                DataContext.SaveChanges();
                return true;
            }

            return false;

        }

        // 
        public bool ValidateUser(UserCrediential userCredential, int domainId)
        {
            var validuser = DataContext.UserAccounts.Where(x => x.UserEmail.ToLower() == userCredential.UserEmail.ToLower() && x.Password == userCredential.Password && x.DomainId == domainId).Any();
            return validuser;
        }
        public Models.AppDomain DomainDetail(string domain)
        {
            return DataContext.AppDomains.Where(s => s.DomainName == domain).Select(s => s).FirstOrDefault();
        }
        public UserLoginDetail UserInfo(string userEmail, string domainName)
         {
            var info = (from a in DataContext.AppDomains
                        join u in DataContext.UserAccounts on a.DomainId equals u.DomainId
                        where u.UserEmail.ToLower() == userEmail.ToLower() && a.DomainName == domainName
                        select new UserLoginDetail
                        {
                            UserId = u.UserId,
                            UserEmail = u.UserEmail,
                            UserName = u.UserName,
                            DomainId = a.DomainId
                        }).FirstOrDefault();
            return info;

            //return DataContext.UserAccounts.Where(x => x.UserName.ToLower() == userName.ToLower()).Select(s => new UserLoginDetail
            //{
            //    UserId = s.UserId,
            //    UserEmail = s.UserEmail,
            //    UserName = s.UserName
                
            //}).FirstOrDefault();
        }
        public bool ValidateGoogleUser(GoogleCrediential userCredential)
            {

            var validgoogleuser = DataContext.UserAccounts.Where(x => x.UserEmail.ToLower() == userCredential.UserEmail.ToLower()).Any();
            return validgoogleuser;

            }
        public UserLoginDetail gAuth(string useremail)
        {

            var Auth = (from a in DataContext.AppDomains
                 join u in DataContext.UserAccounts on a.DomainId equals u.DomainId
                 join ud in DataContext.UserDetails on u.UserId equals ud.UserId
                 where u.UserEmail == useremail.ToLower() || ud.PersonalEmail == useremail.ToLower()
                        select new UserLoginDetail
                 {
                     UserId = u.UserId,
                     UserEmail = u.UserEmail,
                     UserName = u.UserName,
                     DomainId = a.DomainId
                 }).FirstOrDefault();
                return Auth;
        }

        public GetUserName currentUser(int userId)
        {
            var Info1 = DataContext.UserAccounts.Where(u => u.UserId == userId).Select(u => new GetUserName
            {
                UserId = u.UserId,
                UserName = u.UserName
            }).FirstOrDefault();
            return Info1;
        }
        
        public ProfileEntity ProfileInformation(int userId)
        {
            var profileInfo = (from u in DataContext.UserDetails
                               join ua in DataContext.UserAccounts on u.UserId equals ua.UserId
                               where ua.UserId == userId
                               select new ProfileEntity
                               {
                                   PhoneNumber = u.PhoneNumber,
                                   FatherName = u.FatherName,
                                   MotherName = u.MotherName,
                                   AlternateNumber = u.AlternateNumber,
                                   PersonalEmail = u.PersonalEmail,
                                   SkypeId = u.SkypeId,
                                   Address1 = u.Address1,
                                   Address2 = u.Address2,
                                   IsMaritalstatus = u.IsMaritalStatus,
                                   SpouseName = u.SpouseName,
                                   SpousePhone = u.SpousePhone,
                                   SpouseEmail = u.SpouseEmail,
                                   Skills = u.Skills,
                                   UserName = ua.UserName,
                                   UserEmail = ua.UserEmail
                               }).FirstOrDefault();


            return profileInfo;
        }
        public void UpdateProfieInfo(ProfileEntity userInfo,int userId)
        {

            var userAccountInfo = DataContext.UserAccounts.Where(u => u.UserId == userId).Select(s => s).FirstOrDefault();
            var userDetailsInfo = DataContext.UserDetails.Where(ud => ud.UserId == userId).Select(x => x).FirstOrDefault();

            if (userAccountInfo != null || userDetailsInfo != null)
            {
                userDetailsInfo.PhoneNumber = userInfo.PhoneNumber;
                userDetailsInfo.FatherName = userInfo.FatherName;
                userDetailsInfo.MotherName = userInfo.MotherName;
                userDetailsInfo.AlternateNumber = userInfo.AlternateNumber;
                userDetailsInfo.PersonalEmail = userInfo.PersonalEmail;
                userDetailsInfo.SkypeId = userInfo.SkypeId;
                userDetailsInfo.Address1 = userInfo.Address1;
                userDetailsInfo.Address2 = userInfo.Address2;
                userDetailsInfo.IsMaritalStatus = userInfo.IsMaritalstatus;
                userDetailsInfo.SpouseName = userInfo.SpouseName;
                userDetailsInfo.SpousePhone = userInfo.SpousePhone;
                userDetailsInfo.SpouseEmail = userInfo.SpouseEmail;
                userAccountInfo.UserName = userInfo.UserName;
                userAccountInfo.ModifiedBy = userId;
                userAccountInfo.ModifiedOn = DateTime.Now;
                userDetailsInfo.ModifiedBy = userId;
                userDetailsInfo.ModifiedOn = DateTime.Now;
            }
            DataContext.SaveChanges();
        }
    }

}
