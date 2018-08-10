import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { SelectModule } from 'ng-select';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { TableModule } from 'primeng/table';
import { memberReducers } from '../../../core/store/member/member.reducers';
import { MemberEffects } from './../../../core/store/member/member.effects';

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
    StoreModule.forFeature('members', memberReducers),
    EffectsModule.forFeature([MemberEffects])
  ],
  declarations: [MemberNgrxStoreComponent, MemberDetailReactivePopupComponent],
  providers: [NgIOptionService]
})
export class MemberNgrxStoreModule { }
