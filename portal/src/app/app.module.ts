import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatRadioModule} from '@angular/material/radio';
import { AppRoutingModule } from './app-routing.module';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import {NgDynamicBreadcrumbModule} from "ng-dynamic-breadcrumb";
import {
  GoogleLoginProvider,
  FacebookLoginProvider
} from 'angularx-social-login';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatButtonModule} from '@angular/material/button';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatTableModule} from '@angular/material/table';
import {MatDialogModule} from '@angular/material/dialog';
import {CdkTableModule} from '@angular/cdk/table';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule } from '@angular/material/core';
import {MatListModule} from '@angular/material/list';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatMenuModule} from '@angular/material/menu';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatChipsModule} from '@angular/material/chips';
import {MatExpansionModule} from '@angular/material/expansion';
import { MatPaginatorModule } from '@angular/material/paginator';
import {DragDropModule} from '@angular/cdk/drag-drop';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatSnackBar} from '@angular/material/snack-bar';

import { AppComponent } from './app.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { LeaveComponent } from './home/leave/leave.component';
import { HolidayComponent } from './home/holiday/holiday.component';
import { ReportComponent } from './home/report/report.component';
import { MyprofileComponent } from './home/myprofile/myprofile.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

import { HolidayService } from './Services/holiday.service';
import { AuthorizationInterceptorInterceptor} from './utils/interceptor/authorization-interceptor.interceptor';
import { HttpErrorInterceptorServiceInterceptor} from './utils/interceptor/http-error-interceptor-service.interceptor';
import { LeaveServiceService } from './Shared/leave-service.service';
import { MatConfirmDialogComponent } from './Material/mat-confirm-dialog/mat-confirm-dialog.component';
import { NumberDirective } from './utils/directive/number.directive';
import { TwoDigitDecimaNumberDirective } from './utils/directive/two-digit-decima-number.directive';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { AdminComponent } from './home/admin/admin.component';
import { ProjectSetupComponent } from './home/admin/project-setup/project-setup.component';
import { EmployeeManageComponent } from './home/admin/employee-manage/employee-manage.component';
import { AuthService } from './utils/login/auth-service.service';
import { CustomCalendarComponent } from './components/custom-calendar/custom-calendar.component';
import { EmployeeListComponent } from './home/admin/employee-manage/employee-list.component';
import { UniquePipe } from './components/pipe/unique-pipe.pipe';
import { DeleteConfirmDialogeComponent } from './components/delete-confirm-dialoge/delete-confirm-dialoge.component';
import { Clients } from './home/dashboard/project.model';
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
import {MatBadgeModule} from '@angular/material/badge'
import { LeaveDetailsComponent } from './home/report/leave-details/leave-details.component';
import { TimeSheetReportComponent } from './home/report/time-sheet-report/time-sheet-report.component';
import { ActiveDialogComponent } from './components/active-dialog/active-dialog.component';
import { InactiveDialogComponent } from './components/inactive-dialog/inactive-dialog.component';
import { AsignProjectComponent } from './home/admin/employee-manage/asign-project/asign-project.component';
import { TimesheetprojectreportComponent } from './home/report/timesheetprojectreport/timesheetprojectreport.component';
import {MatTreeModule} from '@angular/material/tree';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { ApprovalsComponent } from './home/report/approvals/approvals.component';

// import { GoogleLoginProvider } from 'angularx-social-login';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LeaveComponent,
    HolidayComponent,
    ReportComponent,
    MyprofileComponent,
    LoginComponent,
    HomeComponent,
    MatConfirmDialogComponent,
    NumberDirective,
    TwoDigitDecimaNumberDirective,
    AdminComponent,
    ProjectSetupComponent,
    EmployeeManageComponent,
    CustomCalendarComponent,
    EmployeeListComponent,
    UniquePipe,
    DeleteConfirmDialogeComponent,
    ClientProjectComponent,
    ProjectAddEditComponent,
    ProjectDetailsComponent,
    DocumentComponent,
    UserComponent,
    UsergroupComponent,
    GroupEmpComponent,
    GroupAccessControlComponent,
    ProjectReportComponent,
    EmployeeReportComponent,
    LeaveDetailsComponent,
    TimeSheetReportComponent,
    ActiveDialogComponent,
    InactiveDialogComponent,
    AsignProjectComponent,
    TimesheetprojectreportComponent,
    ApprovalsComponent,
    

    
    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    MatSidenavModule,
    MatGridListModule,
    MatButtonModule,
    MatAutocompleteModule,
    MatButtonToggleModule,
    MatProgressBarModule,
    MatIconModule,
    FlexLayoutModule,
    MatSelectModule,
    MatInputModule,
    MatRadioModule,
    MatCardModule,
    MatTableModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatListModule,
    MatSlideToggleModule,
    MatMenuModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatChipsModule,
    MatPaginatorModule,
    DragDropModule,
    MatProgressSpinnerModule,
    CdkTableModule,
    SocialLoginModule,
    NgDynamicBreadcrumbModule,
    MatBadgeModule,
    MatTreeModule,
    NgxMatSelectSearchModule
  ],
  providers: [ 
    {
      provide: 'SocialAuthServiceConfig',
     useValue: {
      autoLogin: false,
      //keeps the user signed in
      providers: [
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider('812153920315-5nbj51m1donin9h8bghhobc2c095i983.apps.googleusercontent.com') // your client id
        }
      ]}
    },

    {provide : HTTP_INTERCEPTORS, useClass : AuthorizationInterceptorInterceptor, multi: true},
    {provide : HTTP_INTERCEPTORS, useClass : HttpErrorInterceptorServiceInterceptor, multi: true},
    
     HolidayService, LeaveServiceService, MatSnackBar,AuthService],

  
  bootstrap: [AppComponent],
  entryComponents:[MatConfirmDialogComponent]


})
export class AppModule { }
