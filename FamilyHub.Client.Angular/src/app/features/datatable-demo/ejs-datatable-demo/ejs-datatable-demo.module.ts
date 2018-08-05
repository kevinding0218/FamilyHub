import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EjsDatatableDemoRoutingModule } from './ejs-datatable-demo-routing.module';
import { EjsDatatableDemoComponent } from './ejs-datatable-demo.component';


import {
  GridModule,
  SortService,
  ResizeService,
  PageService,
  FilterService,
  GroupService,
  EditService,
  ExcelExportService,
  PdfExportService,
  ContextMenuService
} from '@syncfusion/ej2-ng-grids';

@NgModule({
  imports: [
    CommonModule,
    EjsDatatableDemoRoutingModule,
    GridModule
  ],
  declarations: [EjsDatatableDemoComponent],
  providers: [
    SortService, ResizeService, PageService,
    FilterService, GroupService, EditService,
    ExcelExportService, PdfExportService, ContextMenuService
  ],
})
export class EjsDatatableDemoModule { }
