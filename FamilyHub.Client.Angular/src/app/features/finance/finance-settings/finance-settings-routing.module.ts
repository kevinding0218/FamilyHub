import { FinanceSettingsComponent } from './finance-settings.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: FinanceSettingsComponent,
    data: {
      title: 'Finance Settings',
      icon: 'icon-settings',
      caption: 'Just simple settings',
      status: true
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinanceSettingsRoutingModule { }
