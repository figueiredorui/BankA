import { Component, Inject, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CategoryService } from '../../../categories/services/categories.service';
import { MerchantService } from '../../../merchants/services/merchants.service';
import { RuleService } from '../../../rules/services/rules.service';
import { BaseComponent } from '../../../shared/components/base/base.component';

@Component({ standalone: false,
	selector: 'app-categorize-add-rule',
	templateUrl: './categorize-add-rule.component.html',
	styleUrls: ['./categorize-add-rule.component.scss']
})
export class CategorizeAddRuleComponent extends BaseComponent implements OnInit {
	categoryLookup: any[] = [];
	categoryLookupFiltered: Observable<any[]>;
	merchantLookup: string[] = [];
	merchantLookupFiltered: Observable<string[]>;
	form: UntypedFormGroup;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<CategorizeAddRuleComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private ruleService: RuleService,
		private merchantService: MerchantService,
		private categoryService: CategoryService) {
		super(injector);

		this.form = new FormGroup({
			Keywords: new FormControl(data.description, [Validators.required]),
			MerchantName: new FormControl('', [Validators.required]),
			CategoryId: new FormControl(null, [Validators.required]),
		});

		this.merchantLookupFiltered = this.form.controls['MerchantName'].valueChanges.pipe(
			startWith(''),
			map(value => {
				const name = typeof value === 'string' ? value : null;
				return name ? this.merchantLookupFilter(name || '') : this.merchantLookup.slice();
			}),
		);

		this.categoryLookupFiltered = this.form.controls['CategoryId'].valueChanges.pipe(
			startWith(''),
			map(value => {
				const name = typeof value === 'string' ? value : null;
				return name ? this.categoryLookupFilter(name || '') : this.categoryLookup.slice(); 
			}),
		);
	}

	private merchantLookupFilter(value: string): string[] {
		const filterValue = value.toLowerCase();
		return this.merchantLookup.filter(option => option.toLowerCase().includes(filterValue));
	}

	private categoryLookupFilter(value: string): any[] {
		const filterValue = value.toLowerCase();
		return this.categoryLookup.filter(option => option.CategoryName.toLowerCase().includes(filterValue));
	}

	displayFn(option: any): string {
		return option && option.CategoryName ? option.CategoryName : '';
	}

	trackByIdFn(option: any) {
		return option.Id;
	}

	async ngOnInit() {
		await this.loadCategories();
		await this.loadMerchants();
	}

	cancel() {
		this.dialogRef.close(false);
	}

	async save() {
		try {
			this.loading = true;
			const ruleReq = {
				Keywords: this.form.controls['Keywords'].value,
				CategoryId: this.form.controls['CategoryId'].value.Id,
				MerchantName: this.form.controls['MerchantName'].value
			};
			await this.ruleService.add(ruleReq);
			this.dialogRef.close(true);
		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	private async loadCategories() {
		try {
			this.loading = true;
			const result = await this.categoryService.getLookup();

			this.categoryLookup = result.map(c => {
				return {
					Id: c.Id,
					CategoryName: c.CategoryFullName,
					CategoryType: c.CategoryType
				};
			});

		} catch (err: any) {
			this.errorMessage = err.Message;
		} finally {
			this.loading = false;
		}
	}

	private async loadMerchants() {
		try {
			this.loading = true;
			const result = await this.merchantService.getLookup();

			this.merchantLookup = result.map(s => {
				return s.MerchantName;
			});

		} catch (err: any) {
			this.errorMessage = err.Message;
		} finally {
			this.loading = false;
		}
	}
}
