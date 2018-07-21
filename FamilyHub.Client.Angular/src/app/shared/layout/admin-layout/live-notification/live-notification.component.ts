import { SharedService } from '../../../services/shared.service';
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

  constructor(private sharedService: SharedService) { }

  ngOnInit() {
  }

  notificationOutsideClick(ele: string) {
    console.log(ele);
    this.sharedService.liveNotification$.next(ele);
  }

  toggleLiveNotification() {
    this.sharedService.toggleLiveNotification$.next();
  }
}
