import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { HolidayComponent } from './home/holiday/holiday.component';
import { HomeComponent } from './home/home.component';
import { LeaveComponent } from './home/leave/leave.component';
import { LoginComponent } from './login/login.component';
import { MyprofileComponent } from './home/myprofile/myprofile.component';
import { ReportComponent } from './home/report/report.component';
import { UserActiveGuard } from './utils/login/user-active.guard';
import { AdminComponent } from './home/admin/admin.component';
import { EmployeeManageComponent } from './home/admin/employee-manage/employee-manage.component';
import { ProjectSetupComponent } from './home/admin/project-setup/project-setup.component';
import { EmployeeListComponent } from './home/admin/employee-manage/employee-list.component';
import { ClientProjectComponent } from './home/admin/project-setup/client-project/client-project.component';
import { ProjectAddEditComponent } from './home/admin/project-setup/project-add-edit/project-add-edit.component';
import { ProjectDetailsComponent } from './home/admin/project-setup/project-details/project-details.component';
import { DocumentComponent } from './home/document/document.component';
import { UserComponent } from './home/admin/security/user/user.component';
import { UsergroupComponent } from './home/admin/security/usergroup/usergroup.component';
import { GroupEmpComponent } from './home/admin/security/group-emp/group-emp.component';
import { GroupAccessControlComponent } from './home/admin/security/group-access-control/group-access-control.component';
import { ProjectReportComponent } from './home/report/project-report/project-report.component';
import { EmployeeReportComponent } from './home/report/employee-report/employee-report.component';
import { LeaveDetailsComponent } from './home/report/leave-details/leave-details.component';
import { TimeSheetReportComponent } from './home/report/time-sheet-report/time-sheet-report.component';
import { AsignProjectComponent } from './home/admin/employee-manage/asign-project/asign-project.component';
import { TimesheetprojectreportComponent } from './home/report/timesheetprojectreport/timesheetprojectreport.component';
import { ApprovalsComponent } from './home/report/approvals/approvals.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate:[UserActiveGuard], children: [
    { path: '', component: DashboardComponent },
    { path: 'leave', component: LeaveComponent },
    { path: 'holiday', component: HolidayComponent },
    { path: 'document', component: DocumentComponent },
    { path: 'report', component: ReportComponent },
    {path: 'report/pro-report', component: ProjectReportComponent,
        data: {
          title: 'Project Report',
          breadcrumb: [
            {
              label: 'Report',
              url: '/home/report'
            },
            
            {
              label: 'Project Report', 
              url: ''
            }
          ]
        },
      },
      {path: 'report/emp-report', component: EmployeeReportComponent,
      data: {
        title: 'Employee Report',
        breadcrumb: [
          {
            label: 'Report',
            url: '/home/report'
          },
          
          {
            label: 'Employee Report', 
            url: ''
          }
        ]
      },
    },
    {path: 'report/leave-report', component: LeaveDetailsComponent,
      data: {
        title: 'Leave Report',
        breadcrumb: [
          {
            label: 'Report',
            url: '/home/report'
          },
          
          {
            label: 'Leave Report', 
            url: ''
          }
        ]
      },
    },
    {path: 'report/time-sheet-report', component: TimeSheetReportComponent,
      data: {
        title: 'TimeSheet Report',
        breadcrumb: [
          {
            label: 'Report',
            url: '/home/report'
          },
          
          {
            label: 'TimeSheet Report', 
            url: ''
          }
        ]
      },
    },

    {path: 'report/approvals', component: ApprovalsComponent,
      data: {
        title: 'Approvals',
        breadcrumb: [
          {
            label: 'Report',
            url: '/home/report'
          },
          
          {
            label: 'Approvals', 
            url: ''
          }
        ]
      },
    },

    {path: 'report/timesheetprojectreport', component: TimesheetprojectreportComponent,
     
    },
    { path: 'myProfile', component: MyprofileComponent },
    { path: 'Admin', component: AdminComponent},
    {path: 'Admin/empManag', component: EmployeeManageComponent,
    data: {
      title: 'page1',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        {
          label: 'Employee List',// pageTwoID Parameter value will be add 
          url: '/home/Admin/empList'
        },
        {
          label: 'Add Employee', 
          url: ''
        },
       
      ]
    },
  },
 {path: 'Admin/empList', component: EmployeeListComponent,
    data: {
      title: 'emp List',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        
        {
          label: 'Employee List', 
          url: ''
        }
      ]
    },
  },
  {path: 'Admin/asignProject', component: AsignProjectComponent,
  data: {
    title: 'page1',
    breadcrumb: [
      {
        label: 'Admin',
        url: '/home/Admin'
      },
      {
        label: 'Employee List',// pageTwoID Parameter value will be add 
        url: '/home/Admin/empList'
      },
      {
        label: 'Asigned Project', 
        url: ''
      },
     
    ]
  },
  },
    {path: 'Admin/proj-setp', component: ProjectSetupComponent,
    data: {
      title: 'proj Setp',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        {
          label: 'Project Setup', 
          url: ''
        }
      ]
    },
  },
    {path: 'Admin/proj-setp/client-project', component: ClientProjectComponent,
    data: {
      title: 'Client',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        {
          label: 'client', 
          url: ''
        }
      ]
    },
  },
    {path: 'Admin/proj-setp/project-add-edit', component: ProjectAddEditComponent,
    data: {
      title: 'page1',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        {
          label: 'Project Setup',// pageTwoID Parameter value will be add 
          url: '/home/Admin/proj-setp'
        },
        {
          label: 'Project Add/Edit', 
          url: ''
        }
      ]
    },
  },
    {path: 'Admin/proj-setp/project-details', component: ProjectDetailsComponent,
    data: {
      title: 'page1',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        {
          label: 'Project Setup',// pageTwoID Parameter value will be add 
          url: '/home/Admin/proj-setp'
        },
        {
          label: 'Project Details', 
          url: ''
        }
      ]
    },
  },
  { path: 'Admin/security/user', component: UserComponent,
    data: {
      title: 'user',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin',
        },
        {
          label: 'Group Emp',
          url: '/home/Admin/security/group-emp',
        },
        {
          label: 'user', 
          url: ''
        }
      ]
    },
  },
  { path: 'Admin/security/group-emp', component: GroupEmpComponent,
    data: {
      title: 'group emp',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        {
          label: 'Group Emp', 
          url: ''
        }
      ]
    },
  },
  { path: 'Admin/security/group', component: UsergroupComponent,
    data: {
      title: 'user Group',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        {
          label: 'User Group', 
          url: ''
        }
      ]
    },
  },
  { path: 'Admin/security/group-access-control', component: GroupAccessControlComponent,
    data: {
      title: 'user Group',
      breadcrumb: [
        {
          label: 'Admin',
          url: '/home/Admin'
        },
        {
          label: 'Group Emp',
          url: '/home/Admin/security/group',
        },
        {
          label: 'Group Access Control', 
          url: ''
        }
      ]
    },
  },
  ] },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

 }
