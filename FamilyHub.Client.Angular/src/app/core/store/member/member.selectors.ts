import { createSelector, createFeatureSelector } from '@ngrx/store';
import * as fromReducer from './member.reducers';

export const getMemberState = createFeatureSelector<fromReducer.MemberState>(
    'members'
);

export const getMemberListFetchSuccess = createSelector(
    getMemberState,
    state => state.dataModel
);

export const getMemberListFetchFailure = createSelector(
    getMemberState,
    state => state.errorMsg
);

export const getMemberCreateSuccess = createSelector(
    getMemberState,
    state => state.dataModel
);

export const getMemberCreateFailure = createSelector(
    getMemberState,
    state => state.errorMsg
);
