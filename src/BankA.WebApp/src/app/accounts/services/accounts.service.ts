import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { PagedListRequest } from '../../shared/models/paged-list-request.model';
import { PagedList } from '../../shared/models/paged-list.model';
import { Account } from '../models/accounts.model';
import { FileFormatConfiguration } from '../models/file-format-configuration';

@Injectable()
export class AccountService {
	constructor(private http: HttpClient) { }

	public async getFileFormatsList(): Promise<string[]> {
		const url = `/api/accounts/file-formats`;
		const resp = this.http.get<string[]>(url);
		return await lastValueFrom<any>(resp);
	}

	public async getAccounts(search: PagedListRequest): Promise<PagedList<Account>> {
		let url = `/api/accounts?pageIndex=${search.PageIndex}&search=${search.Search}`;
		if (search.PageSize)
			url += `&pageSize=${search.PageSize}`;

		const resp = this.http.get<PagedList<Account>>(url)
			.pipe(map(json =>
				new PagedList<Account>(json, Account.CreateList(json.Data))
			));
		return await lastValueFrom<any>(resp);
	}

	public async getAccount(id: number): Promise<Account> {
		const url = `/api/accounts/${id}`;
		const resp = this.http.get<Account>(url)
			.pipe(map(json =>
				new Account(json)
			));

		return await lastValueFrom<any>(resp);
	}

	public async addAccount(account: Account): Promise<Account> {
		const url = `/api/accounts`;
		const resp = this.http.post<Account>(url, account);

		return await lastValueFrom<any>(resp);
	}

	public async updateAccount(account: Account): Promise<Account> {
		const url = `/api/accounts/${account.Id}`;
		const resp = this.http.put<Account>(url, account);

		return await lastValueFrom<any>(resp);
	}

	public async deleteAccount(accountId: number): Promise<any> {
		const url = `/api/accounts/${accountId}`;
		const resp = this.http.delete<Account>(url);

		return await lastValueFrom<any>(resp);
	}

	public async updateFileFormatConfig(id: number, fileFormatConfig: FileFormatConfiguration): Promise<Account> {
		const url = `/api/accounts/${id}/file-format-config`;
		const request = {
			FileFormatConfiguration: JSON.stringify(fileFormatConfig)
		};
		const resp = this.http.put<Account>(url, request);

		return await lastValueFrom<any>(resp);
	}
}
