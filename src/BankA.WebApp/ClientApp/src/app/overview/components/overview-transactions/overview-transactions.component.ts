import { Component, Inject, Injector } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { TransactionPagedListRequest } from '../../../transactions/models/transaction-paged-list-request.model';
import { ReportsService } from '../../services/reports.service';

@Component({
	selector: 'app-overview-transactions',
	templateUrl: './overview-transactions.component.html',
	styleUrls: ['./overview-transactions.component.scss']
})
export class OverviewTransactionsComponent extends BaseComponent {
	accountId: number = 0;
	title: string = '';

	filter = new TransactionPagedListRequest();

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<OverviewTransactionsComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private reportService: ReportsService) {
		super(injector);

		this.accountId = data.accountId;
		this.title = data.title;

		this.filter.Period = data.period;
		this.filter.CategoryId = data.categoryId;
		this.filter.MerchantId = data.merchantId;
		this.filter.CategoryType = data.categoryType;
	}

	close() {
		this.dialogRef.close(true);
	}
}
