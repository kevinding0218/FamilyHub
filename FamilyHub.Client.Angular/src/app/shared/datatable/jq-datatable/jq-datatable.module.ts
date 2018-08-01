import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JqDatatableComponent } from './jq-datatable.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [JqDatatableComponent],
  exports: [JqDatatableComponent],
})
export class JqDatatableModule { }
