import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { SettingsContainerComponent } from '../shared/containers/settings-container/settings-container.component';
import { AccountListComponent } from './components/account-list/account-list.component';
import { AccountAddComponent } from './components/account-add/account-add.component';
import { AccountEditComponent } from './components/account-edit/account-edit.component';
import { AccountService } from './services/accounts.service';

const routes: Routes = [
	{ path: '', redirectTo: 'open', pathMatch: 'full' },
	{
		path: '',
		component: SettingsContainerComponent,
		children: [
			{ path: ':status', component: AccountListComponent },
		]
	},
];

@NgModule({
	declarations: [AccountListComponent, AccountAddComponent, AccountEditComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(routes),
		SharedModule
	],
	providers: [AccountService],
})
export class AccountsModule { }
