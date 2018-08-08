import {
    ActionReducer,
    ActionReducerMap,
    createFeatureSelector,
    createSelector,
    MetaReducer,
    Action
} from '@ngrx/store';

import { environment } from '../../../environments/environment';

import * as member from './member';

export interface AppState {
    member: member.MemberState;
}

export const reducers: ActionReducerMap<AppState> = {
    member: member.memberReducer
};

// console.log all actions
export function logger(
    reducer: ActionReducer<AppState>
): ActionReducer<AppState> {
    return function (state: AppState, action: any): AppState {
        console.log('\nstate', state);
        console.log('+ action', action);

        return reducer(state, action);
    };
}

export const metaReducers: MetaReducer<AppState>[] = [logger];

// export const effects = [
//     profile.ProfileEffects
// ];

// export const services = [notify.NotifyService];
