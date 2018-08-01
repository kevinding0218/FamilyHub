import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PrimeNgTableComponent } from './prime-ng-table.component';

const routes: Routes = [{
  path: '',
  component: PrimeNgTableComponent,
  data: {
    title: 'Prime Ng Grid',
    icon: 'icon-settings',
    caption: 'Prime Ng Grid Demo',
    status: true
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrimeNgTableRoutingModule { }
