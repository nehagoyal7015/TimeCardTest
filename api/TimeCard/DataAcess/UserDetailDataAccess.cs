using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Errors;
using TimeCard.Models;
using AppDomain = TimeCard.Models.AppDomain;
using KeyValuePair = TimeCard.Common.KeyValuePair;


namespace TimeCard.DataAcess
{
    public class UserDetailDataAccess : BaseDataAccess
    {
        public UserDetailDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        // Save the User Information
        public void AddUserDetail(UserDetailEntity userDetail, string fileName, int userId)
        {
            var Info = new UserDetail();
            var info1 = new UserAccount();


            info1.DomainId = userDetail.DomainId;
            //info1.UserName = userDetail.UserName;
            //info1.Password = userDetail.Password;
            info1.UserEmail = userDetail.UserEmail;
            info1.IsActive = userDetail.IsActive;
            //info1.Ssoid = userDetail.Ssoid;
            info1.CreatedBy = userId;
            info1.CreatedOn = DateTime.Now;
            info1.ModifiedBy = userId;
            info1.ModifiedOn = DateTime.Now;

            DataContext.UserAccounts.Add(info1);
            DataContext.SaveChanges();

            Info.UserId = info1.UserId;
            //Info.FirstName = userDetail.FirstName;
            //Info.LastName = userDetail.LastName;
            Info.PhoneNumber = userDetail.PhoneNumber;
            //Info.Desgination = userDetail.Desgination;
            //Info.DateOfBirth = userDetail.DateOfBirth;
            Info.JoiningDate = userDetail.JoiningDate;
            Info.EmpId = userDetail.EmpId;
            Info.IsActive = userDetail.IsActive;
            //Info.FatherName = userDetail.FatherName;
            //Info.MotherName = userDetail.MotherName;
            //Info.AlternateNumber = userDetail.AlternateNumber;
                Info.PersonalEmail = userDetail.PersonalEmail;
            //Info.Skills = string.Join(",", (userDetail.SkillList).ToArray());
            Info.SkypeId = userDetail.SkypeId;
            Info.Address1 = userDetail.Address1;
            Info.Address2 = userDetail.Address2;
            //Info.IsMaritalStatus = userDetail.IsMaritalstatus;
            //Info.SpouseName = userDetail.SpouseName;
            //Info.SpouseEmail = userDetail.SpouseEmail;
            //Info.SpousePhone = userDetail.SpousePhone;
            Info.Profile = fileName;
            Info.CreatedBy = userId;
            Info.CreatedOn = DateTime.Now;
            Info.ModifiedBy = userId;
            Info.ModifiedOn = DateTime.Now;


            DataContext.UserDetails.Add(Info);
            DataContext.SaveChanges();
        }



        // Update the Userdetails
        public void UpdateUserDetail(UserDetailEntity userDetail, string fileName, int userId)
        {

            var info = DataContext.UserDetails.Where(u => u.PkId == userDetail.PK_Id).Select(s => s).FirstOrDefault();

            if (info != null)
            {
                info.FirstName = userDetail.FirstName;
                info.LastName = userDetail.LastName;
                info.PhoneNumber = userDetail.PhoneNumber;
                info.Desgination = userDetail.Desgination;
                info.DateOfBirth = userDetail.DateOfBirth;
                info.JoiningDate = userDetail.JoiningDate;
                info.EmpId = userDetail.EmpId;
                info.PersonalEmail = userDetail.PersonalEmail;
                info.IsActive = userDetail.IsActive;
                info.FatherName = userDetail.FatherName;
                info.MotherName = userDetail.MotherName;
                info.AlternateNumber = userDetail.AlternateNumber;
                info.SkypeId = userDetail.SkypeId;
                info.Address1 = userDetail.Address1;
                info.Address2 = userDetail.Address2;
                info.IsMaritalStatus = userDetail.IsMaritalstatus;
                info.SpouseName = userDetail.SpouseName;
                info.SpouseEmail = userDetail.SpouseEmail;
                info.SpousePhone = userDetail.SpousePhone;
                info.Skills = string.Join(",", (userDetail.SkillList).ToArray());
                info.Profile = fileName;
                info.ModifiedBy = userId;
                info.ModifiedOn = DateTime.Now;
            }


        }
        // Get the  All Employee List
        public List<EmployeeList> GetUserDetail()
        {
            return (from ud in DataContext.UserDetails
                    join ua in DataContext.UserAccounts on ud.UserId equals ua.UserId
                    select new EmployeeList
                    {
                        PK_Id = ud.PkId,
                        UserId = ua.UserId,
                        IsActive = ud.IsActive,
                        Name = ua.UserName,
                        PhoneNumber = ud.PhoneNumber,
                        EmpId = ud.EmpId,
                        JoiningDate = ud.JoiningDate,
                        EmailId = ua.UserEmail,
                        Address1 = ud.Address1,
                        Address2 = ud.Address2
                    }).ToList();
        }


        // Get the  UserList
        public UserInformationEntity GetUserDetailInfo(int pkId)
        {
            var userInfo = (from u in DataContext.UserDetails
                            join ua in DataContext.UserAccounts on u.UserId equals ua.UserId
                            where u.PkId == pkId
                            select new UserInformationEntity
                            {
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                PhoneNumber = u.PhoneNumber,
                                DateOfBirth = u.DateOfBirth,
                                Desgination = u.Desgination,
                                JoiningDate = u.JoiningDate,
                                IsActive = u.IsActive,
                                FatherName = u.FatherName,
                                MotherName = u.MotherName,
                                AlternateNumber = u.AlternateNumber,
                                PersonalEmail = u.PersonalEmail,
                                SkypeId = u.SkypeId,
                                Address1 = u.Address1,
                                Address2 = u.Address2,
                                IsMaritalStatus = u.IsMaritalStatus,
                                SpouseName = u.SpouseName,
                                SpousePhone = u.SpousePhone,
                                SpouseEmail = u.SpouseEmail,
                                Skills = u.Skills,
                                EmpId = u.EmpId,
                                UserName = ua.UserName,
                                Password = ua.Password,
                                UserEmail = ua.UserEmail
                            }).FirstOrDefault();


            return userInfo;
        }
        //public List<GetSkill> GetSkills(int pkId)
        //{
        //    var info = (from u in DataContext.UserDetails
        //                where u.PkId == pkId
        //                select new GetSkill
        //                {
        //                    Skills = u.Skills
        //                }).ToList();
        //    return info;


        public DomainDetails domainInfo(int domainId)
        {
            return DataContext.AppDomains.Where(ap => ap.DomainId == domainId).Select(s => new DomainDetails
            {
                DomainId = s.DomainId
            }).FirstOrDefault();
        }

        public void InActiveinfo(int pkId, int userId)
        {
            var inactiveInfo = DataContext.UserDetails.FirstOrDefault(ud => ud.PkId == pkId);
            if (inactiveInfo != null)
            {
                inactiveInfo.IsActive = false;
                inactiveInfo.ModifiedBy = userId;
                inactiveInfo.ModifiedOn = DateTime.Now;
                DataContext.SaveChanges();
            }

        }
        public void Activeinfo(int pkId, int userId)
        {
            var activeInfo = DataContext.UserDetails.FirstOrDefault(ud => ud.PkId == pkId);
            if (activeInfo != null)
            {

                activeInfo.IsActive = true;
                activeInfo.ModifiedBy = userId;
                activeInfo.ModifiedOn = DateTime.Now;
                DataContext.SaveChanges();
            }

        }

        public EmpDetails EmployeeDetails(int pkId)
        {
            
            var empDetailInfo = (from u in DataContext.UserAccounts
                                join ud in DataContext.UserDetails on u.UserId equals ud.UserId
                                 where ud.PkId == pkId
                                 select new EmpDetails
                                 {
                                     UserId = ud.UserId,
                                     EmployeeName = u.UserName,
                                     Email = u.UserEmail,
                                     EmpCode = ud.EmpId,
                                     PhoneNumber = ud.PhoneNumber
                                 }).FirstOrDefault();

            return empDetailInfo;

        }

        public List<ClientDetails> projectInfo()
        {
            var projectInfo = (from project in DataContext.Projects 
                               join client in DataContext.Clients on project.ClientId equals client.ClientId
                               join projuser in DataContext.ProjectUsers on project.ProjectId equals projuser.ProjectId
                               select new
                               {
                                   ClientId = client.ClientId,
                                   ClientName = client.ClientName,
                                   ProjectId = project.ProjectId,
                                   ProjectName = project.Name               
                               }).ToList();
            // Itrate for the client 
            var clients = new List<ClientDetails>();
            foreach (var clientrec in projectInfo.ToList().GroupBy(s => new { s.ClientId,s.ClientName }))
            {
                var client = new ClientDetails()
                {
                    ClientId = clientrec.Key.ClientId,
                    ClientName = clientrec.Key.ClientName,
                    Total = DataContext.Projects.Where(u => u.ClientId == clientrec.Key.ClientId).Select(u => u.ProjectId).Count()
                };

                client.Projectdata = new List<ProjectInfo>();
                foreach (var prjRec in clientrec.ToList().GroupBy(s => new { s.ProjectId, s.ProjectName }))
                {
                    var project = new ProjectInfo() 
                    { ProjectId = prjRec.Key.ProjectId, ProjectName = prjRec.Key.ProjectName }; 
                    // may be need to intiaizre Projects object before add a record to it.
                    client.Projectdata.Add(project);
                }
                clients.Add(client);
            }
            return clients;

        }   

        public List<ProjectListInfo> getProjectList(int userId)
        {
            var projectList = (from p in DataContext.Projects
                               join pu in DataContext.ProjectUsers on p.ProjectId equals pu.ProjectId
                               join c in DataContext.Clients on p.ClientId equals c.ClientId
                               where pu.UserId == userId

                               select new ProjectListInfo 
                               {
                                   ProjectId = p.ProjectId,
                                   ProjectName = p.Name,
                                   ClientName = c.ClientName
                               }).ToList();

            return projectList;
        }


       public void AddProject(List<AddProject> addProject, int logInUserId)
       {
            int userId = addProject.Select(s => s.UserId).FirstOrDefault();
            var assignProjectList = DataContext.ProjectUsers.Where(x => x.UserId == userId).Select(s => s).ToList();
            
                var projUserList = new List<ProjectUser>();
                var existingProjectId = assignProjectList.Select(s => s.ProjectId).ToArray();
                var currentIds = addProject.Select(s => s.ProjectId).ToArray();

                var newIds = currentIds.Except(existingProjectId).ToList();
                var removeIdList = existingProjectId.Except(currentIds).ToList();

                if(removeIdList.Count > 0)
                {
                    foreach(var id in removeIdList)
                    {
                        var assignedProjectInfo = DataContext.ProjectUsers.Where(x => x.ProjectId == id && x.UserId == userId).Select(s => s).FirstOrDefault();
                        DataContext.ProjectUsers.Remove(assignedProjectInfo);
                    }
                }
                if(newIds.Count > 0)
                {
                    foreach(var id in newIds)
                    {
                        var projUserInfo = new ProjectUser();

                        projUserInfo.ProjectId = id;
                        projUserInfo.UserId = userId;

                        projUserInfo.CreatedBy = logInUserId;
                        projUserInfo.CreatedOn = DateTime.Now;
                        projUserInfo.ModifiedBy = logInUserId;
                        projUserInfo.ModifiedOn = DateTime.Now;

                        projUserList.Add(projUserInfo);
                    }
                    DataContext.ProjectUsers.AddRange(projUserList);
                }
            DataContext.SaveChanges();
        }
            
       }
}
