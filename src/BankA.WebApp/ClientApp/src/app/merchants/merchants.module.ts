import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SettingsContainerComponent } from '../shared/containers/settings-container/settings-container.component';
import { SharedModule } from '../shared/shared.module';
import { MerchantAddComponent } from './components/merchant-add/merchant-add.component';
import { MerchantEditComponent } from './components/merchant-edit/merchant-edit.component';
import { MerchantListComponent } from './components/merchant-list/merchant-list.component';
import { MerchantService } from './services/merchants.service';

const routes: Routes = [
	{ path: '', redirectTo: '', pathMatch: 'full' },
	{
		path: '',
		component: SettingsContainerComponent,
		children: [
			{ path: '', component: MerchantListComponent },
		]
	},
];

@NgModule({
	declarations: [MerchantListComponent, MerchantAddComponent, MerchantEditComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(routes),
		SharedModule
	],
	providers: [MerchantService],
})
export class MerchantsModule { }
