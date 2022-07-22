import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-confirm-dialoge',
  templateUrl: './delete-confirm-dialoge.component.html',
  styleUrls: ['./delete-confirm-dialoge.component.scss']
})
export class DeleteConfirmDialogeComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteConfirmDialogeComponent>) { }

  ngOnInit(): void {
  }

  closeDialog() {
    this.dialogRef.close(false);
  }

}
