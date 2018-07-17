

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AccordionDirectiveModule } from '../accordion/accordion.module';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { SpinnerModule } from '../spinner/spinner.module';
import { ClickOutsideModule } from 'ng-click-outside';
import { BreadcrumbsModule } from './admin-layout/breadcrumbs/breadcrumbs.module';
import { LiveNotificationModule } from './admin-layout/live-notification/live-notification.module';

import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { TitleComponent } from './admin-layout/title/title.component';

@NgModule({
  declarations: [
    TitleComponent,
    AdminLayoutComponent,
    AuthLayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule.forRoot(),
    AccordionDirectiveModule,
    PerfectScrollbarModule,
    SpinnerModule,
    ClickOutsideModule,
    BreadcrumbsModule,
    LiveNotificationModule
  ], 
  exports: [],
  providers: []
})
export class FamilyHubLayoutModule {}