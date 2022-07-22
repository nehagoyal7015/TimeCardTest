import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MailService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  mailSender(data: any) {
    return this.http.post(this.baseUrl + 'EmailSender', data);
  }
}
