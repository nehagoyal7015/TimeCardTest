
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class UserDetailManager : BaseManager, IUserDetail
    {
        private readonly UserDetailDataAccess _userDetailDataAccess;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration _config;


        public UserDetailManager(UserDetailDataAccess dataAccess, IWebHostEnvironment hostEnvironment, IConfiguration config)
        {
            _userDetailDataAccess = dataAccess;
            webHostEnvironment = hostEnvironment;
            _config = config;
        }
        public void AddUserDetail(UserDetailEntity userDetail, string fileName, int userId)
        {
            _userDetailDataAccess.AddUserDetail(userDetail,fileName, userId);
        }
        public void UpdateUserDetail(UserDetailEntity userDetail,string fileName, int userId)
        {
            _userDetailDataAccess.UpdateUserDetail(userDetail, fileName, userId);
            _userDetailDataAccess.Commit();
        } 
 
        public string UserProfileUpload(IFormFile file) 
        {
            if (file != null)
            {
                var fileName = file.FileName;

                if (file.Length > 0)
                {
                    string path = $"{_config["FilePath:FileRooTPath"]}/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + fileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
                return fileName;
            }
            else
            {
                throw new ApplicationException(" file not found ");
            }
        }
        public string UpdateUserProfile(IFormFile file)
        {
            if (file != null)
            {
                var fileName = file.FileName;

                if (file.Length > 0)
                {
                    string path = $"{_config["FilePath:FileRooTPath"]}/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + fileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }

                return fileName;
            }
            else
            {
                throw new ApplicationException(" file not found ");
            }
        }   
            

            //public List<UploadEntity> GetProfile(IFormFile file)
            //{
            //    string path = $"{_config["FilePath:FileRooTPath"]}/{Path.GetRandomFileName()}.jpg";
            //    var filepath = path + file + ".jpg";
            //    if (System.IO.File.Exists(filepath)
            //    {
            //        byte[] b = System.IO.File.ReadAllBytes(filepath);
            //        return File(b);
            //    }

            //}
       public List<EmployeeList> GetUserDetail()
        {  
            return _userDetailDataAccess.GetUserDetail();
        }
        public UserInformationEntity GetUserDetailInfo(int pkId) 
        {
            var userDetails = _userDetailDataAccess.GetUserDetailInfo(pkId);
            userDetails.SkillList = new List<string>();
            foreach (var skill in userDetails.Skills.Split(','))
            {
                userDetails.SkillList.Add(skill);

            }
            return userDetails;
        } 
        public void InActiveinfo(int pkId, int userId) 
        {
            _userDetailDataAccess.InActiveinfo(pkId, userId);
        }
        public void Activeinfo(int pkId, int userId)
        {
            _userDetailDataAccess.Activeinfo(pkId, userId);
        }
        public List<ClientDetails> projectInfo()
        {
          return _userDetailDataAccess.projectInfo();
        }
        public List<ProjectListInfo> getProjectList(int userId)
        {
            return _userDetailDataAccess.getProjectList(userId);
        }
        public EmpDetails EmployeeDetails(int pkId)
        {
            return _userDetailDataAccess.EmployeeDetails(pkId);
        }
        public void AddProject(List<AddProject> addProject, int logInUserId)
        {
            _userDetailDataAccess.AddProject(addProject, logInUserId);
        }
    }
}
