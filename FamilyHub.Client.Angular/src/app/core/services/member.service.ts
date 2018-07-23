import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
// import { MemberContactCreateRequest } from './../models/member/member.model';
import { ActionState } from '../config/action.config';
import { MemberDetailRequest } from '../models';

@Injectable()
export class MemberService {
    // #region Subject
    memberDetailAction$ = new Subject<{action: string, dataModel: MemberDetailRequest}>();
    // #endregion

    startCreateMemberDetail(memberDetail: MemberDetailRequest): void {
        this.memberDetailAction$.next({ action: ActionState.CREATE, dataModel: memberDetail });
    }
}
