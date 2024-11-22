import { Component, Injector, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import * as _ from 'lodash';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { ReportsService } from '../../services/reports.service';
import { OverviewTransactionsComponent } from '../overview-transactions/overview-transactions.component';

@Component({ standalone: false,
	selector: 'app-merchant-summary',
	templateUrl: './merchant-summary.component.html',
	styleUrls: ['./merchant-summary.component.scss']
})
export class MerchantSummaryComponent extends BaseComponent implements OnInit, OnChanges {
	@Input()
	accountId: number = 0;

	@Input()
	period: number = 0;

	merchantSummaryList: any[] = [];

	constructor(private injector: Injector, private reportService: ReportsService) {
		super(injector);
	}

	ngOnInit() {
		this.errorMessage = undefined;
	}

	ngOnChanges(changes: SimpleChanges) {
		if (changes['accountId'] || changes['period']) {
			this.loadData(this.accountId, this.period);
		}
	}

	private async loadData(accountId: number, period: any) {
		try {
			if (period == null)
				return;

			this.loading = true;
			const merchantSummary = await this.reportService.getMerchantSummary(accountId, period);
			const categoryTypeGroup = _.groupBy(merchantSummary, 'CategoryType');

			this.merchantSummaryList = [];

			this.merchantSummaryList.push({
				categoryType: 'Income',
				merchantTotal: _.orderBy(categoryTypeGroup['Income'], 'Amount', 'desc')
			});

			this.merchantSummaryList.push({
				categoryType: 'Expense',
				merchantTotal: _.orderBy(categoryTypeGroup['Expense'], 'Amount', 'asc')
			});

			this.merchantSummaryList.push({
				categoryType: 'Transfer',
				merchantTotal: _.orderBy(categoryTypeGroup['Transfer'], 'Amount', 'desc')
			});


		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	async showOverview(merchantId: number, merchantName: string, categoryType: string) {
		try {
			const result = await this.dialogService.openFullScreen(OverviewTransactionsComponent, {
				accountId: this.accountId,
				period: this.period,
				merchantId: merchantId,
				categoryType: categoryType,
				title: merchantName
			});

			if (result == true) {
				this.loadData(this.accountId, this.period);
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	getTotal(merchants: any[]) {
		const sum = merchants?.reduce((sum, current) => sum + current.Amount, 0);
		return sum;
	}
}

