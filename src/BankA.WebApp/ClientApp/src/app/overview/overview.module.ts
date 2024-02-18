import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ContainerComponent } from '../shared/containers/container/container.component';
import { OverviewComponent } from './components/overview/overview.component';
import { AccountService } from '../accounts/services/accounts.service';
import { ReportsService } from './services/reports.service';
import { CashflowChartComponent } from './components/cashflow-chart/cashflow-chart.component';
import { ChartsModule } from '../shared/charts.modules';
import { CategorySummaryComponent } from './components/category-summary/category-summary.component';
import { OverviewTransactionsComponent } from './components/overview-transactions/overview-transactions.component';
import { TransactionListComponent } from '../transactions/components/transaction-list/transaction-list.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CategoryService } from '../categories/services/categories.service';
import { MerchantService } from '../merchants/services/merchants.service';
import { MerchantSummaryComponent } from './components/merchant-summary/merchant-summary.component';

const routes: Routes = [
  {
    path: '',
    component: ContainerComponent,
    children: [
      { path: ':id', component: OverviewComponent },
      { path: '', component: OverviewComponent },
    ]
  },
];

@NgModule({
  declarations: [
    OverviewComponent,
    CashflowChartComponent,
    CategorySummaryComponent,
    MerchantSummaryComponent,
    OverviewTransactionsComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    ChartsModule,
    TransactionListComponent,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule
  ],
  providers: [AccountService, ReportsService, CategoryService, MerchantService]
})
export class OverviewModule { }
