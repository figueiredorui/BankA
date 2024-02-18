import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Injector, Input, OnChanges, OnInit, Output, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { fromEvent, } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, tap } from 'rxjs/operators';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { PagedList } from '../../../shared/models/paged-list.model';
import { SharedModule } from '../../../shared/shared.module';
import { TransactionsService } from '../../services/transactions.service';
import { CategorizeAddRuleComponent } from '../categorize-add-rule/categorize-add-rule.component';
import { TransactionEditComponent } from '../transaction-edit/transaction-edit.component';
import { TransactionPagedListRequest } from '../../models/transaction-paged-list-request.model';
import { Transaction } from '../../models/transaction.model';
import { TransactionList } from '../../models/transaction-list';

@Component({
	standalone: true,
	imports: [CommonModule, SharedModule],
	selector: 'app-transaction-list',
	templateUrl: './transaction-list.component.html',
	styleUrls: ['./transaction-list.component.scss'],
})
export class TransactionListComponent extends BaseComponent implements OnInit, OnChanges, AfterViewInit {

	@ViewChild('inputSearch') inputSearch: ElementRef | undefined;

	@Input()
	public accountId: number = 0;
	@Input()
	public showEdit: boolean = true;
	@Input()
	public showRule: boolean = false;
	@Input()
	public showRunningBalance: boolean = false;
	@Input()
	public filter = new TransactionPagedListRequest();

	@Output()
	public transactionUpdated = new EventEmitter<Transaction>();

	@Output()
	public ruleAdded = new EventEmitter();

	public transactionResult = {} as PagedList<TransactionList>;

	constructor(private injector: Injector,
		private transactionService: TransactionsService) {
		super(injector);
	}

	async ngOnInit() {
		console.log('TransactionListComponent');
		await this.loadTransactions();
		this.transactionService.onBalanceUpdated().subscribe(async () => {
			await this.loadTransactions();
		});
		this.transactionService.onCategorized().subscribe(async () => {
			await this.loadTransactions();
		});
	}

	ngOnChanges() {
		if (this.accountId > 0) {
			this.loadTransactions();
		}
	}

	ngAfterViewInit() {
		fromEvent(this.inputSearch?.nativeElement, 'input')
			.pipe(
				filter(Boolean),
				debounceTime(250),
				distinctUntilChanged(),
				tap(async () => {
					const text = this.inputSearch?.nativeElement.value;
					if (text.length > 2) {
						this.filter.Search = text;
					}
					else {
						this.filter.Search = '';
					}
					await this.loadTransactions();
				})
			)
			.subscribe();
	}

	clearFilter() {
		this.filter = new TransactionPagedListRequest();
	}

	async editTransaction(transaction: TransactionList) {
		try {
			const result = await this.dialogService.open(TransactionEditComponent, { accountId: transaction.AccountId, transactionId: transaction.Id });
			if (result === true) {
				await this.loadTransactions();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	async addCategoryRule(transaction: TransactionList) {
		try {
			const result = await this.dialogService.open(CategorizeAddRuleComponent, { description: transaction.Description });
			if (result == true)
				this.ruleAdded.next(null);
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	private async loadTransactions() {
		try {
			this.noRecords = false;
			this.loading = true;
			const result = await this.transactionService.getTransactions(this.accountId, this.filter);
			this.transactionResult = result;

			if (this.transactionResult.Data.length === 0)
				this.noRecords = true;
		} catch (e: any) {
			this.errorMessage = e;
		} finally {
			this.loading = false;
		}
	}

	async handlePageEvent(e: PageEvent) {
		this.filter.PageIndex = e.pageIndex;
		this.filter.PageSize = e.pageSize;
		await this.loadTransactions();
	}
}
