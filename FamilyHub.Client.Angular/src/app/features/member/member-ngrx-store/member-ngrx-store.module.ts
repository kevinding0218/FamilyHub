import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';

import { MemberNgrxStoreRoutingModule } from './member-ngrx-store-routing.module';
import { MemberNgrxStoreComponent } from './member-ngrx-store.component';
import { TableModule } from 'primeng/table';
import { memberReducer } from '../../../core/store/member/member.reducer';

@NgModule({
  imports: [
    CommonModule,
    MemberNgrxStoreRoutingModule,
    TableModule,
    StoreModule.forFeature('members', memberReducer),
  ],
  declarations: [MemberNgrxStoreComponent]
})
export class MemberNgrxStoreModule { }
