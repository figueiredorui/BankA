import { Component, Injector, OnInit } from '@angular/core';
import { AccountBalance } from '../../../transactions/models/accounts.model';
import { TransactionsService } from '../../../transactions/services/transactions.service';
import { BaseComponent } from '../../components/base/base.component';
import { environment } from '../../../../environments/environment';

@Component({
	selector: 'app-container',
	templateUrl: './container.component.html',
	styleUrls: ['./container.component.scss']
})
export class ContainerComponent extends BaseComponent implements OnInit {
	accountId: number = 0;
	accounts: AccountBalance[] = [];
	accountSelected: AccountBalance | undefined;
	selectedTabIndex: any;
	version = environment.version;

	constructor(private injector: Injector, private transactionsService: TransactionsService) {
		super(injector);
	}

	async ngOnInit() {

		await this.loadAccountBalance();
		this.activatedRoute.children[0]?.params.subscribe((parameter: any) => {
			console.log('ContainerComponent ' + parameter.id);
			if (parameter.id) {
				this.accountId = parameter.id;
			}
			else {
				if (this.accounts.length > 0) {
					this.accountId = this.accounts[0].Id;
					this.router.navigate(['overview', this.accountId]);
				}
			}
			this.selectAccountBy(this.accountId);
		});

		this.transactionsService.onBalanceUpdated().subscribe(async () => {
			await this.loadAccountBalance();
			this.selectAccountBy(this.accountId);
		});
	}

	public selectAccountBy(accountId: number) {
		this.accountSelected = this.accounts.find(a => a.Id == accountId);
	}

	public selectAccount(account: AccountBalance) {
		this.accountSelected = account;
	}

	private async loadAccountBalance() {
		try {
			this.accounts = await this.transactionsService.getAccountBalanceFromCacheOrApi();

			if (this.accounts.length === 0)
				this.noRecords = true;
		} catch (error: any) {
			this.errorMessage = error;
		}
	}
}