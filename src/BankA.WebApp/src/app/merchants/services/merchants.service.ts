import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { Merchant } from '../models/merchants.model';
import { PagedListRequest } from '../../shared/models/paged-list-request.model';
import { PagedList } from '../../shared/models/paged-list.model';

@Injectable()
export class MerchantService {
	constructor(private http: HttpClient) { }

	public async getList(search: PagedListRequest): Promise<PagedList<Merchant>> {
		let url = `/api/merchants?pageIndex=${search.PageIndex}&search=${search.Search}`;
		if (search.PageSize)
			url += `&pageSize=${search.PageSize}`;
		const resp = this.http.get<any>(url);

		return await lastValueFrom<any>(resp);
	}

	public async getLookup(): Promise<Merchant[]> {

		const search = new PagedListRequest();
		const result: Merchant[] = [];

		let pagedList = await this.getList(search);
		result.push(...pagedList.Data);

		while (pagedList.PageCount > pagedList.PageIndex) {
			search.PageIndex++;
			pagedList = await this.getList(search);
			result.push(...pagedList.Data);
		}

		return result;
	}

	async get(merchantId: number): Promise<Merchant> {
		const url = `/api/merchants/${merchantId}`;
		const resp = this.http.get<Merchant>(url);

		return await lastValueFrom<any>(resp);
	}

	async add(merchant: Merchant): Promise<number> {
		const url = `/api/merchants`;
		const resp = this.http.post<number>(url, merchant);

		return await lastValueFrom<any>(resp);
	}

	async update(merchantId: number, merchant: Merchant): Promise<any[]> {
		const url = `/api/merchants/${merchantId}`;
		const resp = this.http.put<any>(url, merchant);

		return await lastValueFrom<any>(resp);
	}

	async delete(merchantId: number): Promise<any[]> {
		const url = `/api/merchants/${merchantId}`;
		const resp = this.http.delete<any>(url);

		return await lastValueFrom<any>(resp);
	}
}
