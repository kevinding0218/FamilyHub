import { createSelector, createFeatureSelector } from '@ngrx/store';
import * as fromReducer from './member.reducer';

export const getMemberState = createFeatureSelector<fromReducer.MemberState>(
    'members'
);

export const getMemberList = createSelector(
    getMemberState,
    state => state.dataModel
);

export const getMemberCreateSuccess = createSelector(
    getMemberState,
    state => state.dataModel
);

export const getMemberCreateFailure = createSelector(
    getMemberState,
    state => state.errorMsg
);
