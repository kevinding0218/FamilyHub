
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FinanceSettingsRoutingModule } from './finance-settings-routing.module';
import { FinanceSettingsComponent } from './finance-settings.component';
import { SharedModule } from './../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FinanceSettingsRoutingModule,
    SharedModule
  ],
  declarations: [FinanceSettingsComponent]
})
export class FinanceSettingsModule { }
