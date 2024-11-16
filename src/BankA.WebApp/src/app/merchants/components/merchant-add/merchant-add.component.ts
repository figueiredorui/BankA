import { Component, OnInit, Injector, Inject } from '@angular/core';
import { Merchant } from '../../models/merchants.model';
import { MerchantService } from '../../services/merchants.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
	selector: 'app-merchant-add',
	templateUrl: './merchant-add.component.html',
	styleUrls: ['./merchant-add.component.scss']
})
export class MerchantAddComponent extends BaseComponent implements OnInit {
	merchant = {} as Merchant;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<MerchantAddComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private merchantService: MerchantService) {
		super(injector);
	}

	async ngOnInit() {
		console.log('MerchantAddComponent');
	}

	cancel() {
		this.dialogRef.close();
	}

	async save() {
		try {
			this.loading = true;
			const merchantReq = new Merchant();
			merchantReq.MerchantName = this.merchant.MerchantName;

			await this.merchantService.add(merchantReq);
			this.dialogRef.close(true);
		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}
}
