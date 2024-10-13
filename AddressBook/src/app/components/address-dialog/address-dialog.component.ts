import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-address-dialog',
  templateUrl: './address-dialog.component.html',
  styleUrls: ['./address-dialog.component.css']
})
export class AddressDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { name: string; address: string },
    private dialogRef: MatDialogRef<AddressDialogComponent>) {}

  close(): void {
    this.dialogRef.close();
  }
}
