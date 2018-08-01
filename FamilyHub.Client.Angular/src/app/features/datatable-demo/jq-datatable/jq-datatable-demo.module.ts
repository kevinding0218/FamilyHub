import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JqDatatableRoutingModule } from './jq-datatable-routing-demo.module';
import { JqDatatableModule } from '../../../shared/datatable/jq-datatable/jq-datatable.module';
import { JqDatatableDemoComponent } from './jq-datatable-demo.component';
import {UiSwitchModule} from 'ng2-ui-switch';

@NgModule({
  imports: [
    CommonModule,
    JqDatatableRoutingModule,
    JqDatatableModule,
    UiSwitchModule,
  ],
  declarations: [JqDatatableDemoComponent]
})
export class JqDatatableDemoModule { }
