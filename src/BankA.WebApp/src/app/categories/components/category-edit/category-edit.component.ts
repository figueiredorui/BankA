import { Component, Inject, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { Category } from '../../models/category.model';
import { CategoryService } from '../../services/categories.service';


@Component({ standalone: false,
	selector: 'app-category-edit',
	templateUrl: './category-edit.component.html',
	styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent extends BaseComponent implements OnInit {
	categoryId: number = 0;
	categoryParentList: Category[] | undefined;
	form: UntypedFormGroup;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<CategoryEditComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private categoryService: CategoryService) {
		super(injector);

		this.categoryId = data.categoryId;

		this.form = new FormGroup({
			CategoryId: new FormControl(null),
			ParentId: new FormControl(0, [Validators.required]),
			CategoryName: new FormControl('', [Validators.required]),
			CategoryType: new FormControl({ value: '', disabled: true }, [Validators.required]),
		});
	}

	async ngOnInit() {
		await this.loadParentCategories();
		this.loadCategory();
	}

	cancel() {
		this.dialogRef.close();
	}

	async save() {
		try {
			this.form.markAllAsTouched();
			if (this.form.valid) {
				this.loading = true;

				const categoryReq = this.form.value;
				await this.categoryService.update(this.categoryId, categoryReq);
				this.dialogRef.close(true);
			}
		} catch (error: any) {
			this.errorMessage = error;
		} finally {
			this.loading = false;
		}
	}

	async delete() {
		try {
			const confirm = await this.dialogService.showConfirmation('Would you like to delete this record?');
			if (confirm) {
				await this.categoryService.delete(this.categoryId);
				this.dialogRef.close(true);
			}
		} catch (error: any) {
			this.errorMessage = error;
		}
	}


	private async loadCategory() {
		try {
			this.loading = true;
			const category = await this.categoryService.get(this.categoryId);

			this.form.get('CategoryId')?.setValue(category.Id);
			this.form.get('ParentId')?.setValue(category.ParentId);
			this.form.get('CategoryName')?.setValue(category.CategoryName);
			this.form.get('CategoryType')?.setValue(category.CategoryType);

		} catch (err: any) {
			this.errorMessage = err.Message;
		} finally {
			this.loading = false;
		}
	}

	private async loadParentCategories() {
		try {
			this.loading = true;
			const resp = await this.categoryService.getParentList();
			this.categoryParentList = resp.Data;
		} catch (err: any) {
			this.errorMessage = err.Message;
		} finally {
			this.loading = false;
		}
	}

	public parentChanged(event: any) {
		const ctrl = this.form.get('CategoryType');

		if (event.value == '0') {
			ctrl?.enable();
		} else {
			ctrl?.disable();
		}
	}


}
