import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';


@Injectable()
export class LayoutService {
    liveNotification$ = new Subject<string>();
    toggleLiveNotification$ = new Subject();

    notificationOutsideClick(ele: string) {
        this.liveNotification$.next(ele);
    }
}
