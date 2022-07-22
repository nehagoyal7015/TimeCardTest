import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-client-project',
  templateUrl: './client-project.component.html',
  styleUrls: ['./client-project.component.scss']
})
export class ClientProjectComponent implements OnInit {

  constructor( private router: Router) { }

  ngOnInit(): void {
  }

  addNewProj() {
    this.router.navigate(['/home/admin/proj-setp/project-add-edit']);
  }

  
}
