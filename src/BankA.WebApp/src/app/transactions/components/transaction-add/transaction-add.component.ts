import { Component, Inject, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CategoryService } from '../../../categories/services/categories.service';
import { MerchantService } from '../../../merchants/services/merchants.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { TransactionAddRequest } from '../../models/transaction-add.model';
import { TransactionsService } from '../../services/transactions.service';

@Component({ standalone: false,
	selector: 'app-transaction-add',
	templateUrl: './transaction-add.component.html',
	styleUrls: ['./transaction-add.component.scss']
})
export class TransactionAddComponent extends BaseComponent implements OnInit {
	accountId: number = 0;
	categoryLookup: any[] = [];
	categoryLookupFiltered: Observable<any[]>;
	merchantLookup: any[] = [];
	merchantLookupFiltered: Observable<any[]>;
	form: UntypedFormGroup;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<TransactionAddComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private transactionsService: TransactionsService,
		private categoryService: CategoryService,
		private merchantService: MerchantService) {
		super(injector);

		this.accountId = data.accountId;

		this.form = new FormGroup({
			AccountId: new FormControl(this.accountId, [Validators.required]),
			TransactionDate: new FormControl('', [Validators.required]),
			Description: new FormControl('', [Validators.required]),
			Balance: new FormControl('', [Validators.required]),
			Category: new FormControl(null),
			Merchant: new FormControl(null),
		});
		
		this.merchantLookupFiltered = this.form.controls['Merchant'].valueChanges.pipe(
			startWith(''),
			map(value => {
				const name = typeof value === 'string' ? value : null;
				return name ? this.merchantLookupFilter(name || '') : this.merchantLookup.slice();
			}),
		);

		this.categoryLookupFiltered = this.form.controls['Category'].valueChanges.pipe(
			startWith(''),
			map(value => {
				const balance = this.form.controls['Balance'].value;
				let categoryType = 'Expense';
				if (balance > 0)
					categoryType = 'Income';

				const name = typeof value === 'string' ? value : null;
				const categoryTypes = ['Transfer', categoryType];
				if (name)
					return this.categoryLookupFilter(name || '', categoryType);
				else
					return this.categoryLookup.filter(option => categoryTypes.includes(option.CategoryType));
			}),
		);
	}

	async ngOnInit() {
		await this.loadCategories();
		await this.loadMerchants();
	}

	async cancel() {
		this.dialogRef.close(false);
	}

	async save() {
		try {

			if (this.form.valid) {
				this.loading = true;

				const transactionAdd: TransactionAddRequest = {
					AccountId: this.form.controls['AccountId'].value,
					TransactionDate: this.form.controls['TransactionDate'].value,
					Description: this.form.controls['Description'].value,
					Balance: this.form.controls['Balance'].value,
					CategoryId: this.form.controls['Category'].value?.Id,
					MerchantId: this.form.controls['Merchant'].value?.Id,
					MerchantName: (typeof this.form.controls['Merchant'].value === 'string') ? this.form.controls['Merchant'].value : null,
				};
				await this.transactionsService.addTransaction(transactionAdd);

				this.toastService.success('Saved successfully!', 'Transactions');
				this.dialogRef.close(true);
			}
			else {
				this.form.markAllAsTouched();
				this.errorMessage = 'Populate required fields';
			}


		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	displayCategoryFn(option: any): string {
		return option && option.CategoryFullName ? option.CategoryFullName : '';
	}

	displayMerchantFn(option: any): string {
		return option && option.MerchantName ? option.MerchantName : '';
	}

	private async loadCategories() {
		try {
			this.loading = true;
			const result = await this.categoryService.getLookup();

			this.categoryLookup = result.map(c => {
				return {
					Id: c.Id,
					CategoryFullName: c.CategoryFullName,
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

			this.merchantLookup = result.map(c => {
				return {
					Id: c.MerchantId,
					MerchantName: c.MerchantName,
				};
			});

		} catch (err: any) {
			this.errorMessage = err.Message;
		} finally {
			this.loading = false;
		}
	}

	private merchantLookupFilter(value: string): string[] {
		const filterValue = value.toLowerCase();
		return this.merchantLookup.filter(option => option.MerchantName.toLowerCase().includes(filterValue));
	}

	private categoryLookupFilter(value: string, categoryType: string): any[] {
		const filterValue = value.toLowerCase();
		const categoryTypes = ['Transfer', categoryType];
		return this.categoryLookup.filter(option => categoryTypes.includes(option.CategoryType) && option.CategoryFullName.toLowerCase().includes(filterValue));
	}

	
}
