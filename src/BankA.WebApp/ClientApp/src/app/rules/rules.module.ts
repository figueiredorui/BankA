import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryService } from '../categories/services/categories.service';
import { MerchantService } from '../merchants/services/merchants.service';
import { SettingsContainerComponent } from '../shared/containers/settings-container/settings-container.component';
import { SharedModule } from '../shared/shared.module';
import { RuleAddComponent } from './components/rule-add/rule-add.component';
import { RuleEditComponent } from './components/rule-edit/rule-edit.component';
import { RuleListComponent } from './components/rule-list/rule-list.component';
import { RuleService } from './services/rules.service';

const routes: Routes = [
  {
    path: '',
    component: SettingsContainerComponent,
    children: [
      { path: '', component: RuleListComponent },
    ]
  },
];

@NgModule({
  declarations: [RuleListComponent, RuleAddComponent, RuleEditComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  providers: [RuleService, MerchantService, CategoryService],
})
export class RulesModule { }
