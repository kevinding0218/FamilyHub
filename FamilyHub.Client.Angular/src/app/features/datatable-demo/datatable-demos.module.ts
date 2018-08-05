import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DatatablesRoutingModule } from './datatable-demos-routing.module';
import { MemberService } from '../../core/services/member.service';

@NgModule({
  imports: [
    CommonModule,
    DatatablesRoutingModule
  ],
  declarations: [],
  providers: [MemberService]
})
export class DatatableDemosModule { }
