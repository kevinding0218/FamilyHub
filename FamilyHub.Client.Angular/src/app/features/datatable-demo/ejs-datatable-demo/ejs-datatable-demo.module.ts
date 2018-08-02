import { GridAllModule } from '@syncfusion/ej2-ng-grids';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EjsDatatableDemoRoutingModule } from './ejs-datatable-demo-routing.module';
import { EjsDatatableDemoComponent } from './ejs-datatable-demo.component';

// import { GridAllModule } from './../../../shared/datatable/ej2-ng-grid/src';


// import {
//   GridModule,
//   SortService,
//   ResizeService,
//   PageService,
//   FilterService,
//   GroupService,
//   EditService,
//   ExcelExportService,
//   PdfExportService,
//   ContextMenuService
// } from '@syncfusion/ej2-ng-grids';

@NgModule({
  imports: [
    CommonModule,
    EjsDatatableDemoRoutingModule,
    GridAllModule
  ],
  declarations: [EjsDatatableDemoComponent]
})
export class EjsDatatableDemoModule { }
