import { SharedModule } from './../../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionDetailComponent } from './transaction-detail.component';
import { TransactionDetailRoutingModule } from './transaction-detail-routing.module';


@NgModule({
  imports: [
    CommonModule,
    TransactionDetailRoutingModule,
    SharedModule,
  ],
  declarations: [TransactionDetailComponent]
})
export class TransactionDetailModule { }