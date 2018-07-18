import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FinanceRoutingModule } from './finance-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FinanceRoutingModule
  ],
  declarations: []
})
export class FinanceModule { }
