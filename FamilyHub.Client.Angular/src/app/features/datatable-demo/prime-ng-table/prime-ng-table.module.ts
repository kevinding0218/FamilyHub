import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PrimeNgTableRoutingModule } from './prime-ng-table-routing.module';
import { PrimeNgTableComponent } from './prime-ng-table.component';
import { TableModule } from 'primeng/table';

@NgModule({
  imports: [
    CommonModule,
    PrimeNgTableRoutingModule,
    TableModule
  ],
  declarations: [PrimeNgTableComponent]
})
export class PrimeNgTableModule { }
