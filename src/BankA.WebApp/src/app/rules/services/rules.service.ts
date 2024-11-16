import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { Rule } from '../models/rules.model';
import { PagedListRequest } from '../../shared/models/paged-list-request.model';
import { PagedList } from '../../shared/models/paged-list.model';

@Injectable()
export class RuleService {
	constructor(private http: HttpClient) { }

	public async GetList(search: PagedListRequest): Promise<PagedList<Rule>> {
		const url = `/api/rules?pageIndex=${search.PageIndex}&search=${search.Search}`;
		const resp = this.http.get<PagedList<Rule>>(url);

		return await lastValueFrom<PagedList<Rule>>(resp);
	}

	async get(ruleId: number): Promise<Rule> {
		const url = `/api/rules/${ruleId}`;
		const resp = this.http.get<Rule>(url);

		return await lastValueFrom<any>(resp);
	}

	async add(rule: any): Promise<number> {
		const url = `/api/rules`;
		const resp = this.http.post<number>(url, rule);

		return await lastValueFrom<any>(resp);
	}

	async update(ruleId: number, rule: any): Promise<any[]> {
		const url = `/api/rules/${ruleId}`;
		const resp = this.http.put<any>(url, rule);

		return await lastValueFrom<any>(resp);
	}

	async delete(ruleId: number): Promise<any[]> {
		const url = `/api/rules/${ruleId}`;
		const resp = this.http.delete<any>(url);

		return await lastValueFrom<any>(resp);
	}
}
