import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef
} from '@angular/material/dialog';

@Component({
  selector: 'app-information-dialog',
  templateUrl: './information-dialog.component.html',
  styleUrls: ['./information-dialog.component.scss']
})
export class InformationDialogComponent {
  message: any;
  constructor(
    public dialogRef: MatDialogRef<InformationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
  ) {
    this.message = data.message;
  }

  public close() {
    this.dialogRef.close(true);
  }
}
