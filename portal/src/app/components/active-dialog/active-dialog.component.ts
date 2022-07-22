import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'app-active-dialog',
  templateUrl: './active-dialog.component.html',
  styleUrls: ['./active-dialog.component.scss']
})
export class ActiveDialogComponent implements OnInit {


  constructor(public dialogRef: MatDialogRef<ActiveDialogComponent>) { }

  ngOnInit(): void {
  }

  closeDialog() {
    this.dialogRef.close(false);
  }


}
