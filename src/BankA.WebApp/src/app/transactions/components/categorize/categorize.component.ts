import { Component, Inject, Injector, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { CategoriseReport } from '../../models/categorise-report.model';
import { TransactionsService } from '../../services/transactions.service';
import { TransactionPagedListRequest } from '../../models/transaction-paged-list-request.model';

@Component({
	selector: 'app-categorize',
	templateUrl: './categorize.component.html',
	styleUrls: ['./categorize.component.scss']
})
export class CategorizeComponent extends BaseComponent implements OnInit {
	accountId: number = 0;
	categorizing: boolean = true;
	filter = new TransactionPagedListRequest();
	report: CategoriseReport | undefined;

	private hasChanges = false;

	constructor(private injector: Injector,
		public dialogRef: MatDialogRef<CategorizeComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private transactionsService: TransactionsService,) {
		super(injector);

		this.accountId = data.accountId;
		this.filter.Uncategorized = true;
	}

	async ngOnInit() {
		await this.categorizeTransactions();
	}

	async onRuleAdded(){
		await this.categorizeTransactions();
	}

	onTransactionUpdated(){
		this.hasChanges = true;
	}

	close() {
		this.dialogRef.close(this.hasChanges);
	}

	private async categorizeTransactions() {
		try {
			this.loading = true;
			this.categorizing = true;
			this.report = await this.transactionsService.categorizeTransaction(this.accountId);
		} catch (err: any) {
			this.errorMessage = err.Message;
		} finally {
			this.categorizing = false;
			this.loading = false;
		}
	}
}
