import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { tap, filter, map, catchError, switchMap } from 'rxjs/operators';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/withLatestFrom';
import 'rxjs/add/operator/distinctUntilChanged';

import { MemberState } from './member.reducers';
import * as MemberActions from './member.actions';
import { MemberService, MemberContactListResponse } from './../../services/member.service';

@Injectable()
export class MemberEffects {

    constructor(
        private actions$: Actions,
        private store: Store<MemberState>,
        private memberService: MemberService
    ) { }

    // @Effect()
    // memberListFetch$ = this.actions$
    //     .ofType(MemberActions.MemberActionTypes.MemberListFetch)
    //     .switchMap((action: MemberActions.MemberListFetch) => {
    //         return this.memberService.listMemberContact(0);

    //         // return {
    //         //     type: MemberActions.MemberListFetchSuccess,
    //         //     payload: response.model as Array<MemberContactListResponse>
    //         // };
    //     })
    //     .map(
    //         (response) => {
    //             console.log('MemberEffects memberListFetch$: ', response);
    //             if (!response.didError) {
    //                 return new MemberActions.MemberListFetchSuccess(response.model as Array<MemberContactListResponse>);
    //             } else {
    //                 return new MemberActions.MemberListFetchFailure(response.errorMessage);
    //             }
    //         }
    //     );

    @Effect()
    memberListFetch$ = this.actions$.pipe(
        ofType(MemberActions.MemberActionTypes.MemberListFetch),
        switchMap((action: MemberActions.MemberListFetch) => {
            console.log('memberListFetch$ MemberListFetch action: ', action);
            return this.memberService.listMemberContact(1);
        }),
        map(
            (response) => {
                console.log('memberListFetch$ MemberListFetch response: ', response);
                if (!response.didError) {
                    return new MemberActions.MemberListFetchSuccess(response.model as Array<MemberContactListResponse>);
                } else {
                    return new MemberActions.MemberListFetchFailure(response.errorMessage);
                }
            },
        ),
    );
}
