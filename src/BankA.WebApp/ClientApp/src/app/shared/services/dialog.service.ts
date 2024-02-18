import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { lastValueFrom } from 'rxjs';
import { ConfirmationDialogComponent } from '../components/confirmation-dialog/confirmation-dialog.component';
import { InformationDialogComponent } from '../components/information-dialog/information-dialog.component';

@Injectable()
export class DialogService {
	constructor(private modalService: MatDialog) { }

	async showConfirmation(message: string) {
		const modalRef = this.modalService.open(ConfirmationDialogComponent, { data: { message: message } });
		const result = await lastValueFrom(modalRef.afterClosed());
		return result;
	}

	async showInformation(message: string) {
		const modalRef = this.modalService.open(InformationDialogComponent, { data: { message: message } });
		const result = await lastValueFrom(modalRef.afterClosed());
		return result;
	}

	async open<T>(component: ComponentType<T>, data: any) {
		const modalRef = this.modalService.open(component,
			{
				data: data,
				width: '500px',
			});
		const result = await lastValueFrom(modalRef.afterClosed());
		return result;
	}
	
	async openFullScreen<T>(component: ComponentType<T>, data: any) {
		const modalRef = this.modalService.open(component,
			{
				data: data,
				height: 'calc(100% - 30px)',
				width: 'calc(100% - 30px)',
				maxWidth: '100%',
				maxHeight: '100%',
				panelClass: 'fullscreen-dialog'
			});
		const result = await lastValueFrom(modalRef.afterClosed());
		return result;
	}
}
