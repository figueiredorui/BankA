import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { PagedList } from '../../shared/models/paged-list.model';
import { CategoryRequest } from '../models/category-request.model';
import { Category } from '../models/category.model';
import { CategoryPagedListRequest } from '../models/category-paged-list-request';

@Injectable()
export class CategoryService {
	constructor(private http: HttpClient) { }

	public async getList(search: CategoryPagedListRequest): Promise<PagedList<Category>> {
		let url = `/api/categories?pageIndex=${search.PageIndex}&search=${search.Search}`;
		if (search.PageSize)
			url += `&pageSize=${search.PageSize}`;
		if (search.CategoryType)
			url += `&categoryType=${search.CategoryType}`;
		const resp = this.http.get<any>(url);

		return await lastValueFrom<any>(resp);
	}


	public async getParentList(): Promise<PagedList<Category>> {
		const url = `/api/categories?pageIndex=0&&pageSize=100&isParent=true`;
		const resp = this.http.get<any>(url);

		return await lastValueFrom<any>(resp);
	}

	public async getLookup(): Promise<Category[]> {
		const search = new CategoryPagedListRequest();
		const result: Category[] = [];

		let pagedList = await this.getList(search);
		result.push(...pagedList.Data);

		while (pagedList.PageCount > pagedList.PageIndex) {
			search.PageIndex++;
			pagedList = await this.getList(search);
			result.push(...pagedList.Data);
		}

		return result;
	}

	async get(categoryId: number): Promise<Category> {
		const url = `/api/categories/${categoryId}`;
		const resp = this.http.get<Category>(url);

		return await lastValueFrom<any>(resp);
	}

	async add(category: CategoryRequest): Promise<number> {
		const url = `/api/categories`;
		const resp = this.http.post<number>(url, category);

		return await lastValueFrom<any>(resp);
	}

	async update(categoryId: number, category: CategoryRequest): Promise<any[]> {
		const url = `/api/categories/${categoryId}`;
		const resp = this.http.put<any>(url, category);

		return await lastValueFrom<any>(resp);
	}

	async delete(categoryId: number): Promise<any[]> {
		const url = `/api/categories/${categoryId}`;
		const resp = this.http.delete<any>(url);

		return await lastValueFrom<any>(resp);
	}
}
