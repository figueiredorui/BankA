import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { debounceTime, distinctUntilChanged, filter, fromEvent, tap } from 'rxjs';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { PagedListRequest } from '../../../shared/models/paged-list-request.model';
import { PagedList } from '../../../shared/models/paged-list.model';
import { Merchant } from '../../models/merchants.model';
import { MerchantService } from '../../services/merchants.service';
import { MerchantAddComponent } from '../merchant-add/merchant-add.component';
import { MerchantEditComponent } from '../merchant-edit/merchant-edit.component';

@Component({
	selector: 'app-merchant-list',
	templateUrl: './merchant-list.component.html',
	styleUrls: ['./merchant-list.component.scss']
})
export class MerchantListComponent extends BaseComponent implements OnInit, AfterViewInit {
	@ViewChild('inputSearch') inputSearch: ElementRef | undefined;
	public merchants: Merchant[] = [];
	public pagedList = {} as PagedList<Merchant>;
	public filter = new PagedListRequest();

	constructor(private injector: Injector,
		private merchantService: MerchantService) {
		super(injector);
	}

	async ngOnInit() {
		await this.loadMerchants();
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
					await this.loadMerchants();
				})
			)
			.subscribe();
	}

	async addMerchant() {
		try {
			const result = await this.dialogService.open(MerchantAddComponent, {});
			if (result === true) {
				this.toastService.success('Created successfully!', 'Merchant');
				this.loadMerchants();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	async editMerchant(merchantId: number) {
		try {
			const result = await this.dialogService.open(MerchantEditComponent, { merchantId: merchantId });
			if (result === true) {
				this.toastService.success('Saved successfully!', 'Merchant');
				this.loadMerchants();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	async handlePageEvent(e: PageEvent) {
		this.filter.PageIndex = e.pageIndex;
		this.filter.PageSize = e.pageSize;

		await this.loadMerchants();
	}

	private async loadMerchants() {
		try {
			this.loading = true;
			this.pagedList = await this.merchantService.getList(this.filter);
			this.merchants = this.pagedList.Data;
			
			if (this.merchants.length === 0)
				this.noRecords = true;
		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}
}
