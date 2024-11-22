import { Component, OnInit, Injector } from '@angular/core';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { TransactionsService } from '../../services/transactions.service';
import { CategorizeComponent } from '../categorize/categorize.component';
import { ImportFileComponent } from '../import-file/import-file.component';
import { ImportFileCsvComponent } from '../import-file-csv/import-file-csv.component';
import { AccountService } from '../../../accounts/services/accounts.service';
import { Account } from '../../../accounts/models/accounts.model';
import { TransactionAddComponent } from '../transaction-add/transaction-add.component';

@Component({ standalone: false,
	selector: 'app-transactions',
	templateUrl: './transactions.component.html',
	styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent extends BaseComponent implements OnInit {
	accountId: number = 0;

	constructor(private injector: Injector,
		private accountService: AccountService,
		private transactionsService: TransactionsService) {
		super(injector);
	}

	ngOnInit() {
		this.activatedRoute.params.subscribe((parameter: any) => {
			console.log('TransactionsComponent ' + parameter.id);
			if (parameter.id)
				this.accountId = parameter.id;
		});
	}

	public async openImportFile() {
		try {
			const account = await this.accountService.getAccount(this.accountId);
			if (account.FileFormat == 'CSV') {
				await this.importCsvFile(account);
			}
			else {
				await this.importFile(account);
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	private async importFile(account: Account) {
		try {
			const result = await this.dialogService.open(ImportFileComponent, { account: account });
			if (result === true) {
				//await this.LoadTransactions();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	private async importCsvFile(account: Account) {
		try {
			const result = await this.dialogService.openFullScreen(ImportFileCsvComponent, { account: account });
			if (result === true) {
				//await this.LoadTransactions();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}


	async openCategorizeDialog() {
		try {
			const result = await this.dialogService.openFullScreen(CategorizeComponent, { accountId: this.accountId });
			if (result === true) {
				//await this.LoadTransactions();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	async addTransaction() {
		try {
			const result = await this.dialogService.open(TransactionAddComponent, { accountId: this.accountId, transactionId: 0 });
			if (result === true) {
				//await this.loadTransactions();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

}


