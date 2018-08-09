import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { TextMaskModule } from 'angular2-text-mask';
import { SelectModule } from 'ng-select';
import { StoreModule } from '@ngrx/store';

import { TableModule } from 'primeng/table';
import { memberReducer } from '../../../core/store/member/member.reducer';

import { MemberNgrxStoreRoutingModule } from './member-ngrx-store-routing.module';
import { MemberNgrxStoreComponent } from './member-ngrx-store.component';
import { MemberDetailReactivePopupComponent } from './member-detail-popup/member-detail-reactive-popup.component';

import { NgIOptionService } from '../../../shared/services/ng-option.service';

@NgModule({
  imports: [
    CommonModule,
    MemberNgrxStoreRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    TableModule,
    TextMaskModule,
    SelectModule,
    StoreModule.forFeature('members', memberReducer),
  ],
  declarations: [MemberNgrxStoreComponent, MemberDetailReactivePopupComponent],
  providers: [NgIOptionService]
})
export class MemberNgrxStoreModule { }
