import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  constructor(public router: Router) { }

  ngOnInit(): void {
  }

  // navigateWithState()
  // {
  //   this.router.navigateByUrl('/home/holiday', { state: { value: '0' } });
  // }

}
