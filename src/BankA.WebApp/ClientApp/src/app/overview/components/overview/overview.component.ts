import { Component, Injector, OnInit } from '@angular/core';
import { AccountService } from '../../../accounts/services/accounts.service';
import { BaseComponent } from '../../../shared/components/base/base.component';

@Component({
	selector: 'app-overview',
	templateUrl: './overview.component.html',
	styleUrls: ['./overview.component.scss']
})
export class OverviewComponent extends BaseComponent implements OnInit {
	accountId: number = 0;
	period: number = 0;
	periodOptions = new Map();

	constructor(private injector: Injector, private accountService: AccountService) {
		super(injector);

		this.periodOptions.set(0, 'This Year');
		this.periodOptions.set(1, 'Last Year');
		this.periodOptions.set(12, 'Last 12 Months');
		this.periodOptions.set(24, 'Last 24 Months');
	}

	ngOnInit() {

		this.activatedRoute.params.subscribe((parameter: any) => {
			console.log('OverviewComponent ' + parameter.id);
			if (parameter.id) {
				this.accountId = parameter.id;

				const cache = localStorage.getItem('period');
				if (cache != null) {
					this.period = parseInt(cache);
				}
				else {
					this.period = 12;
				}
			}
		});
	}

	public async changePeriod(periodId: number) {
		this.period = periodId;
		localStorage.setItem('period', periodId.toString());
	}
}
