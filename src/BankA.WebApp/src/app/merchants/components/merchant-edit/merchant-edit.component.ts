import { Component, OnInit, Injector, Inject } from '@angular/core';
import { Merchant } from '../../models/merchants.model';
import { MerchantService } from '../../services/merchants.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
	selector: 'app-merchant-edit',
	templateUrl: './merchant-edit.component.html',
	styleUrls: ['./merchant-edit.component.scss']
})
export class MerchantEditComponent extends BaseComponent implements OnInit {
	merchantId: number = 0;
	merchant = {} as Merchant;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<MerchantEditComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private merchantService: MerchantService) {
		super(injector);
		this.merchantId = data.merchantId;
	}

	async ngOnInit() {
		this.loadMerchant();
	}

	cancel() {
		this.dialogRef.close();
	}

	async save() {
		try {
			this.loading = true;
			const merchantReq = new Merchant();
			merchantReq.MerchantName = this.merchant.MerchantName;
			await this.merchantService.update(this.merchantId, merchantReq);
			this.dialogRef.close(true);
		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	async delete() {
		try {
			const confirm = await this.dialogService.showConfirmation('Would you like to delete this merchant?');
			if (confirm) {
				await this.merchantService.delete(this.merchantId);
				this.dialogRef.close(true);
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	private async loadMerchant() {
		try {
			this.loading = true;
			this.merchant = await this.merchantService.get(this.merchantId);
		} catch (err: any) {
			this.errorMessage = err.Message;
		} finally {
			this.loading = false;
		}
	}
}
