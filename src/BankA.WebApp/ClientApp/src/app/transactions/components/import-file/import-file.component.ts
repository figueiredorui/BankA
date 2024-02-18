import { Component, Inject, Injector, OnInit, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { TransactionsService } from '../../services/transactions.service';
import { Account } from '../../../accounts/models/accounts.model';


@Component({
  selector: 'app-import-file',
  templateUrl: './import-file.component.html',
  styleUrls: ['./import-file.component.scss']
})
export class ImportFileComponent extends BaseComponent implements OnInit {
  @ViewChild('fileUpload') fileUpload : any;
  account: Account;
  fileControl: FormControl = new FormControl('', Validators.required);
  selectedFiles?: FileList;

  get IsFileSelected(): boolean {
    return this.selectedFiles != null;
  }

  constructor(private injector: Injector,
		public dialogRef: MatDialogRef<ImportFileComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private transactionsService: TransactionsService) {
		super(injector);

		this.account = data.account;
	}

  public ngOnInit() {
    console.log('ImportFileComponent');
  }

  public onFileSelected(event:any) {
    this.selectedFiles = event.target.files;
    this.fileControl.patchValue(`${this.selectedFiles?.item(0)?.name}`);
  }
  
  public async import() {
    try {
      this.loading = true;
      this.errorMessage = undefined;
      const file = this.selectedFiles?.item(0);
      if (file && this.account){
        await this.transactionsService.importFile(this.account.Id, file);
        this.dialogRef.close(true);
      }
      else{
        this.fileControl.markAsTouched();
        this.errorMessage = 'Populate required fields';
      }

    } catch (error: any) {
     
      this.errorMessage = error;
    } finally {
      this.fileControl.reset();
      this.fileUpload.nativeElement.value = null;
      this.loading = false;
    }
  }

  public cancel() {
    this.dialogRef.close(false);
  }

}


