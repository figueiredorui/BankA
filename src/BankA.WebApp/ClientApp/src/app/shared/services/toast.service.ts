import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ToastService {
	constructor(private toastr: MatSnackBar) { }

	success(message: string, title: string) {
		this.toastr.open(message, title, { duration: 2000 });
	}

	error(message: string, title: string) {
		this.toastr.open(message, title, { duration: 2000 });
	}
}
