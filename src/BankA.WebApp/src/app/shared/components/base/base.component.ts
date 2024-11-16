import { Injector } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Identity } from '../../../core/models/identity.model';
import { DialogService } from '../../services/dialog.service';
import { ToastService } from '../../services/toast.service';

export abstract class BaseComponent {
	protected router: Router;
	protected activatedRoute: ActivatedRoute;
	protected toastService: ToastService;
	protected dialogService: DialogService;
	protected formBuilder: FormBuilder;

	errorMessage: string | undefined;
	loading: boolean = false;
	noRecords: boolean = false;
	identity: Identity | undefined;

	constructor(private inject: Injector) {
		this.router = inject.get(Router);
		this.activatedRoute = inject.get(ActivatedRoute);
		this.toastService = inject.get(ToastService);
		this.dialogService = inject.get(DialogService);
		this.formBuilder = inject.get(FormBuilder);

		console.log(this.constructor.name);
	}

	protected markAllAsTouched(group: FormGroup) {
		group.markAsTouched({ onlySelf: true });

		Object.keys(group.controls).map(field => {
			const control = group.get(field);
			if (control instanceof FormControl) {
				control.markAsTouched({ onlySelf: true });
			} else if (control instanceof FormGroup) {
				this.markAllAsTouched(control);
			}
		});
	}
}
