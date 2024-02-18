import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';

const routes: Routes = [

	{
		path: '',
		loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule),
	},
	{
		path: 'overview',
		loadChildren: () => import('./overview/overview.module').then(m => m.OverviewModule),
	},
	{
		path: 'transactions',
		loadChildren: () => import('./transactions/transactions.module').then(m => m.TransactionsModule),
	},
	{
		path: 'settings',
		redirectTo: 'settings/accounts', pathMatch: 'full'
	},
	{
		path: 'settings/accounts',
		loadChildren: () => import('./accounts/accounts.module').then(m => m.AccountsModule),
	},
	{
		path: 'settings/categories',
		loadChildren: () => import('./categories/categories.module').then(m => m.CategoriesModule),
	},
	{
		path: 'settings/merchants',
		loadChildren: () => import('./merchants/merchants.module').then(m => m.MerchantsModule),
	},
	{
		path: 'settings/rules',
		loadChildren: () => import('./rules/rules.module').then(m => m.RulesModule),
	},
	{ path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
