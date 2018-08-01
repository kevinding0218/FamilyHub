import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AgGridRoutingModule } from './ag-grid-routing.module';
import { AgGridComponent } from './ag-grid.component';
import { AgGridModule } from 'ag-grid-angular';

@NgModule({
  imports: [
    CommonModule,
    AgGridRoutingModule,
    AgGridModule.withComponents([]),
  ],
  declarations: [AgGridComponent]
})
export class AgGridDemoModule { }
