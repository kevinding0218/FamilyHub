import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MemberRoutingModule } from './member-routing.module';
import { MemberService } from '../../core/services/member.service';
import { SharedConfig } from './../../shared/utils/shared.config';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MemberRoutingModule
  ],
  declarations: [],
  providers: [MemberService, SharedConfig]
})
export class MemberModule { }
