import { Component, Inject, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CategoryService } from '../../../categories/services/categories.service';
import { MerchantService } from '../../../merchants/services/merchants.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { TransactionUpdateRequest } from '../../models/transaction-update.model';
import { TransactionsService } from '../../services/transactions.service';

@Component({ standalone: false,
	selector: 'app-transaction-edit',
	templateUrl: './transaction-edit.component.html',
	styleUrls: ['./transaction-edit.component.scss']
})
export class TransactionEditComponent extends BaseComponent implements OnInit {
	transactionId: number = 0;
	accountId: number = 0;
	categoryLookup: any[] = [];
	categoryLookupFiltered: Observable<any[]>;
	merchantLookup: any[] = [];
	merchantLookupFiltered: Observable<any[]>;
	form: UntypedFormGroup;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<TransactionEditComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private transactionsService: TransactionsService,
		private categoryService: CategoryService,
		private merchantService: MerchantService) {
		super(injector);

		this.transactionId = data.transactionId;
		this.accountId = data.accountId;

		this.form = new FormGroup({
			TransactionId: new FormControl(this.transactionId),
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
		this.loadTransaction(this.transactionId);
	}

	cancel() {
		this.dialogRef.close(false);
	}

	async delete() {
		const confirm = await this.dialogService.showConfirmation('Would you like to delete this record?');
		if (confirm) {
			await this.transactionsService.deleteTransaction(this.accountId, this.transactionId);
			this.dialogRef.close(true);
		}
	}

	async save() {
		try {

			if (this.form.valid) {
				this.loading = true;

				const transactionUpdate: TransactionUpdateRequest = {
					AccountId: this.form.controls['AccountId'].value,
					TransactionId: this.form.controls['TransactionId'].value,
					TransactionDate: this.form.controls['TransactionDate'].value,
					Description: this.form.controls['Description'].value,
					Balance: this.form.controls['Balance'].value,
					CategoryId: this.form.controls['Category'].value?.Id,
					MerchantId: this.form.controls['Merchant'].value?.Id,
					MerchantName: (typeof this.form.controls['Merchant'].value === 'string') ? this.form.controls['Merchant'].value : null,
				};

				await this.transactionsService.updateTransaction(transactionUpdate);

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

	private async loadTransaction(transactionId: number) {
		try {
			this.loading = true;
			const result = await this.transactionsService.getTransaction(this.accountId, transactionId);

			this.form.controls['TransactionId'].setValue(result.Id);
			this.form.controls['TransactionDate'].setValue(result.TransactionDate);
			this.form.controls['Description'].setValue(result.Description);
			this.form.controls['Balance'].setValue(result.Balance);
			this.form.controls['Category'].setValue({ Id: result.Category?.Id, CategoryFullName: result.Category?.CategoryFullName }); 
			this.form.controls['Merchant'].setValue({ Id: result.Merchant?.Id, MerchantName: result.Merchant?.MerchantName }); 

		} catch (err: any) {
			this.errorMessage = err.Message;
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
