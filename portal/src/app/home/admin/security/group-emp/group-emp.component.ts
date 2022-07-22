import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Router} from '@angular/router';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

import { EmployeeService } from './../../../../Services/employee.service';
import { LoaderServiceService } from './../../../../utils/loader/loader-service.service';
import { UserGroupService } from 'src/app/Services/user-group.service';

@Component({
  selector: 'app-group-emp',
  templateUrl: './group-emp.component.html',
  styleUrls: ['./group-emp.component.scss']
})
export class GroupEmpComponent implements OnInit {
  public searchValueEmpCode = '';
  public searchValueEmpName = '';
  public searchValueEmpEmail = '';
  private searchValue = '';
  displayedColumns = ['name','empId','joiningDate','emailId','phoneNumber', 'edit'];
  
  // dataSource: MatTableDataSource<empListIsActive>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
   dataSource: MatTableDataSource<any>;
  constructor(private router: Router, public empService: EmployeeService, private loaderService: LoaderServiceService, private userGroupService: UserGroupService) {}

  ngOnInit(): void {
    this.empList();
  }

  searchEmp(){
    
    if(this.searchValueEmpCode) {
      this.searchValue = this.searchValueEmpCode;
      this.applyFilter();
    } else if(this.searchValueEmpName){
      this.searchValue = this.searchValueEmpName;
      this.applyFilter();
    } else if(this.searchValueEmpEmail){
      this.searchValue = this.searchValueEmpEmail;
      this.applyFilter();
    } else {
      this.searchValue = '';
      this.applyFilter();
    }
  }

  blankFilter(event:any) {
    
    const notValue = (event.target as HTMLInputElement).value;
    if(!notValue) {
      this.searchValue = '';
      this.applyFilter();
    }
  }

  applyFilter() {
    const filterValue = this.searchValue;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  AssignEmp(emoDtls:any) {
    
    this.userGroupService.setEmpData(emoDtls);
    this.router.navigate(['/home/Admin/security/user']);
  }



   /**
   * @name: empList
   * @description: emp List Table Data
   */

  
    empList() {
      this.loaderService.emitLoadingChangeEvent(true);
      this.empService.empDetailList().subscribe( (result:any) => {
        if(result.success) {
          this.dataSource = new MatTableDataSource(result.data);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        } else {}
        this.loaderService.emitLoadingChangeEvent(false);
      }, (error)  => {
        this.loaderService.emitLoadingChangeEvent(false);
      }
      )
    }
    


}
