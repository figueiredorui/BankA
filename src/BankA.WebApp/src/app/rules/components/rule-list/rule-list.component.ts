import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { debounceTime, distinctUntilChanged, filter, fromEvent, tap } from 'rxjs';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { PagedListRequest } from '../../../shared/models/paged-list-request.model';
import { PagedList } from '../../../shared/models/paged-list.model';
import { Rule } from '../../models/rules.model';
import { RuleService } from '../../services/rules.service';
import { RuleAddComponent } from '../rule-add/rule-add.component';
import { RuleEditComponent } from '../rule-edit/rule-edit.component';

@Component({ standalone: false,
	selector: 'app-rule-list',
	templateUrl: './rule-list.component.html',
	styleUrls: ['./rule-list.component.scss']
})
export class RuleListComponent extends BaseComponent implements OnInit, AfterViewInit {
	@ViewChild('inputSearch') inputSearch: ElementRef | undefined;
	
	public rules: Rule[] = [];
	public pagedList = {} as PagedList<Rule>;
	public filter = new PagedListRequest();

	constructor(private injector: Injector, private ruleService: RuleService) {
		super(injector);
	}

	async ngOnInit() {
		await this.loadRules();
	}

	ngAfterViewInit() {
		fromEvent(this.inputSearch?.nativeElement, 'input')
			.pipe(
				filter(Boolean),
				debounceTime(250),
				distinctUntilChanged(),
				tap(async () => {
					const text = this.inputSearch?.nativeElement.value;
					if (text.length > 2) {
						this.filter.Search = text;
					}
					else {
						this.filter.Search = '';
					}
					await this.loadRules();
				})
			)
			.subscribe();
	}


	async addRule() {
		try {
			const result = await this.dialogService.open(RuleAddComponent, { });
			if (result === true) {
				this.toastService.success('Created successfully!', 'Rule');
				this.loadRules();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	async editRule(ruleId: number) {
		try {
			const result = await this.dialogService.open(RuleEditComponent, { ruleId: ruleId });
			if (result === true) {
				this.toastService.success('Saved successfully!', 'Rule');
				this.loadRules();
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}

	async handlePageEvent(e: PageEvent) {
		this.filter.PageIndex = e.pageIndex;
		this.filter.PageSize = e.pageSize;

		await this.loadRules();
	}

	
	private async loadRules() {
		try {
			this.loading = true;
			this.pagedList = await this.ruleService.GetList(this.filter);
			this.rules = this.pagedList.Data;

			if (this.rules.length === 0)
				this.noRecords = true;
		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}
}	
