import { MemberActions, MemberActionTypes } from './member.actions';

import { MemberContactListResponse } from '../../services/member.service';

const memberDefault: MemberContactListResponse = {
    memberContactID: 3,
    firstName: 'Tom',
    lastName: 'Cruise',
    fullName: 'Tom Cruise',
    contactPhone: '(123) 123-1231',
    mobilePhone: '(123) 123-1231',
    homePhone: '',
    location: 'California',
    emailAddress: '',
    memberRelationshipID: '6',
    memberRelationshipName: 'Friend',
    imageSource: 'assets/images/avatar-4.jpg',
    createdOn: '07/27/2018'
};

const memberListDefault: Array<MemberContactListResponse> = [memberDefault];

export interface MemberState {
    dataModel: Array<MemberContactListResponse>;
    errorMsg: string;
}

export const initialMemberState: MemberState = {
    dataModel: memberListDefault,
    errorMsg: ''
};

export function memberReducer(
    state = initialMemberState,
    action: MemberActions
): MemberState {
    switch (action.type) {
        case MemberActionTypes.MemberListFetch:
            return {
                ...state,
                dataModel: [...action.payload]
            };
        case MemberActionTypes.MemberContactCreateSuccess:
            return {
                ...state,
                dataModel: [...state.dataModel, action.payload]
            };
        case MemberActionTypes.MemberContactCreateFailure:
            return {
                ...state,
                errorMsg: action.payload
            };
        case MemberActionTypes.MemberContactUpdateSuccess:
            const oldMemberContact = state.dataModel[action.payload.memberContactIndex];
            const updatedMemberContact = {
                ...oldMemberContact,
                ...action.payload.updateMemberContact
            };
            const memberList = [...state.dataModel];
            memberList[action.payload.memberContactIndex] = updatedMemberContact;
            return {
                ...state,
                dataModel: memberList
            };
        case MemberActionTypes.MemberContactUpdateFailure:
            return {
                ...state,
                errorMsg: action.payload
            };
        case MemberActionTypes.MemberContactDeleteSuccess:
            const oldMemberList = [...state.dataModel];
            const newMemberList = oldMemberList.splice(action.payload, 1);
            return {
                ...state,
                dataModel: newMemberList
            };
        case MemberActionTypes.MemberContactDeleteFailure:
            return {
                ...state,
                errorMsg: action.payload
            };
        default:
            return state;
    }
}
