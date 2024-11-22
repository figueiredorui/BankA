import { Component, Inject, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { AccountService } from '../../services/accounts.service';

@Component({ standalone: false,
	selector: 'app-account-edit',
	templateUrl: './account-edit.component.html',
	styleUrls: ['./account-edit.component.scss']
})
export class AccountEditComponent extends BaseComponent implements OnInit {
	public fileFormats: string[] = [];
	accountId: number = 0;
	form: UntypedFormGroup;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<AccountEditComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private accountService: AccountService) {
		super(injector);

		this.accountId = data.accountId;

		this.form = new FormGroup({
			Id: new FormControl(this.accountId),
			Description: new FormControl('', [Validators.required]),
			FileFormat: new FormControl(null, [Validators.required]),
			Closed: new FormControl('', [Validators.required]),
		});
	}

	ngOnInit() {
		this.loadFileFormats();
		this.loadAccount(this.accountId);
	}

	async saveAccount() {
		try {
			this.loading = true;

			const request = this.form.value;
			await this.accountService.updateAccount(request);
			this.dialogRef.close(true);
		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	cancel() {
		this.dialogRef.close();
	}

	async delete() {
		const confirm = await this.dialogService.showConfirmation('Would you like to delete this Account?');
		if (confirm) {
			await this.accountService.deleteAccount(this.accountId);
			this.dialogRef.close(true);
		}

	}

	private async loadAccount(id: number) {
		try {
			this.loading = true;
			const account = await this.accountService.getAccount(id);

			this.form.controls['Id'].setValue(account.Id);
			this.form.controls['Description'].setValue(account.Description);
			this.form.controls['FileFormat'].setValue(account.FileFormat);
			this.form.controls['Closed'].setValue(account.Closed);

		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}


	private async loadFileFormats() {
		try {
			this.fileFormats = await this.accountService.getFileFormatsList();
		} catch (error: any) {
			this.errorMessage = error;
		}
	}


}
