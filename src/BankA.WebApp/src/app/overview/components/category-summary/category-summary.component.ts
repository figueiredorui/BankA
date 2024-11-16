import { Component, Injector, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import * as _ from 'lodash';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { ReportsService } from '../../services/reports.service';
import { OverviewTransactionsComponent } from '../overview-transactions/overview-transactions.component';


@Component({
	selector: 'app-category-summary',
	templateUrl: './category-summary.component.html',
	styleUrls: ['./category-summary.component.scss']
})
export class CategorySummaryComponent extends BaseComponent implements OnInit, OnChanges {
	@Input()
	accountId: number = 0;

	@Input()
	period: number = 0;

	categorySummaryList: any[] = [];

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
			const categorySummary = await this.reportService.getCategorySummary(accountId, period);
			const categoryTypeGroup = _.groupBy(categorySummary, 'CategoryType');

			this.categorySummaryList = [];

			this.categorySummaryList.push({
				categoryType: 'Income',
				categoryTotal: _.orderBy(categoryTypeGroup['Income'], 'Amount', 'desc')
			});

			this.categorySummaryList.push({
				categoryType: 'Expense',
				categoryTotal: _.orderBy(categoryTypeGroup['Expense'], 'Amount', 'asc')
			});

			this.categorySummaryList.push({
				categoryType: 'Transfer',
				categoryTotal: _.orderBy(categoryTypeGroup['Transfer'], 'Amount', 'desc')
			});


		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	async showOverview(categoryId: number, categoryName: string, categoryType: string) {
		try {
			const result = await this.dialogService.openFullScreen(OverviewTransactionsComponent, {
				accountId: this.accountId,
				period: this.period,
				categoryId: categoryId,
				categoryType: categoryType,
				title: categoryName
			});

			if (result == true) {
				this.loadData(this.accountId, this.period);
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	getTotal(categories: any[]) {
		const sum = categories?.reduce((sum, current) => sum + current.Amount, 0);
		return sum;
	}
}

