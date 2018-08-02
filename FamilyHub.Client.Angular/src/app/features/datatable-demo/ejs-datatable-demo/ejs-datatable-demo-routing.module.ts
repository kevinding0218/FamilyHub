import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EjsDatatableDemoComponent } from './ejs-datatable-demo.component';

const routes: Routes = [{
  path: '',
  component: EjsDatatableDemoComponent,
  data: {
    title: 'Ejs 2 Grid',
    icon: 'icon-settings',
    caption: 'Ejs 2 Grid Demo',
    status: true
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EjsDatatableDemoRoutingModule { }
