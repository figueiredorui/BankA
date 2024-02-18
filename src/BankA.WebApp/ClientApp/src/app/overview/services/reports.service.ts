import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { MerchantSummary } from '../models/merchant-summary.model';
import { CashFlow } from '../models/cashflow.model';
import { CategorySummary } from '../models/category-summary.model';

@Injectable()
export class ReportsService {
	private baseUrl = environment.apiUrl;
	constructor(private http: HttpClient) { }

	public getCashflow(accountId: number, period: number): Promise<CashFlow[]> {
		const url = `${this.baseUrl}/reports/${accountId}/cashflow?period=${period}`;
		return this.http.get<any>(url)
			.pipe(map(d => d.Data)).toPromise();
	}

	public getCategorySummary(accountId: number, period: number, categoryId?: number): Promise<CategorySummary[]> {
		let url = `${this.baseUrl}/reports/${accountId}/category?period=${period}`;
		if (categoryId) {
			url += `/${categoryId}`;
		}
		const resp = this.http.get<any>(url)
			.pipe(map(d => d.Data));

		return lastValueFrom<any>(resp);
	}

	public getMerchantSummary(accountId: number, period: number): Promise<MerchantSummary[]> {
		const url = `${this.baseUrl}/reports/${accountId}/merchant?period=${period}`;

		const resp = this.http.get<any>(url)
			.pipe(map(d => d.Data));

		return lastValueFrom<any>(resp);
	}
}
