import { Component, Inject, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { AccountService } from '../../services/accounts.service';

@Component({ standalone: false,
	selector: 'app-account-add',
	templateUrl: './account-add.component.html',
	styleUrls: ['./account-add.component.scss']
})
export class AccountAddComponent extends BaseComponent implements OnInit {
	public fileFormats: string[] = [];
	form: UntypedFormGroup;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<AccountAddComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private accountService: AccountService) {
		super(injector);

		this.form = new FormGroup({
			Description: new FormControl('', [Validators.required]),
			FileFormat: new FormControl(null, [Validators.required]),
		});
	}

	ngOnInit() {
		this.loadFileFormats();
	}

	async saveAccount() {
		try {
			this.loading = true;

			const request = this.form.value;
			await this.accountService.addAccount(request);
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

	private async loadFileFormats() {
		try {
			this.fileFormats = await this.accountService.getFileFormatsList();
		} catch (error: any) {
			this.errorMessage = error;
		}
	}


}
