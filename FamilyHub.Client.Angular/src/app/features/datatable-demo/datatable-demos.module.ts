import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DatatablesRoutingModule } from './datatable-demos-routing.module';
import { MemberService } from '../../core/services/member.service';
import { EjsDatatableDemoComponent } from './ejs-datatable-demo/ejs-datatable-demo.component';

@NgModule({
  imports: [
    CommonModule,
    DatatablesRoutingModule
  ],
  declarations: [EjsDatatableDemoComponent],
  providers: [MemberService]
})
export class DatatableDemosModule { }
