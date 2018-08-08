import { Action } from '@ngrx/store';
import { MemberDetailRequest, MemberDetailResponse, MemberContactListResponse } from '../../services/member.service';

export enum MemberActionTypes {
    MemberListFetch = '[MEMBER LIST Request] Fetch Action',
    MemberContactCreate = '[Member Contact Request] Create Action',
    MemberContactCreateSuccess = '[Member Contact Response] Create Action Success',
    MemberContactCreateFailure = '[Member Contact Response] Create Action Failure',
    MemberContactUpdate = '[Member Contact Request] Update Action',
    MemberContactUpdateSuccess = '[Member Contact Response] Update Action Success',
    MemberContactUpdateFailure = '[Member Contact Response] Update Action Failure',
    MemberContactDelete = '[Member Contact Request] Delete Action',
    MemberContactDeleteSuccess = '[Member Contact Response] Delete Action Success',
    MemberContactDeleteFailure = '[Member Contact Response] Delete Action Failure',
}

export class MemberListFetch implements Action {
    readonly type = MemberActionTypes.MemberListFetch;
    constructor(public payload: Array<MemberContactListResponse>) {}
}

export class MemberContactCreate implements Action {
    readonly type = MemberActionTypes.MemberContactCreate;
    constructor(public payload: MemberDetailRequest) {}
}

export class MemberContactCreateSuccess implements Action {
    readonly type = MemberActionTypes.MemberContactCreateSuccess;
    constructor(public payload: MemberDetailResponse) {}
}

export class MemberContactCreateFailure implements Action {
    readonly type = MemberActionTypes.MemberContactCreateFailure;
    constructor(public payload: string) {}
}

export class MemberContactUpdate implements Action {
    readonly type = MemberActionTypes.MemberContactUpdate;
    constructor(public payload: {memberContactId: number, updateMemberContact: MemberDetailRequest}) {}
}

export class MemberContactUpdateSuccess implements Action {
    readonly type = MemberActionTypes.MemberContactUpdateSuccess;
    constructor(public payload: {updateMemberContact: MemberDetailRequest, memberContactIndex: number}) {}
}

export class MemberContactUpdateFailure implements Action {
    readonly type = MemberActionTypes.MemberContactUpdateFailure;
    constructor(public payload: string) {}
}

export class MemberContactDelete implements Action {
    readonly type = MemberActionTypes.MemberContactDelete;
    constructor(public payload: number) {}
}

export class MemberContactDeleteSuccess implements Action {
    readonly type = MemberActionTypes.MemberContactDeleteSuccess;
    constructor(public payload: number) {}
}

export class MemberContactDeleteFailure implements Action {
    readonly type = MemberActionTypes.MemberContactDeleteFailure;
    constructor(public payload: string) {}
}

export type MemberActions =
    | MemberListFetch
    | MemberContactCreate | MemberContactCreateSuccess | MemberContactCreateFailure
    | MemberContactUpdate | MemberContactUpdateSuccess | MemberContactUpdateFailure
    | MemberContactDelete | MemberContactDeleteSuccess | MemberContactDeleteFailure;
