import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { debounceTime, distinctUntilChanged, filter, fromEvent, tap } from 'rxjs';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { PagedListRequest } from '../../../shared/models/paged-list-request.model';
import { PagedList } from '../../../shared/models/paged-list.model';
import { Account } from '../../models/accounts.model';
import { AccountService } from '../../services/accounts.service';
import { AccountAddComponent } from '../account-add/account-add.component';
import { AccountEditComponent } from '../account-edit/account-edit.component';



@Component({ standalone: false,
	selector: 'app-account-list',
	templateUrl: './account-list.component.html',
	styleUrls: ['./account-list.component.scss']
})
export class AccountListComponent extends BaseComponent implements OnInit, AfterViewInit {
	@ViewChild('inputSearch') inputSearch: ElementRef | undefined;
	public accounts: Account[] = [];
	public accountStatus: string = '';
	public pagedList = {} as PagedList<Account>;
	public filter = new PagedListRequest();


	constructor(private injector: Injector, private accountService: AccountService) {
		super(injector);
	}

	ngOnInit() {
		this.activatedRoute.params.subscribe((parameter: any) => {
			this.accountStatus = parameter.status;
			if (this.accountStatus == null)
				this.accountStatus = 'open';
			this.loadAccounts();
		});
	}

	ngAfterViewInit() {
		fromEvent(this.inputSearch?.nativeElement, 'input')
			.pipe(
				filter(Boolean),
				debounceTime(250),
				distinctUntilChanged(),
				tap(async () => {
					const text = this.inputSearch?.nativeElement.value;
					if (text.length >= 2) {
						this.filter.Search = text;
					}
					else {
						this.filter.Search = '';
					}
					await this.loadAccounts();
				})
			)
			.subscribe();
	}

	async addAccount() {
		try {
			const result = await this.dialogService.open(AccountAddComponent, {});
			if (result === true) {
				this.toastService.success('Created successfully!', 'Accounts');
				this.loadAccounts();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	async editAccount(accountId: any) {
		try {
			const result = await this.dialogService.open(AccountEditComponent, { accountId: accountId });
			if (result === true) {
				this.toastService.success('Saved successfully!', 'Accounts');
				this.loadAccounts();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	private async loadAccounts() {
		try {
			this.loading = true;
			this.pagedList = await this.accountService.getAccounts(this.filter);

			if (this.accountStatus) {
				this.accounts = this.pagedList.Data.filter((item: Account) => item.Closed === (this.accountStatus.toLowerCase() == 'closed' ? true : false));
			} else {
				this.accounts = this.pagedList.Data.filter((item: Account) => item.Closed === false);
			}

			if (this.accounts.length === 0)
				this.noRecords = true;

		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	async handlePageEvent(e: PageEvent) {
		this.filter.PageIndex = e.pageIndex;
		this.filter.PageSize = e.pageSize;

		await this.loadAccounts();
	}
}
