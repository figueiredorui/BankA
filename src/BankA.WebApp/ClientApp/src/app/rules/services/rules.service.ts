import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { Rule } from '../models/rules.model';
import { PagedListRequest } from '../../shared/models/paged-list-request.model';
import { PagedList } from '../../shared/models/paged-list.model';
import { environment } from '../../../environments/environment';

@Injectable()
export class RuleService {
	private baseUrl = environment.apiUrl;
	constructor(private http: HttpClient) { }

	public async GetList(search: PagedListRequest): Promise<PagedList<Rule>> {
		const url = `${this.baseUrl}/rules?pageIndex=${search.PageIndex}&search=${search.Search}`;
		const resp = this.http.get<PagedList<Rule>>(url);

		return await lastValueFrom<PagedList<Rule>>(resp);
	}

	async get(ruleId: number): Promise<Rule> {
		const url = `${this.baseUrl}/rules/${ruleId}`;
		const resp = this.http.get<Rule>(url);

		return await lastValueFrom<any>(resp);
	}

	async add(rule: any): Promise<number> {
		const url = `${this.baseUrl}/rules`;
		const resp = this.http.post<number>(url, rule);

		return await lastValueFrom<any>(resp);
	}

	async update(ruleId: number, rule: any): Promise<any[]> {
		const url = `${this.baseUrl}/rules/${ruleId}`;
		const resp = this.http.put<any>(url, rule);

		return await lastValueFrom<any>(resp);
	}

	async delete(ruleId: number): Promise<any[]> {
		const url = `${this.baseUrl}/rules/${ruleId}`;
		const resp = this.http.delete<any>(url);

		return await lastValueFrom<any>(resp);
	}
}
