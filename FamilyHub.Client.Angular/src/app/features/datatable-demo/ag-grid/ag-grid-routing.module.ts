import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AgGridComponent } from './ag-grid.component';

const routes: Routes = [{
  path: '',
  component: AgGridComponent,
  data: {
    title: 'Ag Grid',
    icon: 'icon-settings',
    caption: 'Ag Grid Demo',
    status: true
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AgGridRoutingModule { }
