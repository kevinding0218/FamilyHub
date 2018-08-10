import {
    ActionReducer,
    ActionReducerMap,
    createFeatureSelector,
    createSelector,
    MetaReducer,
    Action
} from '@ngrx/store';

import { environment } from '../../../environments/environment';

// tslint:disable-next-line:no-empty-interface
export interface AppState {
    // member: member.MemberState;
}

export const reducers: ActionReducerMap<AppState> = {
    // member: member.memberReducer
};

export function logger(
    reducer: ActionReducer<AppState>
): ActionReducer<AppState> {
    return function (state: AppState, action: any): AppState {
        console.log('state: ', state);
        console.log('action: ', action);

        return reducer(state, action);
    };
}

export const metaReducers: MetaReducer<AppState>[] = [logger];

export const effects = [

];
