import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class HolidayService {
  baseUrl =  environment.apiUrl; 

  constructor(private http: HttpClient) { }
  holiday(year: number): Observable<any> {
    return this.http.get(this.baseUrl + 'Holiday/get?year=' + year);    
  }

  addHoliday(data: any) {
    console.log(data);
    return this.http.post(this.baseUrl + 'Holiday/add', data);
  }

  opt(optingElement: any) {
    return this.http.post(this.baseUrl + 'Holiday/opt', optingElement);
  }

  editOpt(optingElement: any) {
    return this.http.put(this.baseUrl + 'Holiday/editOptHoliday', optingElement);
  }


  editHoliday(data:any) {
    
    return this.http.put(this.baseUrl + 'Holiday/edit', data);
  }

  deleteHoliday(holidayId: string) {
    return this.http.delete(this.baseUrl + 'Holiday/delete?id=' + holidayId );
  }
  
  GetUpcomingHoliday(): Observable<any> {
    return this.http.get(this.baseUrl + 'Holiday/getUpcoming');    
  }

  GetEmpOptHolidayList(floatingHolidayId: any): Observable<any> {
    return this.http.get(this.baseUrl + 'Holiday/getOptEmp?floatingHolidayId=' + floatingHolidayId);    
  }
}
