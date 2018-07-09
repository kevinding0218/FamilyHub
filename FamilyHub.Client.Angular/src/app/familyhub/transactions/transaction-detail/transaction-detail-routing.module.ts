import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TransactionDetailComponent } from './transaction-detail.component';

const routes: Routes = [{
  path: '',
  component: TransactionDetailComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransactionDetailRoutingModule { }
