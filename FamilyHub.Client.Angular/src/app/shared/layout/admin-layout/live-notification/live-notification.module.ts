import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LiveNotificationComponent } from './live-notification.component';

@NgModule({
  declarations: [LiveNotificationComponent],
  imports: [CommonModule],  
  exports: [LiveNotificationComponent]
})
export class LiveNotificationModule { }
