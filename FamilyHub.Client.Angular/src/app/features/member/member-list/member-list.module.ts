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


import { MemberDetailPopupComponent } from './member-detail-popup/member-detail-popup.component';
import { MemberDetailPopup2Component } from './member-detail-popup2/member-detail-popup2.component';
import { I18nModule } from './../../../shared/i18n/i18n.module';
import { NgIOptionService } from './../../../shared/services/ng-option.service';

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
    SelectModule,
    I18nModule
  ],
  declarations: [MemberListComponent, MemberDetailPopupComponent, MemberDetailPopup2Component],
  providers: [NgIOptionService]
})
export class MemberListModule { }
