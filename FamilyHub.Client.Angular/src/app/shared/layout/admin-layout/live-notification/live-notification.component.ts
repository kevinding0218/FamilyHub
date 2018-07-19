import { LayoutService } from '../../../../core/services/layout.service';
import { notificationBottom } from '../../../utils/animations';
import { Component, OnInit, Input } from '@angular/core';
import {animate, AUTO_STYLE, state, style, transition, trigger} from '@angular/animations';

@Component({
  selector: 'app-live-notification',
  templateUrl: './live-notification.component.html',
  animations: [notificationBottom]
})
export class LiveNotificationComponent implements OnInit {

  @Input() liveNotification: string;
  @Input() liveNotificationClass: string;

  // @Output() notificationOutsideEmitter = new EventEmitter<string>();
  // @Output() toggleLiveNotificationEmitter = new EventEmitter<void>();

  constructor(private layoutService: LayoutService) { }

  ngOnInit() {
  }

  notificationOutsideClick(ele: string) {
    this.layoutService.notificationOutsideClick(ele);
  }

  toggleLiveNotification() {
    this.layoutService.toggleLiveNotification$.next();
  }
}
