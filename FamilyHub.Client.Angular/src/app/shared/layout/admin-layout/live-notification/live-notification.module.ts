import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClickOutsideModule } from 'ng-click-outside';
import { LiveNotificationComponent } from './live-notification.component';

@NgModule({
  declarations: [LiveNotificationComponent],
  imports: [CommonModule, ClickOutsideModule],
  exports: [LiveNotificationComponent]
})
export class LiveNotificationModule { }
