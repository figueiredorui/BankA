import { Component, Injector, Input, NgZone, OnChanges, OnInit, ViewChild } from '@angular/core';
import { ChartjsComponent } from '@ctrl/ngx-chartjs';
import type { ChartOptions } from 'chart.js';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { CashFlow } from '../../models/cashflow.model';
import { ReportsService } from '../../services/reports.service';

@Component({
	selector: 'app-cashflow-chart',
	templateUrl: './cashflow-chart.component.html',
	styleUrls: ['./cashflow-chart.component.scss']
})
export class CashflowChartComponent extends BaseComponent implements OnInit, OnChanges {
	@ViewChild('chart', { static: false }) chart: ChartjsComponent | undefined;

	public chartData: any = null;

	@Input()
	accountId: number = 0;

	@Input()
	period: number = 0;

	private cashFlow: CashFlow[] = [];

	constructor(private injector: Injector, private ngZone: NgZone, private reportService: ReportsService) {
		super(injector);
	}

	ngOnChanges() {
		if (this.accountId > 0 && this.period >= 0) {
			this.loadData(this.accountId, this.period);
		}
	}

	ngOnInit() {
		console.log('CashflowChartComponent');
	}

	private async loadData(accountId: number, period: number) {
		try {
			this.loading = true;
			this.cashFlow = await this.reportService.getCashflow(accountId, period);
			this.chartData = {
				labels: this.cashFlow.map(m => m.MonthYear),
				datasets: [
					{
						data: this.cashFlow.map(m => m.DebitAmount),
						label: 'Expenses',
						backgroundColor: 'rgba(255, 99, 132, 0.5)',
						borderColor: 'rgba(255, 99, 132, 1)',
						cubicInterpolationMode: 'monotone',
						barPercentage: 0.7,
						categoryPercentage: 0.4,
						borderRadius: 50,
					},
					{
						data: this.cashFlow.map(m => m.CreditAmount),
						label: 'Income',
						backgroundColor: 'rgba(75, 192, 192, 0.5)',
						borderColor: 'rgba(75, 192, 192, 1)',
						cubicInterpolationMode: 'monotone',
						barPercentage: 0.7,
						categoryPercentage: 0.4,
						borderRadius: 50,
					}
				]
			};
		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	chartOptions: ChartOptions = {
		animation: false,
		responsive: true,
		maintainAspectRatio: false,
		scales: {
			x: {

				display: true,
				title: {
					display: true
				},
				grid: {

					display: false,
				}
			},
			y: {
				display: true,
				title: {
					display: true,
				},
				grid: {
					display: true,
				}
			}
		},
		plugins: {
			legend: {
				display: true
			},
		},
		onClick: async () => {
		}
	};

}
