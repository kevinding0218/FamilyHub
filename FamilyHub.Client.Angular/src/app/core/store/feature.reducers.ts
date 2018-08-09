import * as fromApp from './app.reducers';
import { MemberState } from './member/member.reducer';

export interface FeatureState extends fromApp.AppState {
    members: MemberState;
}
