import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-inactive-dialog',
  templateUrl: './inactive-dialog.component.html',
  styleUrls: ['./inactive-dialog.component.scss']
})
export class InactiveDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<InactiveDialogComponent>) { }

  ngOnInit(): void {
  }

  closeDialog() {
    this.dialogRef.close(false);
  }

}
