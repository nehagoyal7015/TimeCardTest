using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;   
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;

namespace TimeCard.App_start
{
    public static class DataBaseService
    {
        public static void Scope(IServiceCollection services)
        {

            services.AddScoped<BaseDataAccess>();
            services.AddScoped<UserDetailDataAccess>();
            services.AddScoped<UserAccountDataAccess>();
            services.AddScoped<AppDomainDataAccess>();
            services.AddScoped<HolidayDataAccess>();
            services.AddScoped<UserLeaveDataAccess>();
            services.AddScoped<ProjectDataAccess>();
            services.AddScoped<ProjectInvoiceDataAccess>();
            services.AddScoped<ProjectUserDataAccess>();
            services.AddScoped<ClientDataaAccess>();
            services.AddScoped<DocsDataAccess>();
            services.AddScoped<TimeSheetDataAccess>();
            services.AddScoped<GroupDataAccess>();
            services.AddScoped<AccessControlDataAccess>();
            services.AddScoped<UserGroupDataAccess>();
            services.AddScoped<GroupAccessControlDataAccess>();
            // services.AddScoped <ProjectDetailDataAccess>();
            services.AddScoped<ApproveDataAccess>();
        }

            

    }
}
