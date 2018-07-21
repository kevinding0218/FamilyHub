import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MemberListRoutingModule } from './member-list-routing.module';
import { MemberListComponent } from './member-list.component';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { DataTableModule } from 'angular2-datatable';
import { TextMaskModule } from 'angular2-text-mask';
import { SelectModule } from 'ng-select';

import { NgIOptionService } from './../../../core/services/ng-option.service';
import { MemberDetailPopupComponent } from './member-detail-popup/member-detail-popup.component';
import { MemberDetailPopup2Component } from './member-detail-popup2/member-detail-popup2.component';


@NgModule({
  imports: [
    CommonModule,
    MemberListRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    DataTableModule,
    TextMaskModule,
    SelectModule
  ],
  declarations: [MemberListComponent, MemberDetailPopupComponent, MemberDetailPopup2Component],
  providers: [NgIOptionService]
})
export class MemberListModule { }
