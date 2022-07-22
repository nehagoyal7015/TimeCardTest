using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class UserDetailEntity
    {
        public int PK_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Desgination { get; set; }
        public DateTime? JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string AlternateNumber { get; set; }
        public string PersonalEmail { get; set; }
        public string SkypeId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool? IsMaritalstatus { get; set; }
        public string SpouseName { get; set; }
        public string SpousePhone { get; set; }
        public string SpouseEmail { get; set; }
        public string Profile { get; set; }
        public List<string> SkillList { get; set; }
        public string EmpId { get; set; }
        public int DomainId { get; set; }
        public int? Ssoid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public string DomainName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int ClientCode { get; set; }

    }

    public class UploadEntity
    {
        public string data { get; set; }
        public IFormFile File { get; set; }
    }
    public class EmployeeList
    {
        public int UserId { get; set; }
        public int PK_Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string EmpId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }
    public class UserInformationEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Desgination { get; set; }
        public DateTime? JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string AlternateNumber { get; set; }
        public string PersonalEmail { get; set; }
        public string SkypeId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool? IsMaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public string SpousePhone { get; set; }
        public string SpouseEmail { get; set; }
        public List<string> SkillList { get; set; }
        public string Skills { get; set; }
        public string EmpId { get; set; }
        public string Ssoid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
    }

    public class EmployeeDetails
    {
      public string EmployeeName { get; set;}
      public DateTime EnteredTime { get; set; } // Modifcation On
       
    }
    public class DomainDetails
    {
     public int DomainId { get; set; }
    }
    public class EmpDetails
    {
        public int UserId { get; set;}
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string EmpCode { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ClientDetails
    {
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int Total { get; set; }
        public int asignTotal { get; set; }
        public List<ProjectInfo> Projectdata { get; set; }
    }
    public class ProjectInfo
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool IsSelected { get; set; }
       
    }

    public class ProjectListInfo
    {
        public int ProjectId  { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
    }

    public class AddProject
    {
      public int ProjectId { get; set; }
      public int UserId { get; set; }
    }
   

}
