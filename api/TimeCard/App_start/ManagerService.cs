using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.Interface;
using TimeCard.Manager;


namespace TimeCard.App_start
{
    public static class ManagerService
    {
        public static void Scope(IServiceCollection services)
        {

            services.AddScoped<IUserDetail, UserDetailManager>();
            services.AddScoped<IUserAccount, UserAccountManager>();
            services.AddScoped<IAppDomain, AppDomainManager>();
            services.AddScoped<IHoliday, HolidayManager>();
            services.AddScoped<IUserLeave, UserLeaveManager>();
            services.AddScoped<IProject, ProjectManager>();
            services.AddScoped<IProjectInvoice, ProjectInvoiceManager>();
            services.AddScoped<IClient, ClientManager>();
            services.AddScoped<IProjectUser, ProjectUserManager>();
            services.AddScoped<ITimeSheet, TimeSheetManager>();
            services.AddScoped<IDocs, DocsManager>();
            services.AddScoped<IGroup, GroupManager>();
            services.AddScoped<IAccessControl, AccessControlManager>();
            services.AddScoped<IUserGroup, UserGroupManager>();
            services.AddScoped<IGroupAccessControl, GroupAccessControlManager>();
            services.AddScoped<IEmailSend,EmailSender>();
            services.AddScoped<IApprove, ApproveManager>();

        }
    }
}
