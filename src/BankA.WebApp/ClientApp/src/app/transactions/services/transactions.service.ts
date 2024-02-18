import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom, map, Observable, Subject, tap } from 'rxjs';
import { TransactionAddRequest } from '../models/transaction-add.model';
import { Transaction } from '../models/transaction.model';
import { TransactionList } from '../models/transaction-list';
import { environment } from '../../../environments/environment';
import { PagedList } from '../../shared/models/paged-list.model';
import { TransactionPagedListRequest } from '../models/transaction-paged-list-request.model';
import { CategoriseReport } from '../models/categorise-report.model';
import { AccountBalance } from '../models/accounts.model';
import { TransactionUpdateRequest } from '../models/transaction-update.model';

@Injectable({
	providedIn: 'root',
})
export class TransactionsService {
	private accountBalanceCache: AccountBalance[] = [];
	private balanceUpdated = new Subject();
	private transactionsCategorized = new Subject();
	private baseUrl = environment.apiUrl;

	constructor(private http: HttpClient) { }

	onBalanceUpdated(): Observable<any> {
		return this.balanceUpdated.asObservable();
	}

	onCategorized(): Observable<any> {
		return this.transactionsCategorized.asObservable();
	}

	public async getTransactions(accountId: number, search: TransactionPagedListRequest): Promise<PagedList<TransactionList>> {
		let url = `${this.baseUrl}/accounts/${accountId}/transactions?pageIndex=${search.PageIndex}`;

		if (search.Search != null) {
			url += `&Search=${search.Search}`;
		}
		if (search.CategoryId != null) {
			url += `&CategoryId=${search.CategoryId}`;
		}
		if (search.Category != null) {
			url += `&Category=${search.Category}`;
		}
		if (search.CategoryType != null) {
			url += `&CategoryType=${search.CategoryType}`;
		}
		if (search.MerchantId != null) {
			url += `&MerchantId=${search.MerchantId}`;
		}
		if (search.Uncategorized != null) {
			url += `&Uncategorized=${search.Uncategorized}`;
		}
		if (search.Period != null) {
			url += `&Period=${search.Period}`;
		}

		const resp = this.http.get<PagedList<Transaction>>(url);

		return await lastValueFrom<any>(resp);
	}


	public async getTransaction(accountId: number, transactionId: number): Promise<Transaction> {
		const url = `${this.baseUrl}/accounts/${accountId}/transactions/${transactionId}`;
		const resp = this.http.get<Transaction>(url);

		return await lastValueFrom<any>(resp);
	}

	public async addTransaction(transaction: TransactionAddRequest): Promise<any> {
		const url = `${this.baseUrl}/accounts/${transaction.AccountId}/transactions`;
		const resp = this.http.post<any>(url, transaction);

		await lastValueFrom<any>(resp);

		this.accountBalanceCache = [];
		this.balanceUpdated.next(true);
	}

	public async updateTransaction(transaction: TransactionUpdateRequest): Promise<any> {
		const url = `${this.baseUrl}/accounts/${transaction.AccountId}/transactions/${transaction.TransactionId}`;
		const resp = this.http.put<any>(url, transaction);

		await lastValueFrom<any>(resp);

		this.accountBalanceCache = [];
		this.balanceUpdated.next(true);
	}

	public async deleteTransaction(accountId: number, transactionId: number): Promise<any> {
		const url = `${this.baseUrl}/accounts/${accountId}/transactions/${transactionId}`;
		const resp = this.http.delete<any>(url);

		await lastValueFrom<any>(resp);

		this.accountBalanceCache = [];
		this.balanceUpdated.next(true);
	}

	public async categorizeTransaction(accountId: number): Promise<CategoriseReport> {
		const url = `${this.baseUrl}/accounts/${accountId}/categorize`;
		const resp = this.http.post<any>(url, null);

		const result = await lastValueFrom<any>(resp);

		this.transactionsCategorized.next(true);

		return result;
	}

	public async importFile(id: number, file: File): Promise<boolean> {
		const httpOptions = {
			headers: new HttpHeaders({})
		};

		const formData: FormData = new FormData();
		formData.append('formFile', file, file.name);

		const url = `${this.baseUrl}/accounts/${id}/import-file`;
		const resp = this.http.post<boolean>(url, formData, httpOptions);
		await lastValueFrom<any>(resp);

		this.accountBalanceCache = [];
		this.balanceUpdated.next(true);

		return true;
	}

	public async getAccountBalanceFromCacheOrApi(): Promise<AccountBalance[]> {
		if (this.accountBalanceCache.length > 0) {
			return Promise.resolve(this.accountBalanceCache);
		}
		else {
			const url = `${this.baseUrl}/accounts/balance`;
			const resp = this.http
				.get<any>(url)
				.pipe(tap(d => {
					this.accountBalanceCache = d.Data;
				}), map(d => d.Data));

			return await lastValueFrom<any>(resp);
		}
	}
}
