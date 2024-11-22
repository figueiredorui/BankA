import { Component, Inject, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { Category } from '../../models/category.model';
import { CategoryService } from '../../services/categories.service';

@Component({ standalone: false,
	selector: 'app-category-add',
	templateUrl: './category-add.component.html',
	styleUrls: ['./category-add.component.scss']
})
export class CategoryAddComponent extends BaseComponent implements OnInit {
	categoryParentList: Category[] | undefined;
	form: UntypedFormGroup;

	constructor(private injector: Injector,
		private dialogRef: MatDialogRef<CategoryAddComponent>,
		@Inject(MAT_DIALOG_DATA) private data: any,
		private categoryService: CategoryService) {
		super(injector);

		this.form = new FormGroup({
			CategoryId: new FormControl(null),
			ParentId: new FormControl(0, [Validators.required]),
			CategoryName: new FormControl('', [Validators.required]),
			CategoryType: new FormControl('', [Validators.required]),
		});
	}

	async ngOnInit() {
		await this.loadParentCategories();
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
				await this.categoryService.add(categoryReq);
				this.dialogRef.close(true);
			}
		} catch (error: any) {
			this.errorMessage = error;
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
			ctrl?.value('');
		} else {
			ctrl?.disable();
		}
	}
}
