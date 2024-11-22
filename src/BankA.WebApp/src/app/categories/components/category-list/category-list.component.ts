import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { debounceTime, distinctUntilChanged, filter, fromEvent, tap } from 'rxjs';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { PagedList } from '../../../shared/models/paged-list.model';
import { Category } from '../../models/category.model';
import { CategoryService } from '../../services/categories.service';
import { CategoryAddComponent } from '../category-add/category-add.component';
import { CategoryEditComponent } from '../category-edit/category-edit.component';
import { CategoryPagedListRequest } from '../../models/category-paged-list-request';

@Component({ standalone: false,
	selector: 'app-category-list',
	templateUrl: './category-list.component.html',
	styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent extends BaseComponent implements OnInit, AfterViewInit {
	@ViewChild('inputSearch') inputSearch: ElementRef | undefined;
	public categories: Category[] = [];
	public pagedList = {} as PagedList<Category>;
	public filter = new CategoryPagedListRequest();

	constructor(private injector: Injector, private categoryService: CategoryService) {
		super(injector);
	}

	ngOnInit() {
		this.activatedRoute.params.subscribe((parameter: any) => {
			this.filter.CategoryType = parameter.categoryType;
			this.loadCategories();
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
					await this.loadCategories();
				})
			)
			.subscribe();
	}

	async addCategory() {
		try {
			const result = await this.dialogService.open(CategoryAddComponent, {});
			if (result === true) {
				this.toastService.success('Created successfully!', 'Category');
				this.loadCategories();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	async editCategory(categoryId: number) {
		try {
			const result = await this.dialogService.open(CategoryEditComponent, { categoryId: categoryId });
			if (result === true) {
				this.toastService.success('Saved successfully!', 'Category');
				this.loadCategories();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	private async loadCategories() {
		try {
			this.loading = true;
			this.pagedList = await this.categoryService.getList(this.filter);
			this.categories = this.pagedList.Data;

			if (this.categories.length === 0)
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
		await this.loadCategories();
	}

}
