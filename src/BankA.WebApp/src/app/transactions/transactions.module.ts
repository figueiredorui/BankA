import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

import { TransactionsComponent } from './components/transactions/transactions.component';
import { TransactionListComponent } from './components/transaction-list/transaction-list.component';
import { TransactionEditComponent } from './components/transaction-edit/transaction-edit.component';
import { MerchantService } from '../merchants/services/merchants.service';
import { CategorizeComponent } from './components/categorize/categorize.component';
import { CategorizeAddRuleComponent } from './components/categorize-add-rule/categorize-add-rule.component';
import { RuleService } from '../rules/services/rules.service';
import { CategoryService } from '../categories/services/categories.service';
import { ImportFileComponent } from './components/import-file/import-file.component';
import { ImportFileCsvComponent } from './components/import-file-csv/import-file-csv.component';
import { ContainerComponent } from '../shared/containers/container/container.component';
import { AccountService } from '../accounts/services/accounts.service';
import { TransactionAddComponent } from './components/transaction-add/transaction-add.component';

const routes: Routes = [
  {
    path: '',
    component: ContainerComponent,
    children: [
      { path: ':id', component: TransactionsComponent },
      { path: '', component: TransactionsComponent },
    ]
  },
];

@NgModule({
  declarations: [
    TransactionsComponent,
    TransactionAddComponent,
    TransactionEditComponent,
    CategorizeComponent,
    CategorizeAddRuleComponent,
    ImportFileComponent,
    ImportFileCsvComponent
  ],
  exports: [

  ],
  imports: [
    TransactionListComponent,
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  providers: [AccountService, CategoryService, MerchantService, RuleService],
})
export class TransactionsModule { }
