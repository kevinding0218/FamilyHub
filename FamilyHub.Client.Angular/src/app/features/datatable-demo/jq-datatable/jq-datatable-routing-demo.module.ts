import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JqDatatableDemoComponent } from './jq-datatable-demo.component';

const routes: Routes = [{
  path: '',
  component: JqDatatableDemoComponent,
  data: {
    title: 'Jq Datatable',
    icon: 'icon-settings',
    caption: 'Jq Datatable Demo',
    status: true
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class JqDatatableRoutingModule { }
