import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
// import { MemberContactCreateRequest } from './../models/member/member.model';
import { ActionState } from '../config/action.config';
import { MemberContactCreateRequest } from '../models';

@Injectable()
export class MemberService {
    // #region Subject
    memberAction$ = new Subject<{action: string, dataModel: MemberContactCreateRequest}>();
    // #endregion

    preloadMemberDetail(memberDetail: MemberContactCreateRequest): void {
        this.memberAction$.next({ action: ActionState.PRELOAD, dataModel: memberDetail });
    }
}
