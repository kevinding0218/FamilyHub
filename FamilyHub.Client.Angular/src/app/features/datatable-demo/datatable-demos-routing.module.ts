import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [{
  path: '',
  data: {
    title: 'Datatable Demo',
    status: true
  },
  children: [
    {
      path: 'ag-grid',
      loadChildren: './ag-grid/ag-grid.module#AgGridDemoModule'
    },
    {
      path: 'ng2-smart-table',
      loadChildren: './ng2-smart/ng2-smart.module#Ng2SmartModule'
    },
    {
      path: 'jq-datatable',
      loadChildren: './jq-datatable/jq-datatable-demo.module#JqDatatableDemoModule'
    },
    {
      path: 'prime-ng-table',
      loadChildren: './prime-ng-table/prime-ng-table.module#PrimeNgTableModule'
    },
    {
      path: 'ejs-datatable',
      loadChildren: './ejs-datatable-demo/ejs-datatable-demo.module#EjsDatatableDemoModule'
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DatatablesRoutingModule { }
