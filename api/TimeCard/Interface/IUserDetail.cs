using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public  interface IUserDetail
    {
        void AddUserDetail(UserDetailEntity userDetail, string fileName, int userId);
        void UpdateUserDetail(UserDetailEntity userdetails,string fileName, int userId);
        List<EmployeeList> GetUserDetail();
        //List<UploadEntity> GetProfile(IFormFile file);
        string UserProfileUpload(IFormFile file );
        string UpdateUserProfile(IFormFile file);
        UserInformationEntity GetUserDetailInfo(int pkId);
        void InActiveinfo(int pkId, int userId);
        void Activeinfo(int pkId, int userId);
        List<ClientDetails> projectInfo();
        List<ProjectListInfo> getProjectList(int userId);
        EmpDetails EmployeeDetails(int pkId);
        void AddProject(List<AddProject> addProject, int logInUserId);
    }

}
