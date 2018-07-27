import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
// import { MemberContactCreateRequest } from './../models/member/member.model';

import { MemberDetailRequest } from '../models';
import { ActionState } from '../../shared/services/action.config';

@Injectable()
export class MemberService {
    // #region Subject
    memberDetailAction$ = new Subject<{action: string, dataModel: MemberDetailRequest}>();
    // #endregion

    startCreateMemberDetail(memberDetail: MemberDetailRequest): void {
        this.memberDetailAction$.next({ action: ActionState.CREATE, dataModel: memberDetail });
    }
}
