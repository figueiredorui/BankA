import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { EmptyStateComponent } from './components/empty-state/empty-state.component';
import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { InformationDialogComponent } from './components/information-dialog/information-dialog.component';
import { LoaderComponent } from './components/loader/loader.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ContainerComponent } from './containers/container/container.component';
import { SettingsContainerComponent } from './containers/settings-container/settings-container.component';
import { MatModule } from './mat.modules';
import { DialogService } from './services/dialog.service';
import { ToastService } from './services/toast.service';
import { AboutComponent } from './components/about/about.component';

@NgModule({
	imports: [CommonModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule,
		MatModule],
	declarations: [
		ErrorMessageComponent,
		LoaderComponent,
		InformationDialogComponent,
		ConfirmationDialogComponent,
		EmptyStateComponent,
		SettingsContainerComponent,
		ContainerComponent,
		NotFoundComponent,
		AboutComponent
	],
	exports: [
		FormsModule,
		RouterModule,
		ReactiveFormsModule,
		MatModule,
		ErrorMessageComponent,
		LoaderComponent,
		EmptyStateComponent,
		SettingsContainerComponent,
		ContainerComponent,
		NotFoundComponent
	],
	providers: [ToastService, DialogService]
})
export class SharedModule { }
