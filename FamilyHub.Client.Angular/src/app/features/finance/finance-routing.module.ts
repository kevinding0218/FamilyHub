import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [{
  path: '',
  data: {
    title: 'Finance Components',
    status: true
  },
  children: [
    {
      path: 'settings',
      loadChildren: './finance-settings/finance-settings.module#FinanceSettingsModule'
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinanceRoutingModule { }
