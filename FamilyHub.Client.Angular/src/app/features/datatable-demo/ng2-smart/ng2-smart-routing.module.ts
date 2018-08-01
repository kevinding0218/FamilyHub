import { Ng2SmartComponent } from './ng2-smart.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [{
  path: '',
  component: Ng2SmartComponent,
  data: {
    title: 'Ng2 Smart Table',
    icon: 'icon-settings',
    caption: 'Ng2 Smart Table Demo',
    status: true
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class Ng2SmartRoutingModule { }
