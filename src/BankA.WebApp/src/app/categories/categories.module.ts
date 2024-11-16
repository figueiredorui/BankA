import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { SettingsContainerComponent } from '../shared/containers/settings-container/settings-container.component';
import { CategoryService } from './services/categories.service';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { CategoryAddComponent } from './components/category-add/category-add.component';
import { CategoryEditComponent } from './components/category-edit/category-edit.component';

const routes: Routes = [
	{ path: '', redirectTo: 'expense', pathMatch: 'full' },
	{
		path: '',
		component: SettingsContainerComponent,
		children: [
			{ path: ':categoryType', component: CategoryListComponent },
		]
	},
];

@NgModule({
	declarations: [CategoryListComponent, CategoryAddComponent, CategoryEditComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(routes),
		SharedModule
	],
	providers: [CategoryService],
})
export class CategoriesModule { }
